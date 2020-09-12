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

        public async Task<IEnumerable<T>> GetAll(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Include an orderBy query if there is an order by condition
            query = AddOrderQuery(query, orderBy);

            //Remove national data
            var result = National(query, false);

            return await result.ToListAsync();

        }

        public async Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Include filter and order by condition in the query
            query = AddFilterQuery(query, filter);

            query = AddOrderQuery(query, orderBy);

            //Remove national data
            var result = National(query, false);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Remove national data
            var result = National(query, false);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<T>> Get(
            Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Include filter condition in the query
            query = AddFilterQuery(query, filter);

            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Remove national data
            var result = National(query, false);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetNational(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Include filter and order by condition in the query
            query = AddFilterQuery(query, filter);

            query = AddOrderQuery(query, orderBy);

            //Get national data only
            var result = National(query, true);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetNational(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            //Include filter and order by condition in the query
            query = AddFilterQuery(query, filter);

            query = AddOrderQuery(query, orderBy);

            //Get national data only
            var result = National(query, true);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetNational(
            Expression<Func<T, bool>> filter = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Include filter condition in the query
            query = AddFilterQuery(query, filter);

            //Get national data only
            var result = National(query, true);

            return await result.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetNational(
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Include order by condition in the query
            query = AddOrderQuery(query, orderBy);

            //Get national data only
            var result = National(query, true);

            return await result.ToListAsync();
        }


        public async Task<IEnumerable<T>> GetNational(
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Get national data only
            var result = National(query, true);

            return await result.ToListAsync();
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
        /// Get or remove national data
        /// </summary>
        /// <param name="data">
        /// Query to include the national condition
        /// </param>
        /// <param name="getNational">
        /// If one requires the national data or not
        /// </param>
        /// <returns>Returns query with national condition</returns>
        private IQueryable<T> National(IQueryable<T> data,Boolean getNational)
        {
            
            //Build the expression
            ParameterExpression pe = Expression.Parameter(typeof(T), "s");

            MemberExpression me = Expression.Property(pe, "URN");

            ConstantExpression constant = Expression.Constant(9, typeof(int));

            //Create the condition URN == 9 if one requires the national data
            //or URN != 9 if one wants to exclude the national data
            Expression body = getNational? Expression.Equal(me, constant) : Expression.NotEqual(me, constant);


            //Create the expression data.where(s => s.URN !=9 ) to exclude national data
            //or the expression data.where(s => s.URN ==9 ) to only return national data
            MethodCallExpression callExpression = Expression.Call(
                    typeof(Queryable),
                    "Where",
                    new Type[] { data.ElementType },
                    data.Expression,
                    Expression.Lambda<Func<T, bool>>(body, new ParameterExpression[] { pe }));

            // Return an executable query from the expression tree.
            return data.Provider.CreateQuery<T>(callExpression);

        }

        public async Task<IEnumerable<T>> GetByUrnOrLAESATB(
            int id, 
            Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Distinguish between LAESTAB and URN
            //as LAESTAB is 7 digits 
            if (id.ToString().Length == 7)
            {
                query = LAESTABxpression(query, id);
            }
            else
            {
                query = URNExpression(query, id);
            }
            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Include filter and order by condition in the query
            query = AddFilterQuery(query, filter);

            query = AddOrderQuery(query, orderBy);

            return await query.ToListAsync();

        }

        public async Task<IEnumerable<T>> GetByUrnOrLAESATB(
            int id, 
            Expression<Func<T, bool>> filter = null, 
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Distinguish between LAESTAB and URN
            //as LAESTAB is 7 digits 
            if (id.ToString().Length == 7)
            {
                query = LAESTABxpression(query, id);
            }
            else
            {
                query = URNExpression(query, id);
            }
            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Include filter condition in the query
            query = AddFilterQuery(query, filter);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByUrnOrLAESATB(
            int id, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Distinguish between LAESTAB and URN
            //as LAESTAB is 7 digits 
            if (id.ToString().Length == 7)
            {
                query = LAESTABxpression(query, id);
            }
            else
            {
                query = URNExpression(query, id);
            }
            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            //Include order by condition in the query
            query = AddOrderQuery(query, orderBy);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetByUrnOrLAESATB(
            int id, 
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Distinguish between LAESTAB and URN
            //as LAESTAB is 7 digits 
            if (id.ToString().Length == 7)
            {
                query = LAESTABxpression(query, id);
            }
            else
            {
                query = URNExpression(query, id);
            }
            //Include/merge other DbSets into our query
            query = AddDbSets(query, includes);

            return await query.ToListAsync();
        }


        /// <summary>
        /// Builds an expression to get data by URN
        /// </summary>
        /// <param name="data">
        /// Query to include the condition
        /// </param>
        /// <param name="id">School URN</param>
        /// /// <returns>Returns query with URN == id condition</returns>
        private IQueryable<T> URNExpression(IQueryable<T> data, int id)
        {
            //Build the expression
            ParameterExpression pe = Expression.Parameter(typeof(T), "s");

            MemberExpression me = Expression.Property(pe, "URN");

            ConstantExpression constant = Expression.Constant(id, typeof(int));

            //Create the condition
            Expression body = Expression.Equal(me, constant);


            //Create the expression data.where(s => s.URN == id ) 
            MethodCallExpression callExpression = Expression.Call(
                    typeof(Queryable),
                    "Where",
                    new Type[] { data.ElementType },
                    data.Expression,
                    Expression.Lambda<Func<T, bool>>(body, new ParameterExpression[] { pe }));

            // Return an executable query from the expression tree.
            return data.Provider.CreateQuery<T>(callExpression);
        }

        private IQueryable<T> LAESTABxpression(IQueryable<T> data, int id)
        {
            //Get the first 3 digits of the 
            //Id which is the LA number
            var LA = id / 10000;

            //Get the last 4 digits of the 
            //Id which is the ESTAB number
            var ESTAB = id - (LA * 10000);

            ParameterExpression pe = Expression.Parameter(typeof(T), "s");

            MemberExpression meLEA;
            MemberExpression meESTAB;

            
            if (typeof(T) == typeof(School))
            {
                //Build a basic expression property where type is School

                meLEA = Expression.Property(pe, "LA");

                meESTAB = Expression.Property(pe, "ESTAB");
            }

            else
            {
                //Build a nested property expression of type T.School

                Expression school = Expression.Property(pe, typeof(T).GetProperty("School"));

                meLEA = Expression.Property(school, typeof(School).GetProperty("LA"));

                meESTAB = Expression.Property(school, typeof(School).GetProperty("ESTAB"));
            }

            ConstantExpression constantLEA = Expression.Constant(LA, typeof(int));

            ConstantExpression constantESTAB = Expression.Constant(ESTAB, typeof(int));

            //Create the condition  
            Expression body = Expression.AndAlso(
                Expression.Equal(meLEA, constantLEA), 
                Expression.Equal(meESTAB, constantESTAB));

            //Create the expression data.where(s => s.School.LA == LA && s.school.ESTAB == ESTAB) 
            //or if the type is School create the expression data.where(s => s.LA == LA && s.ESTAB == ESTAB) 
            MethodCallExpression callExpression = Expression.Call(
                    typeof(Queryable),
                    "Where",
                    new Type[] { data.ElementType },
                    data.Expression,
                    Expression.Lambda<Func<T, bool>>(body, new ParameterExpression[] { pe }));

            // Return an executable query from the expression tree.
            return data.Provider.CreateQuery<T>(callExpression);
        }


    }
}
