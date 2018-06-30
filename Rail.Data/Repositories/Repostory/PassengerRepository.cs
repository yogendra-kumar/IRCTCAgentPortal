
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail;
namespace Mpower.Rail.Data.Repository
{
    public class PassengerRepository : EntityBaseRepository<Passengers>, IPassengerRepository
    {
        public PassengerRepository(ApplicationDbContext context) : base(context)
        { }
    
    }
}