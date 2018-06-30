using System.Collections.Generic;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public interface IApplication_FilesRepository
    {
        /// <summary>
        /// Inserts ApplicationErrors
        /// </summary>
        /// <param name="FileDetail"> parameter is the type of ApplicationErrors</param>
        /// <returns>bool value</returns>
        Application_Files Insert(Application_Files FileDetail);

         /// <summary>
        /// Find ApplicationErrors by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>ApplicationErrors</returns>
        Application_Files FindById(System.Int64 Id);


        /// <summary>
        /// Get List of ApplicationErrors
        /// </summary>
        /// <returns>IEnumerable Type Data of ApplicationErrors Type</returns>
        IEnumerable<Application_Files> GetList();

        /// <summary>
        /// /// /// Get List of ApplicationErrors by ApplicationId
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>IEnumerable Type Data of ApplicationErrors Type</returns>
        IEnumerable<Application_Files> GetListByApplicationId(System.Int64 Id);

          /// <summary>
        /// Update the Application_Files
        /// </summary>
        /// <param name="Files">Object of Application_Files</param>
        /// <returns>Application_Files</returns>
        Application_Files Update(Application_Files Files);

          /// <summary>
        /// Delete the Application_Files by Id
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>Boolean value</returns>
        bool DeleteById(System.Int64 Id);
    }
}