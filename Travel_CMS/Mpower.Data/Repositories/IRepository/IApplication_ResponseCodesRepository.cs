using System.Collections.Generic;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public interface IApplication_ResponseCodesRepository
    {
        /// <summary>
        /// Delete the ApplicationResponseCodes by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Boolean Value</returns>
        bool DeleteById(System.Int64 Id);

        /// <summary>
        /// Insert ApplicationResponseCodes
        /// </summary>
        /// <param name="ResponseCode">Object of ApplicationResponseCodes</param>
        /// <returns>Boolean Value</returns>
        bool Insert(Application_ResponseCodes ResponseCode);


        /// <summary>
        /// Find ApplicationResponseCodes by Id
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>ApplicationResponseCodes</returns>
        Application_ResponseCodes FindById(System.Int64 Id);


        /// <summary>
        /// Get List of ApplicationResponseCodes
        /// </summary>
        /// <returns>IEnumerable Type Data of ApplicationResponseCodes Type</returns>
        IEnumerable<Application_ResponseCodes> GetList();


        /// <summary>
        /// /// /// Get List of ApplicationResponseCodes by ApplicationId
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>IEnumerable Type Data of ApplicationResponseCodes Type</returns>
        IEnumerable<Application_ResponseCodes> GetListByApplicationId(System.Int64 Id);

        /// <summary>
        /// Update the ApplicationResponseCodes
        /// </summary>
        /// <param name="ResponseCode">Object of ApplicationResponseCodes</param>
        /// <returns>ApplicationResponseCodes</returns>
        Application_ResponseCodes Update(Application_ResponseCodes ResponseCode);
    }
}