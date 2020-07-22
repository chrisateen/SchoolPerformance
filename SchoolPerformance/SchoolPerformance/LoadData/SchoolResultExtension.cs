using FastMember;
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
        //private static IEnumerable<string> _properties;
        //private static TypeAccessor _accessor;

        /// <summary>
        /// Remove any SchoolResult object where all the attainment and progress properties are null
        /// </summary>
        /// <param name="results">IEnumerable list of SchoolResult object to check</param>
        /// <returns>Returns a IEnumerable list of SchoolResult with null results removed</returns>
        public static IEnumerable<SchoolResult> removeNullResults(this IEnumerable<SchoolResult> results)
        {

            return results.Where(
                r => r.ATT8SCR != null ||
                r.ATT8SCR_FSM6CLA1A != null ||
                r.ATT8SCR_NFSM6CLA1A != null ||
                r.P8MEA != null ||
                r.P8MEA_FSM6CLA1A != null ||
                r.P8MEA_NFSM6CLA1A != null ||
                r.PTFSM6CLA1ABASICS_94 != null ||
                r.PTFSM6CLA1ABASICS_95 != null ||
                r.PTL2BASICS_94 != null ||
                r.PTL2BASICS_95 != null ||
                r.PTNOTFSM6CLA1ABASICS_94 != null ||
                r.PTNOTFSM6CLA1ABASICS_95 != null
                );

            //_properties = getProperties();

            //var schoolResultObj = new SchoolResult();

            //_accessor = TypeAccessor.Create(schoolResultObj.GetType());

            //return results.Where(r => resultsNotEmpty(r));
        }

        ///// <summary>
        ///// Get all properties of SchoolResult excluding URN and ACADEMICYEAR and School
        ///// </summary>
        ///// <returns>
        ///// IEnumerable list of properties in SchoolResult
        ///// </returns>
        //private static IEnumerable<String> getProperties()
        //{
        //    //Get all properties in school result
        //    var allProperties = typeof(SchoolResult).GetProperties();

        //    //Remove properties School, URN and Academic Year
        //    var propLst = allProperties.Where(x => x.Name != "URN"
        //    && x.Name != "ACADEMICYEAR" && x.Name != "School");

        //    var propStr = propLst.Select(p => p.Name);

        //    return propStr;
        //}

        ///// <summary>
        ///// Checks if all properties are null
        ///// </summary>
        ///// <param name="result">SchoolResult object to check</param>
        ///// <returns>Returns if some properties are not null</returns>
        //private static bool resultsNotEmpty(SchoolResult result)
        //{
        //    var includeSchool = false;

        //    foreach (var prop in _properties)
        //    {
        //        var data = _accessor[result, prop];

        //        //If one of the result property is not null then that school should be included
        //        if (data != null)
        //        {
        //            includeSchool = true;
        //            return includeSchool;
        //        }
        //    };

        //    return includeSchool;

        //}


    }
}
