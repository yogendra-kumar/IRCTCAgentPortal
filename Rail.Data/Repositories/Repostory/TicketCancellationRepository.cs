using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Ticket;


namespace Mpower.Rail.Data.Repository
{
    public class TicketCancellationRepository : EntityBaseRepository<TicketCancellations>,ITicketCancellationRepository
    {
       private  ApplicationDbContext _context;
        public TicketCancellationRepository(ApplicationDbContext context) : base(context)
        {
            _context=context;
        }
        public TicketCancellations AddCancelTicket(TicketCancellations objTicketCancel)
        {
             TicketCancellations dbEntityEntry = _context.Add(objTicketCancel).Entity;
            _context.SaveChanges();
            return dbEntityEntry;
        }
    }
}
