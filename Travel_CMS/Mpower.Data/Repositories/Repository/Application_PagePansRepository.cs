using System;
using System.Collections.Generic;
using System.Linq;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public class Application_PagePansRepository : IApplication_PagePansRepository
    {
        private ApplicationDbContext _db;
        private IApplication_ErrorsRepository _appError;
        public Application_PagePansRepository(ApplicationDbContext db)
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
                var Obj = _db.ObjPage_Pans.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogError(e, obj.applicationID, 0);
                return false;
            }
        }

        public bool Insert(Application_PagePans ObjPage_Pans)
        {           
            try
            {
                if (ObjPage_Pans == null)
                {
                    return false;
                }
                var Obj = _db.ObjPage_Pans.Add(ObjPage_Pans);            
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogError(e, ObjPage_Pans.applicationID, 0);
                return false;
            }
        }

        public Application_PagePans FindById(Int64 Id)
        {
            Int64 applicationId = 0;
            try
            {
                if (Id == 0)
                {
                    return null;
                }
                var Obj = _db.ObjPage_Pans.Where(m => m.id == Id).Single();
                applicationId = Obj == null ? 0 : Obj.applicationID;
                return Obj;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                return null;
            }
        }

        public IEnumerable<Application_PagePans> GetList()
        {
            Int64 applicationId = 0;
            try
            {
                if (!_db.ObjPage_Pans.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.ObjPage_Pans.ToList();
                applicationId = asyncEnumerable.Any() ? asyncEnumerable.First().applicationID : 0;
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                return null;
            }
        }


        public IEnumerable<Application_PagePans> GetListByApplicationId(Int64 Id)
        {
            try
            {
                if (!_db.ObjPage_Pans.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.ObjPage_Pans.Where(e => e.applicationID == Id).ToList();
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                LogError(e, Id, 0);
                return null;
            }
        }
        public Application_PagePans Update(Application_PagePans ObjPage_Pans)
        {
            try
            {
                if (ObjPage_Pans == null)
                {
                    return null;
                }
                _db.ObjPage_Pans.Update(ObjPage_Pans);
                _db.SaveChanges();
                return ObjPage_Pans;
            }
            catch (Exception e)
            {
                LogError(e, ObjPage_Pans.applicationID, 0);
                return null;
            }
        }

        public Int64 GetApplicationId_By_Page_PanId(Int64 Id)
        {
            Int64 applicationId = 0;
            try
            {
                if (Id == 0)
                {
                    return 0;
                }
                var asyncEnumerable = _db.ObjPage_Pans.Where(e => e.id == Id).Single();
                applicationId = asyncEnumerable.applicationID;
                return asyncEnumerable.applicationID;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                return 0;
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