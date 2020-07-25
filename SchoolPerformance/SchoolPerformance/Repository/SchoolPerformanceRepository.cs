using Microsoft.EntityFrameworkCore;
using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SchoolPerformance.Repository
{
    /// <summary>
    /// Interface for the repository class using generics
    /// Enables one to get records from the database
    /// </summary>
    public class SchoolPerformanceRepository<T> : ISchoolPerformanceRepository<T> where T : class
    {
        private SchoolPerformanceContext _context;
        private DbSet<T> _dbSet;

        public SchoolPerformanceRepository(SchoolPerformanceContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Include an orderBy query if there is an order by condition
            query = AddOrderQuery(query, orderBy);

            //Return result removing national data
            return removeNational(query).ToList();

        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Include filter and order by condition in the query
            query = AddFilterQuery(query, filter);

            query = AddOrderQuery(query, orderBy);

            //Return result removing national data
            return removeNational(query).ToList();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            //Include an orderBy query if there is an order by condition
            query = AddOrderQuery(query, orderBy);

            //Return result removing national data
            return removeNational(query).ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            //Include filter and order by condition in the query
            query = AddFilterQuery(query, filter);

            query = AddOrderQuery(query, orderBy);

            //Return result removing national data
            return removeNational(query).ToList();
        }

        /// <summary>
        /// Include a filter condition in a query
        /// </summary>
        private IQueryable<T> AddFilterQuery(IQueryable<T> query, Expression<Func<T, bool>> filter = null)
        {
            //Filter the query if there is a filter condition
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        /// <summary>
        /// Include an order condition in a query 
        /// </summary>
        private IQueryable<T> AddOrderQuery(IQueryable<T> query, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            //Include an orderBy query if there is an order by condition
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        /// <summary>
        /// Include other DbSets in a query
        /// </summary>
        private IQueryable<T> AddDbSets(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            //Checks if we have other DbSets to include/merge into our query
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            return query;
        }

        /// <summary>
        /// Remove any national result
        /// </summary>
        private IQueryable<T> removeNational(IQueryable<T> data)
        {

            //Build the expression
            ParameterExpression pe = Expression.Parameter(typeof(T), "s");

            MemberExpression me = Expression.Property(pe, "URN");

            ConstantExpression constant = Expression.Constant(9, typeof(int));

                
            //Create the condition s.URN != 9
            Expression body = Expression.NotEqual(me, constant);

            //Create the expression data.where(s => s.URN !=9 )
            MethodCallExpression callExpression = Expression.Call(
                    typeof(Queryable),
                    "Where",
                    new Type[] { data.ElementType },
                    data.Expression,
                    Expression.Lambda<Func<T,bool>>(body, new ParameterExpression[] { pe }));

            // Return an executable query from the expression tree.
            return data.Provider.CreateQuery<T>(callExpression);

        }
    }
}
