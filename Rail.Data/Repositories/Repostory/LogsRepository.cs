using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Model.Rail;
using System.Collections.Generic;
using System;


namespace Mpower.Rail.Data.Repository
{
    public class LogsRepository : EntityBaseRepository<Logs>,ILogsRepository
    {
        public LogsRepository(ApplicationDbContext context) : base(context)
        {
            
        }
        
        public bool Insert(Logs Log)
        {
            try
            {
                if (Log != null)
                {
                    base.Add(Log);
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

        public Logs FindById(double Id)
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

        public IEnumerable<Logs> GetList()
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

        public IEnumerable<Logs> GetListBysession(string Session)
        {
            try
            {                
                return base.FindBy(e=>e.session.Equals(Session));
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