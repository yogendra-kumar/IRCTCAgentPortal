using System.Collections.Generic;
using Mpower.Data.Models;
using System;
using Mpower.Models.ApplicationViewModel;


namespace Mpower.Data.Repository
{
    public interface IApplication_ErrorViewModelRepository
    {
         /// <summary>
        /// Get List of ApplicationErrors by Page Index
        /// </summary>
        /// <returns>IEnumerable Type Data of ApplicationErrors Type</returns>
        ApplicationErrorViewModel GetPagedList(int pageIndex, int pages);

        /// <summary>
        /// /// /// Get List of ApplicationErrors by page index
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>IEnumerable Type Data of ApplicationErrors Type</returns>
       ApplicationErrorViewModel GetPagedListByApplicationId(Int64 Id, int pageIndex, int Pages);
    }
}