using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail.Master;


namespace Mpower.Rail.Data.Repository.Master
{
    public class BerthTypeRepository:EntityBaseRepository<BerthType>, IBerthTypeRepository
    {
        public BerthTypeRepository(ApplicationDbContext context) : base(context)
        { }        
    }
}