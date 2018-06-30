using System.Collections.Generic;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public interface IApplicationRepository
    {
        /// <summary>
        /// Delete the Application by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Boolean Value</returns>
        bool DeleteById(System.Int64 Id);

        /// <summary>
        /// Insert Application
        /// </summary>
        /// <param name="ObjApplication">Object of Application</param>
        /// <returns>Boolean Value</returns>
        bool Insert(Application ObjApplication);


        /// <summary>
        /// Find Application by Id
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>Application</returns>
        Application FindById(System.Int64 Id);


        /// <summary>
        /// Get List of Application
        /// </summary>
        /// <returns>IEnumerable Type Data of Application Type</returns>
        IEnumerable<Application> GetList();


        /// <summary>
        /// Update the Application
        /// </summary>
        /// <param name="ObjApplication">Object of Application</param>
        /// <returns>Application</returns>
        Application Update(Application ObjApplication);
    }
}