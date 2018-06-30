using System.Collections.Generic;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public interface IApplication_LayoutsRepository
    {

        /// <summary>
        /// Delete the Application_Layouts by Id
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>Boolean value</returns>
        bool DeleteById(System.Int64 Id);

          /// <summary>
        /// Insert Application_Layouts
        /// </summary>
        /// <param name="Layout">Object of Application_Layouts</param>
        /// <returns>Boolean value</returns>
        bool Insert(Application_Layouts Layout);


        /// <summary>
        /// Find Application_Layouts by Id
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>Application_Layouts</returns>
        Application_Layouts FindById(System.Int64 Id);


        /// <summary>
        /// Get List of Application_Layouts
        /// </summary>
        /// <returns>IEnumerable Type Data of Application_Layouts Type</returns>
        IEnumerable<Application_Layouts> GetList();


        /// <summary>
        /// Get List of Application_Layouts by ApplicationId
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>IEnumerable Type Data of Application_Layouts Type</returns>
        IEnumerable<Application_Layouts> GetListByApplicationId(System.Int64 Id);


        /// <summary>
        /// Update the Application_Layouts
        /// </summary>
        /// <param name="Layout">Object of Application_Layouts</param>
        /// <returns>Application_Layouts</returns>
        Application_Layouts Update(Application_Layouts Layout);

    }
}