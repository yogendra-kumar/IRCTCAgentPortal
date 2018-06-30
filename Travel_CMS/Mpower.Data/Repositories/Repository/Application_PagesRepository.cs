using System;
using System.Collections.Generic;
using System.Linq;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public class Application_PagesRepository : IApplication_PagesRepository
    {
        private ApplicationDbContext _db;
        private IApplication_ErrorsRepository _appError;
        public Application_PagesRepository(ApplicationDbContext db)
        {
            _db = db;
            _appError = new Application_ErrorsRepository(_db);
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
                var Obj = _db.ObjPages.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogError(e, obj.applicationID, obj.id);
                return false;
            }
        }

        public bool Insert(Application_Pages ObjPages)
        {
            try
            {
                if (ObjPages == null)
                {
                    return false;
                }
                int maxIndex=0;
                ObjPages.createdDate=DateTime.Now;
                if(_db.ObjPages.Where(p=>p.index>=0).ToList().Count>0)
                {
                maxIndex=_db.ObjPages.Max(p=>p.index)+1;
                }
                ObjPages.index=maxIndex;
                var Obj = _db.ObjPages.Add(ObjPages);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogError(e, ObjPages.applicationID, ObjPages.id);
                return false;
            }
        }

        public Application_Pages FindById(Int64 Id)
        {
            Int64 applicationId = 0;
            try
            {
                if (Id == 0)
                {
                    return null;
                }
                var Obj = _db.ObjPages.Where(m => m.id == Id).Single();
                applicationId = Obj.applicationID;
                return Obj;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, Id);
                return null;
            }
        }

        public IEnumerable<Application_Pages> GetList()
        {
            Int64 applicationId = 0;
            try
            {
                if (!_db.ObjPages.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.ObjPages.ToList();
                applicationId = asyncEnumerable.Any() ? asyncEnumerable.First().applicationID : 0;
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                return null;
            }
        }

        public Application_Pages Update(Application_Pages ObjPages)
        {
            try
            {
                if (ObjPages == null)
                {
                    return null;
                }
                ObjPages.modifiedDate = DateTime.Now;
                _db.ObjPages.Update(ObjPages);
                _db.SaveChanges();
                return ObjPages;
            }
            catch (Exception e)
            {
                LogError(e, ObjPages.applicationID, ObjPages.id);
                return null;
            }
        }

        public IEnumerable<Application_Pages> GetListByApplicationId(Int64 Id)
        {
            try
            {
                if (!_db.ObjPages.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.ObjPages.Where(Entity => Entity.applicationID == Id).ToList();
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                LogError(e, Id, 0);
                return null;
            }
        }

        private void LogError(Exception ex, Int64 applicationId, Int64 pageId)
        {
            _appError.Insert(new Application_Errors
            {
                applicationID = applicationId,
                errorType = ex.GetType().ToString(),
                errorDescription = ex.Message,
                logDate = DateTime.Now.Date,
                pageID = pageId
            });
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