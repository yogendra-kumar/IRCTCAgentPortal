using Mpower.Rail.Model.Rail.Master;
using Mpower.Rail.Data.IRepository;

namespace Mpower.Rail.Data.Repository.Master
{
    public class TicketStatusRepository : EntityBaseRepository<TicketStatus>,ITicketStatusRepository
    {
        public TicketStatusRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}