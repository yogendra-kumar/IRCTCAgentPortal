using System.Collections.Generic;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public interface IApplication_PagePansRepository
    {
        /// <summary>
        /// Delete the Page_Pans by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Boolean Value</returns>
        bool DeleteById(System.Int64 Id);

        /// <summary>
        /// Insert Page_Pans
        /// </summary>
        /// <param name="ObjPage_Block">Object of Page_Pans</param>
        /// <returns>Boolean Value</returns>
        bool Insert(Application_PagePans ObjPage_Block);


        /// <summary>
        /// Find Page_Pans by Id
        /// </summary>
        /// <param name="Id">Int64 Type</param>
        /// <returns>Page_Pans</returns>
        Application_PagePans FindById(System.Int64 Id);


        /// <summary>
        /// Get List of Page_Pans
        /// </summary>
        /// <returns>IEnumerable Type Data of Page_Pans Type</returns>
        IEnumerable<Application_PagePans> GetList();


        /// <summary>
        /// /// /// Get List of Page_Pans by ApplicationId
        /// </summary>
        /// <param name="Id">Int64 Type</param>
        /// <returns>IEnumerable Type Data of Page_Pans Type</returns>
        IEnumerable<Application_PagePans> GetListByApplicationId(System.Int64 Id);

        /// <summary>
        /// Update the Page_Pans
        /// </summary>
        /// <param name="ObjPage_Pans">Object of Application_PagePans</param>
        /// <returns>Page_Pans</returns>
        Application_PagePans Update(Application_PagePans ObjPage_Pans);
    }
}