using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Ticket;


namespace Mpower.Rail.Data.Repository
{
    public class BookedTicketsRepository : EntityBaseRepository<BookedTickets>,IBookedTicketsRepository
    {
        public BookedTicketsRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
