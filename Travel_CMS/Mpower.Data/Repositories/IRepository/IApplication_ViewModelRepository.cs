using Mpower.Models.ApplicationViewModel;

namespace Mpower.Data.Repository
{
    public interface IApplication_ViewModelRepository
    {
      
        /// <summary>
        /// /// Get List of Application view model by ApplicationId
        /// </summary>
        /// <param name="Id">System.Int64 Type</param>
        /// <returns>IEnumerable Type Data of Application_Settings Type</returns>
        Application_ViewModel FindById(System.Int64 Id);
    }
}