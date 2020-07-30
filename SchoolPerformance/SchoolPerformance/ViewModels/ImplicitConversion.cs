using SchoolPerformance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolPerformance.ViewModels
{
    public static class ImplicitConversion
    {
        /// <summary>
        /// Converts a list of SchoolResult to
        /// a list of ScatterplotViewModel
        /// </summary>
        /// <param name="source">A list of SchoolResult</param>
        /// <returns>A list of ScatterplotViewModel</returns>
        public static List<ScatterplotViewModel> ConvertToScatterplotViewModel (this IEnumerable<SchoolResult> source)
        {
            List<ScatterplotViewModel> output = new List<ScatterplotViewModel>();

            foreach(var item in source)
            {
                //Implicit conversion from SchoolResult to ScatterplotViewModel
                ScatterplotViewModel viewModel = item;
                output.Add(viewModel);
            }

            return output;
        }

        /// <summary>
        /// Converts a list of SchoolResult to
        /// a list of TableViewModel
        /// </summary>
        /// <param name="source">A list of SchoolResult</param>
        /// <returns>A list of TableViewModel</returns>
        public static List<TableViewModelAll> ConvertToTableViewModel (this IEnumerable<SchoolResult> source)
        {
            List<TableViewModelAll> output = new List<TableViewModelAll>();

            foreach (var item in source)
            {
                //Implicit conversion from SchoolResult to TableViewModel
                TableViewModelAll viewModel = item;
                output.Add(viewModel);
            }

            return output;
        }
    }

}
