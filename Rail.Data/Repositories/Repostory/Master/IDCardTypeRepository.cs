using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail.Master;

namespace Mpower.Rail.Data.Repository.Master
{
    public class IDCardTypeRepository:EntityBaseRepository<IDCardType>, IIDCardTypeRepository
    {
        public IDCardTypeRepository(ApplicationDbContext context) : base(context)
        { }        
    }
}