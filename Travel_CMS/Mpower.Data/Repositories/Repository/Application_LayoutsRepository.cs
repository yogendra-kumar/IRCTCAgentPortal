using System;
using Mpower.Data.Models;
using System.Linq;
using System.Collections.Generic;

namespace Mpower.Data.Repository
{
    public class Application_LayoutsRepository : IApplication_LayoutsRepository, IDisposable
    {
        private ApplicationDbContext _db;
        private IApplication_ErrorsRepository _appError;
        public Application_LayoutsRepository(ApplicationDbContext db)
        {
            _db = db;
            _appError = new Application_ErrorsRepository(_db);
        }

        public bool DeleteById(Int64 Id)
        {
            var obj = FindById(Id);
            try
            {
                if (obj != null)
                {
                    var Obj = _db.ObjApplication_Layouts.Remove(obj);
                    _db.SaveChanges();
                    return true;
                }
                else
                {

                    return false;
                }
            }
            catch (Exception e)
            {

                LogError(e, obj.applicationID, 0);
                return false;
            }
        }

        public bool Insert(Application_Layouts Layout)
        {
            try
            {
                if (Layout != null)
                {
                    _db.ObjApplication_Layouts.Add(Layout);
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                LogError(e, Layout.applicationID, 0);
                return false;
            }
        }

        public Application_Layouts FindById(Int64 Id)
        {
            Int64 applicationId = 0;
            try
            {
                if (Id == 0)
                {
                    return null;
                }
                var Obj = _db.ObjApplication_Layouts.Where(m => m.id == Id).Single();
                applicationId = Obj == null ? 0 : Obj.applicationID;
                return Obj;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                return null;
            }
        }

        public IEnumerable<Application_Layouts> GetList()
        {
            Int64 applicationId = 0;
            try
            {
                if (!_db.ObjApplication_Layouts.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.ObjApplication_Layouts.ToList();
                applicationId = asyncEnumerable.Any() ? asyncEnumerable.First().applicationID : 0;
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                return null;
            }
        }

        public IEnumerable<Application_Layouts> GetListByApplicationId(Int64 Id)
        {
            try
            {
                if (!_db.ObjApplication_Layouts.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.ObjApplication_Layouts.Where(Entity => Entity.applicationID == Id).ToList();
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                 LogError(e, Id, 0);
                return null;
            }
        }
        public Application_Layouts Update(Application_Layouts Layout)
        {
            try
            {
                if (Layout == null)
                {
                    return null;
                }
                _db.ObjApplication_Layouts.Update(Layout);
                _db.SaveChanges();
                return Layout;
            }
            catch (Exception e)
            {
                LogError(e, Layout.applicationID, 0);
                return null;
            }
        }

    private void LogError(Exception ex,Int64 applicationId, Int64 pageId)
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