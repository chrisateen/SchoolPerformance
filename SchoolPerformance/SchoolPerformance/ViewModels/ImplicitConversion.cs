﻿using SchoolPerformance.Models;
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
        /// a list of TableViewModelAll
        /// </summary>
        /// <param name="source">A list of SchoolResult</param>
        /// <returns>A list of TableViewModelAll</returns>
        public static List<TableViewModelAll> ConvertToTableViewModelAll (this IEnumerable<SchoolResult> source)
        {
            List<TableViewModelAll> output = new List<TableViewModelAll>();

            foreach (var item in source)
            {
                //Implicit conversion from SchoolResult to TableViewModelAll
                TableViewModelAll viewModel = item;
                output.Add(viewModel);
            }

            return output;
        }

        /// <summary>
        /// Converts a list of SchoolResult to
        /// a list of TableViewModelDisadvantaged
        /// </summary>
        /// <param name="source">A list of SchoolResult</param>
        /// <returns>A list of TableViewModelDisadvantaged</returns>
        public static List<TableViewModelDisadvantaged> ConvertToTableViewModelDisadvantaged(this IEnumerable<SchoolResult> source)
        {
            List<TableViewModelDisadvantaged> output = new List<TableViewModelDisadvantaged>();

            foreach (var item in source)
            {
                //Implicit conversion from SchoolResult to TableViewModelAll
                TableViewModelDisadvantaged viewModel = item;
                output.Add(viewModel);
            }

            return output;
        }

        public static List<AutocompleteViewModel> ConvertToAutocompleteViewModel (this IEnumerable<SchoolResult> source)
        {
            List<AutocompleteViewModel> output = new List<AutocompleteViewModel>();

            foreach (var item in source)
            {
                //Implicit conversion from SchoolResult to AutocompleteViewModel
                AutocompleteViewModel viewModel = item;
                output.Add(viewModel);
            }

            return output;
        }
    }

}
