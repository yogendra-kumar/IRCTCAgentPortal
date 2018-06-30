
using System.Collections.Generic;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail;
namespace Mpower.Rail.Data.Repository
{
    public class ConfigurationRepository : EntityBaseRepository<Configuration>, IConfigurationRepository
    {
        public ConfigurationRepository(ApplicationDbContext context) : base(context)
        { }

        public virtual new IEnumerable<Configuration> GetAll()
        {
            return base.GetAll();
        }
        public virtual IEnumerable<Configuration> GetAllByApplicationID(string applicationID)
        {
            return base.FindBy(s => s.applicationID.Equals(applicationID));
        }
    
    }
}