using System;
using System.Collections.Generic;
using Mpower.Models.ApplicationViewModel;

namespace Mpower.Data.Repository
{
    public interface IApplication_PageViewModelRepository
    {
         /// <summary>
        /// /// /// Get List of Page view model by ApplicationId
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>IEnumerable Type Data of Pages Type</returns>
        IEnumerable<PageViewModel> GetListByApplicationId(Int64 applicationId);

        PageViewModel FindById(Int64 Id);
    }
}