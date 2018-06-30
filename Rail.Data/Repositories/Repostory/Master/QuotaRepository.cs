using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail.Master;

namespace Mpower.Rail.Data.Repository.Master
{
    public class QuotaRepository : EntityBaseRepository<Quota>,IQuotaRepository
    {
        public QuotaRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}