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
    public class SchoolResultRepository<T> : ISchoolResultRepository<T> where T : class
    {
        private SchoolPerformanceContext _context;
        private DbSet<T> _dbSet;

        public SchoolResultRepository(SchoolPerformanceContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public SchoolResultRepository()
        {
            _context = new SchoolPerformanceContext();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Checks if we have other DbSets to include/merge into our query
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = _dbSet.Include(include);
                }
            }

            //Include an orderBy query if there is an orderby condition
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();

        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _dbSet;

            //Checks if we have other DbSets to include/merge into our query
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = _dbSet.Include(include);
                }
            }

            //Include filter condition in the query
            query = AddFilterQuery(query, filter);

            //Include an orderBy query if there is an orderby condition
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            //Include an orderBy query if there is an orderby condition
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            //Include filter condition in the query
            query = AddFilterQuery(query, filter);

            //Include an orderBy query if there is an orderby condition
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
        }

        /// <summary>
        /// Include a filter condition in a query
        /// </summary>
        /// <param name="query">Query in which a filter condition will be added to the query</param>
        /// <param name="filter">Filter condition</param>
        /// <returns></returns>
        private IQueryable<T> AddFilterQuery(IQueryable<T> query, Expression<Func<T, bool>> filter = null)
        {
            //Filter the query if there is a filter condition
            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }
    }
}
