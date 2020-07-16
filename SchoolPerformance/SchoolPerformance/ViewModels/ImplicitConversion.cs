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
                //implicit convertion from SchoolResult to ScatterplotViewModel
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
        public static List<TableViewModel> ConvertToTableViewModel (this IEnumerable<SchoolResult> source)
        {
            List<TableViewModel> output = new List<TableViewModel>();

            foreach (var item in source)
            {
                //implicit convertion from SchoolResult to TableViewModel
                TableViewModel viewModel = item;
                output.Add(viewModel);
            }

            return output;
        }
    }

}
