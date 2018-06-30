
using System.Collections.Generic;
using Mpower.Rail.Data;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;
using Mpower.Rail.Model.Rail;


public class MerchantRepository : EntityBaseRepository<Rail_Merchant>, IMerchantRepository
{
    public MerchantRepository(ApplicationDbContext context) : base(context)
    { }

    public virtual new IEnumerable<Rail_Merchant> GetAll()
    {
        return base.GetAll();
    }

    public bool IsUserExists(string UserName, string Password)
    {
        bool _isUserExists = false;
        Rail_Merchant _agent = base.GetSingle(s => s.UserName.Equals(UserName), s => s.Password.Equals(Password));

        if (_agent != null)
        {
            _isUserExists = true;
        }
        return _isUserExists;
    }

}