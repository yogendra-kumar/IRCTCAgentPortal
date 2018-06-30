using System;
using Mpower.Models.ClientViewModels;
using System.Collections.Generic;

namespace Mpower.Data.Repository
{
    public interface IClient_ConfigurationRepository
    {
         /// <summary>
        /// /// /// Get Application Configuration by ApplicationId
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>Data of Application Configuration Type</returns>
         ApplicationConfigurationViewModel GetApplicationConfigurationId(Int64 Id);


         /// <summary>
        /// /// /// Get Pages Configuration by PageId
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>Data of Pages Configuration Type</returns>
        PageConfigurationViewModel GetPagesConfigurationById (Int64 Id);


         /// <summary>
        /// /// /// Get List of Pages by ApplicationId
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>Data of Menu Configuration Type</returns>
       IEnumerable<MenuConfigurationViewModel> GetPagesListByApplicationId (Int64 Id);
    }
}