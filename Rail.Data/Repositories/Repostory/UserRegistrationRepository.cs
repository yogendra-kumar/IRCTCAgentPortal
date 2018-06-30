
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail;


namespace Mpower.Rail.Data.Repository
{
    public class UserRegistrationRepository : EntityBaseRepository<UserRegistration>, IUserRegistrationRepository
    {
        public UserRegistrationRepository(ApplicationDbContext context) : base(context)
        { }
    }
}