using System.Collections.Generic;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public interface IApplication_SettingsRepository
    {
        /// <summary>
        /// Delete the Application_Settings by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Boolean Value</returns>
        bool DeleteById(System.Int64 Id);

        /// <summary>
        /// Insert Application_Settings
        /// </summary>
        /// <param name="Settings">Object of Application_Settings</param>
        /// <returns>Boolean Value</returns>
        bool Insert(Application_Settings Settings);


        /// <summary>
        /// Find Application_Settings by Id
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>Application_Settings</returns>
        Application_Settings FindById(System.Int64 Id);


        /// <summary>
        /// Get List of Application_Settings
        /// </summary>
        /// <returns>IEnumerable Type Data of Application_Settings Type</returns>
        IEnumerable<Application_Settings> GetList();


        /// <summary>
        /// /// Get List of Application_Settings by ApplicationId
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>IEnumerable Type Data of Application_Settings Type</returns>
        IEnumerable<Application_Settings> GetListByApplicationId(System.Int64 Id);

        /// <summary>
        /// Update the Application_Settings
        /// </summary>
        /// <param name="Settings">Object of Application_Settings</param>
        /// <returns>Application_Settings</returns>
        Application_Settings Update(Application_Settings Settings);


        /// <summary>
        /// find the Application_Settings by key
        /// </summary>
        /// <param name="Settings">key of Application_Settings</param>
        /// <returns>Application_Settings</returns>
        Application_Settings GetByKey(string key);
    }
}