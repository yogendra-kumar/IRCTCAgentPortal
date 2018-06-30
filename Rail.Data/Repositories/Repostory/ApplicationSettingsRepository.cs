
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Application;

namespace Mpower.Rail.Data.Repository
{
    public class ApplicationSettingsRepository : EntityBaseRepository<Application_Settings>, IApplicationSettingsRepository
    {
        public ApplicationSettingsRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}