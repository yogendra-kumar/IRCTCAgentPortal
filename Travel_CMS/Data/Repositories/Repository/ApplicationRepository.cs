using System;
using System.Collections.Generic;
using System.Linq;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public class ApplicationRepository : IApplicationRepository
    {
        private ApplicationDbContext _db;
        private IApplication_ErrorsRepository _appError;
        public ApplicationRepository(ApplicationDbContext db)
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
                var Obj = _db.ObjApplication.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogError(e, Id, 0);
                return false;
            }
        }

        public bool Insert(Application ObjApplication)
        {
            try
            {
                if (ObjApplication == null)
                {
                    return false;
                }
                var Obj = _db.ObjApplication.Add(ObjApplication);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogError(e, ObjApplication.Id, 0);
                return false;
            }
        }

        public Application FindById(Int64 Id)
        {
            try
            {
                if (Id > 0)
                {
                    var Obj = _db.ObjApplication.Where(m => m.Id == Id).Single();
                    return Obj;
                }
                return null;
            }
            catch (Exception e)
            {
                LogError(e, Id, 0);
                return null;
            }
        }

        public IEnumerable<Application> GetList()
        {
            try
            {
                if (!_db.ObjApplication.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.ObjApplication.ToList();
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                LogError(e, 0, 0);
                return null;
            }
        }

        public Application Update(Application ObjApplication)
        {
            try
            {
                if (ObjApplication == null)
                {
                    return null;
                }

                _db.ObjApplication.Update(ObjApplication);
                _db.SaveChangesAsync();
                return ObjApplication;
            }
            catch (Exception e)
            {
                LogError(e, ObjApplication.Id, 0);
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