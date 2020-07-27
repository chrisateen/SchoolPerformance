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
    public interface ISchoolPerformanceRepository<T> where T : class
    {
        /// <summary>
        /// Gets all records from a DbSet.
        /// </summary>
        /// <param name="orderBy">
        /// Use a function to specify how to order the records. 
        /// If ordering is not need enter null
        /// </param>
        /// <param name="includes">
        /// Specify a list of other DbSets to be included
        /// </param>
        /// <returns>Returns an IEnumerable list</returns>
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Gets all records from a DbSet.
        /// </summary>
        /// <param name="orderBy">
        /// Use a function to specify how to order the records. 
        /// If ordering is not need enter null
        /// </param>
        /// <returns>Returns an IEnumerable list</returns>
        public IEnumerable<T> GetAll(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);


        /// <summary>
        /// Gets records from a DbSet that meets a specified condition
        /// </summary>
        /// <param name="filter">
        /// Use a function to specify how to filter the records. 
        /// If filtering is not need enter null
        /// </param>
        /// <param name="orderBy">
        /// Use a function to specify how to order the records. 
        /// If ordering is not need enter null</param>
        /// <param name="includes">
        /// Specify a list of other DbSets to be included
        /// </param>
        /// <returns>Returns an IEnumerable list</returns>
        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Gets records from a DbSet that meets a condition
        /// </summary>
        /// <param name="filter">
        /// Use a function to specify how to filter the records. 
        /// If filtering is not need enter null</param>
        /// <param name="orderBy">
        /// Use a function to specify how to order the records. 
        /// If ordering is not need enter null</param>
        /// <returns>Returns an IEnumerable list</returns>
        public IEnumerable<T> Get(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        /// <summary>
        /// Gets the national records from a DbSet.
        /// </summary>
        /// <param name="filter">
        /// Use a function to specify how to filter the records. 
        /// If filtering is not need enter null</param>
        /// <param name="orderBy">
        /// Use a function to specify how to order the records. 
        /// If ordering is not need enter null</param>
        /// <param name="includes">
        /// Specify a list of other DbSets to be included
        /// </param>
        /// <returns>Returns an IEnumerable list</returns>
        public IEnumerable<T> GetNational(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Gets the national records from a DbSet.
        /// </summary>
        /// <param name="filter">
        /// Use a function to specify how to filter the records. 
        /// If filtering is not need enter null</param>
        /// <param name="orderBy">
        /// Use a function to specify how to order the records. 
        /// If ordering is not need enter null</param>
        /// <returns>Returns an IEnumerable list</returns>
        public IEnumerable<T> GetNational(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);


    }
}
