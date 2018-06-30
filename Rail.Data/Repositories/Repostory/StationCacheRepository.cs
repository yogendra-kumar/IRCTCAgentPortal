using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail;

namespace Mpower.Rail.Data.Repository
{
    public class StationCacheRepository : EntityBaseRepository<StationsCache>,IStationCacheRepository
    {
        public StationCacheRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}