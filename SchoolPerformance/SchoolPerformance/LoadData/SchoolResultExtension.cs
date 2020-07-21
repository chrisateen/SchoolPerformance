using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SchoolPerformance.LoadData
{
    /// <summary>
    /// Removes any school with no results
    /// </summary>
    public static class SchoolResultExtension
    {
        private static IEnumerable<PropertyInfo> _properties;

        /// <summary>
        /// Remove any SchoolResult object where all the attainment and progress properties are null
        /// </summary>
        /// <param name="results">IEnumerable list of SchoolResult object to check</param>
        /// <returns>Returns a IEnumerable list of SchoolResult with null results removed</returns>
        public static IEnumerable<SchoolResult> removeNullResults(this IEnumerable<SchoolResult> results)
        {
            //Get all the properties
            _properties = getProperties();

            //Remove schools that do not have any results
            var filteredResults = results.Where(x => resultsNotEmpty(x));

            return filteredResults;
        }

        /// <summary>
        /// Get all properties of SchoolResult excluding URN and ACADEMICYEAR and School
        /// </summary>
        /// <returns>
        /// IEnumerable list of properties in SchoolResult
        /// </returns>
        private static IEnumerable<PropertyInfo> getProperties()
        {
            //Get all properties in school result
            var allProperties = typeof(SchoolResult).GetProperties();

            //Remove properties School, URN and Academic Year
            var propLst = allProperties.Where(x => x.Name != "URN"
            && x.Name != "ACADEMICYEAR" && x.Name != "School");

            return propLst;
        }

        /// <summary>
        /// Checks if all properties are null
        /// </summary>
        /// <param name="result">SchoolResult object to check</param>
        /// <returns>Returns if some properties are not null</returns>
        private static bool resultsNotEmpty (SchoolResult result)
        {
            var includeSchool = false;

            foreach (var prop in _properties)
            {
                //If one of the result property is not null then that school should be included
                if(prop.GetValue(result) != null)
                {
                    includeSchool = true;
                    return includeSchool;
                }
            };

            return includeSchool;

        }

    }
}
