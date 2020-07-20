using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.Repository
{
    /// <summary>
    /// Enables one to remove records with no school results
    /// </summary>
    public interface ISchoolResultRepository
    {
        /// <summary>
        /// Removes any records that have null
        /// attainment or progress results
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SchoolResult> removeNullResults();
    }
}
