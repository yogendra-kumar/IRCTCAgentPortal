
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail;

namespace Mpower.Rail.Data.Repository
{
    public class TravelListRepository:EntityBaseRepository<TravelList>, ITravelListRepository
    {
        public TravelListRepository(ApplicationDbContext context) : base(context)
        { }

        
    }
}