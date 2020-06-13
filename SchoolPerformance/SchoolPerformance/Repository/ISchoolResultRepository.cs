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
    public interface ISchoolResultRepository<T> where T : class
    {
        /// <summary>
        /// Gets all records from a DbSet.
        /// </summary>
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                params Expression<Func<T, object>>[] includes);

        public IEnumerable<T> Get();
        

        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);
    }
}
