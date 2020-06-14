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

        public SchoolResultRepository(SchoolPerformanceContext context)
        {
            _context = context;
        }

        public SchoolResultRepository()
        {
            _context = new SchoolPerformanceContext();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            DbSet<T> dbSet = _context.Set<T>();
            IQueryable<T> query = dbSet;

            //Checks if we have other DbSets to include/merge into our query
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = dbSet.Include(include);
                }
            }

            //Order the query if there is an orderby condition
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();

        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            DbSet<T> dbSet = _context.Set<T>();
            IQueryable<T> query = dbSet;

            //Checks if we have other DbSets to include/merge into our query
            if (includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = dbSet.Include(include);
                }
            }

            //Filter the query if there is a filter condition
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Order the query if there is an orderby condition
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            DbSet<T> dbSet = _context.Set<T>();
            IQueryable<T> query = dbSet;

            //Order the query if there is an orderby condition
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            DbSet<T> dbSet = _context.Set<T>();
            IQueryable<T> query = dbSet;

            //Filter the query if there is a filter condition
            if (filter != null)
            {
                query = query.Where(filter);
            }

            //Order the query if there is an orderby condition
            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.ToList();
        }
    }
}
