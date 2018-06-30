using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Ticket;



namespace Mpower.Rail.Data.Repository
{
    public class CancelledTicketPassengersRepository : EntityBaseRepository<CancelledTicketPassengers>,ICancelledTicketPassengersRepository
    {
        public CancelledTicketPassengersRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}