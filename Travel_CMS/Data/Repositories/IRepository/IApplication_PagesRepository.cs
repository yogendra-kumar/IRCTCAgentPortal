using System.Collections.Generic;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public interface IApplication_PagesRepository
    {
        /// <summary>
        /// Delete the Pages by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Boolean Value</returns>
        bool DeleteById(System.Int64 Id);

        /// <summary>
        /// Insert Pages
        /// </summary>
        /// <param name="ObjPages">Object of Pages</param>
        /// <returns>Boolean Value</returns>
        bool Insert(Application_Pages ObjPages);


        /// <summary>
        /// Find Pages by Id
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>Pages</returns>
        Application_Pages FindById(System.Int64 Id);


        /// <summary>
        /// Get List of Pages
        /// </summary>
        /// <returns>IEnumerable Type Data of Pages Type</returns>
        IEnumerable<Application_Pages> GetList();


        /// <summary>
        /// /// /// Get List of Application Pages view mode by ApplicationId
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>IEnumerable Type Data of Pages Type</returns>
        IEnumerable<Application_Pages> GetListByApplicationId(System.Int64 Id);

        /// <summary>
        /// Update the Pages
        /// </summary>
        /// <param name="ObjPages">Object of Pages</param>
        /// <returns>Pages</returns>
        Application_Pages Update(Application_Pages ObjPages);
    }
}