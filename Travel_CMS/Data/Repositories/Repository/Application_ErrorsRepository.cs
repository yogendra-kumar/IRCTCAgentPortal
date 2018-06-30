using System;
using System.Collections.Generic;
using System.Linq;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public class Application_ErrorsRepository : IApplication_ErrorsRepository, IDisposable
    {
        private ApplicationDbContext _db;
        public Application_ErrorsRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool Insert(Application_Errors Errors)
        {
            try
            {
                if (Errors == null)
                {
                    return false;
                }
                Errors.logDate=DateTime.Now;
                var Obj = _db.ObjApplicationErrors.Add(Errors);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool DeleteById(Int64 Id)
        {
            var obj = FindById(Id);
            try
            {
                if (obj == null)
                {
                    return false;
                }
                var Obj = _db.ObjApplicationErrors.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Application_Errors FindById(Int64 Id)
        {
            try
            {
                if (Id == 0)
                {
                    return null;
                }
                var Obj = _db.ObjApplicationErrors.Where(m => m.id == Id).Single();
                return Obj;
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
                var asyncEnumerable = _db.ObjApplicationErrors.ToList();
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Application_Errors> GetListByApplicationId(Int64 Id)
        {
            try
            {
                var asyncEnumerable = _db.ObjApplicationErrors.Where(Entity => Entity.applicationID == Id).ToList();
                return asyncEnumerable;
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