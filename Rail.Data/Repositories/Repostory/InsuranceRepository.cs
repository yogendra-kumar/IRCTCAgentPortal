using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Ticket;


namespace Mpower.Rail.Data.Repository
{
    public class InsuranceRepository : EntityBaseRepository<InsuranceDetails>,IInsuranceRepository
    {
        public InsuranceRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
