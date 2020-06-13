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

        public IEnumerable<T> Get()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            throw new NotImplementedException();
        }
    }
}
