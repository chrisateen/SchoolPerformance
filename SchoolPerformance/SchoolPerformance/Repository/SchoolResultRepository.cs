using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.Repository
{
    public class SchoolResultRepository:ISchoolResultRepository
    {
        public IEnumerable<SchoolResult> removeNullResults()
        {
            //Get all properties in school result
            var allPropLst = typeof(SchoolResult).GetProperties();

           //Remove properties School, URN and Academic Year
            var propLst = allPropLst.Where(x => x.Name != "URN" || 
            x.Name != "ACADEMICYEAR" || x.Name != "School");
            
            throw new NotImplementedException();
        }


        //Count all null result method - define in interface and make private
        //get properties method - define in interface and make private
    }
}
