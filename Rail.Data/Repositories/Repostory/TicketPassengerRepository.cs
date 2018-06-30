using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Ticket;


namespace Mpower.Rail.Data.Repository
{
    public class TicketPassengersRepository : EntityBaseRepository<TicketPassengers>, ITicketPassengersRepository
    {
        private ApplicationDbContext _context;
        public TicketPassengersRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public TicketPassengers AddPassenger(TicketPassengers objPassenger)
        {
            TicketPassengers dbEntityEntry = _context.Add(objPassenger).Entity;
            _context.SaveChanges();
            return dbEntityEntry;
        }
    }
}
