using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Ticket;


namespace Mpower.Rail.Data.Repository
{
    public class TicketOrdersRepository : EntityBaseRepository<TicketOrders>, ITicketOrdersRepository
    {
        private ApplicationDbContext _context;
        public TicketOrdersRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public TicketOrders AddTicketOrder(TicketOrders objTicketOrdr)
        {
            TicketOrders dbEntityEntry = _context.Add(objTicketOrdr).Entity;
            _context.SaveChanges();
            return dbEntityEntry;
        }
    }
}
