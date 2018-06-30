
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail;

namespace Mpower.Rail.Data.Repository
{
    public class TravelPassengerListRepository:EntityBaseRepository<TravelPassengerLists>, ITravelPassengerListRepository
    {
        public TravelPassengerListRepository(ApplicationDbContext context) : base(context)
        { }

        
    }
}