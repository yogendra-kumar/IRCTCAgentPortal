using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail;

namespace Mpower.Rail.Data.Repository
{
    public class MerchantTypeRepository : EntityBaseRepository<MerchantType>,IMerchantTypeRepository
    {
        public MerchantTypeRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}