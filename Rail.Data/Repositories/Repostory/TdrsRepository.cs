using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail;



namespace Mpower.Rail.Data.Repository
{
    public class TdrsRepository : EntityBaseRepository<TdrDetails>,ITdrsRepository
    {
        public TdrsRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
