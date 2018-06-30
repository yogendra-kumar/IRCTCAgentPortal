using System;
using System.Collections.Generic;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Application;

namespace Mpower.Rail.Data.Repository
{
    public class ApplicationErrorsRepository : EntityBaseRepository<Application_Errors>, IApplicationErrorRepository
    {
        //private ApplicationDbContext _db;
        public ApplicationErrorsRepository(ApplicationDbContext context) : base(context)
        { }

        public bool Insert(Application_Errors Errors)
        {
            try
            {
                if (Errors != null)
                {
                    base.Add(Errors);
                    base.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Application_Errors FindById(double Id)
        {
            try
            {
                if (Id > 0)
                {                    
                    return base.GetSingle(e=>e.Id.Equals(Id));
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Application_Errors> GetList()
        {
            try
            {
                return base.GetAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Application_Errors> GetListByApplicationId(double ApplicationId)
        {
            try
            {                
                return base.FindBy(e=>e.applicationID.Equals(ApplicationId));
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Dispose the Current Object
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}