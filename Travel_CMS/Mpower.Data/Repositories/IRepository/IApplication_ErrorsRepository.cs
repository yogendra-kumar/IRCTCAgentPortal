using System.Collections.Generic;
using Mpower.Data.Models;
using System;

namespace Mpower.Data.Repository
{
    public interface IApplication_ErrorsRepository
    {
        /// <summary>
        /// Inserts ApplicationErrors
        /// </summary>
        /// <param name="Errors"> parameter is the type of ApplicationErrors</param>
        /// <returns>bool value</returns>
        bool Insert(Application_Errors Errors);

        /// <summary>
        /// Delete ApplicationErrors
        /// </summary>
        /// <param name="Errors"> parameter is the type of ApplicationErrors</param>
        /// <returns>bool value</returns>
        bool DeleteById(Int64 Id);

        /// <summary>
        /// Find ApplicationErrors by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>ApplicationErrors</returns>
        Application_Errors FindById(Int64 Id);


        /// <summary>
        /// Get List of ApplicationErrors
        /// </summary>
        /// <returns>IEnumerable Type Data of ApplicationErrors Type</returns>
        IEnumerable<Application_Errors> GetList();

        /// <summary>
        /// /// /// Get List of ApplicationErrors by ApplicationId
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>IEnumerable Type Data of ApplicationErrors Type</returns>
        IEnumerable<Application_Errors> GetListByApplicationId(Int64 Id);       
    }
}