using SchoolPerformance.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolPerformance.Cache
{
    public interface IRedisCache
    {
        /// <summary>
        /// Checks and gets AutoCompleteViewModel data from the cache
        /// </summary>
        /// <returns>
        /// List of AutoCompleteViewModel object if data is in cache
        /// or an empty AutoCompleteViewModel List if data is not in cache
        /// </returns>
        Task<IEnumerable<ScatterplotViewModel>> GetAutoCompleteData();

        /// <summary>
        /// Adds a list of AutoCompleteViewModel data to the cache
        /// </summary>
        /// <param name="autoCompleteDataLst">
        /// List of AutoCompleteViewModel data to be added to the cache
        /// </param>
        Task SaveAutoCompleteData(IEnumerable<ScatterplotViewModel> autoCompleteDataLst);

        /// <summary>
        /// Checks and gets ScatterplotViewModel data from the cache
        /// </summary>
        /// <returns>
        /// List of ScatterplotViewModel object if data is in cache
        /// or an empty ScatterplotViewModel List if data is not in cache
        /// </returns>
        Task<IEnumerable<ScatterplotViewModel>> GetScatterplotData();

        /// <summary>
        /// Adds a list of ScatterplotViewModel data to the cache
        /// </summary>
        /// <param name="scatterplotDataLst">
        /// List of ScatterplotViewModel data to be added to the cache
        /// </param>
        Task SaveScatterplotData(IEnumerable<ScatterplotViewModel> scatterplotDataLst);

        /// <summary>
        /// Checks and gets TableViewModelAll data from the cache
        /// </summary>
        /// <returns>
        /// List of TableViewModelAll data if data is in cache
        /// or an empty TableViewModelAll list if data is not in cache
        /// </returns>
        Task<IEnumerable<TableViewModelAll>> GetTableDataAll();

        /// <summary>
        /// Adds a list of TableViewModelAll data to the cache
        /// </summary>
        /// <param name="tableDataLst">
        /// List of TableViewModelAll data to be added to the cache
        /// </param>
        Task SaveTableDataAll(IEnumerable<TableViewModelAll> tableDataLst);

        /// <summary>
        /// Checks and gets TableViewModelDisadvantaged data from the cache
        /// </summary>
        /// <returns>
        /// List of TableViewModelDisadvantaged data if data is in cache
        /// or an empty TableViewModelDisadvantaged list if data is not in cache
        /// </returns>
        Task<IEnumerable<TableViewModelDisadvantaged>> GetTableDataDisadvantaged();

        /// <summary>
        /// Adds a list of TableViewModelDisadvantaged data to the cache
        /// </summary>
        /// <param name="tableDataLst">
        /// List of TableViewModelDisadvantaged data to be added to the cache
        /// </param>
        Task SaveTableDataDisadvantaged(IEnumerable<TableViewModelDisadvantaged> tableDataLst);

        /// <summary>
        /// Checks and gets National TableViewModelAll data from the cache
        /// </summary>
        /// <returns>
        /// National TableViewModelAll data if data is in cache
        /// or an empty TableViewModelAll object if data is not in cache
        /// </returns>
        Task<TableViewModelAll> GetNationalTableDataAll();

        /// <summary>
        /// Adds National TableViewModelAll data to the cache
        /// </summary>
        /// <param name=" nationalTableData">
        /// National TableViewModelAll data to be added to the cache
        /// </param>
        Task SaveNationalTableDataAll(TableViewModelAll nationalTableData);

        /// <summary>
        /// Checks and gets National TableViewModelDisadvantaged data from the cache
        /// </summary>
        /// <returns>
        /// National TableViewModelDisadvantaged data if data is in cache
        /// or an empty TableViewModelDisadvantaged object if data is not in cache
        /// </returns>
        Task<TableViewModelDisadvantaged> GetNationalTableDataDisadvantaged();

        /// <summary>
        /// Adds National TableViewModelDisadvantaged data to the cache
        /// </summary>
        /// <param name=" nationalTableData">
        /// National TableViewModelDisadvantaged data to be added to the cache
        /// </param>
        Task SaveNationalTableDataDisadvantaged(TableViewModelDisadvantaged nationalTableData);
    }
}