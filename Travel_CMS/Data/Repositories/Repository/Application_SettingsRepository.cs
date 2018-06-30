using System;
using System.Collections.Generic;
using System.Linq;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public class Application_SettingsRepository : IApplication_SettingsRepository
    {
        private ApplicationDbContext _db;
        private IApplication_ErrorsRepository _appError;
        public Application_SettingsRepository(ApplicationDbContext db)
        {
            _db = db;
            _appError = new Application_ErrorsRepository(_db);
        }

        public bool DeleteById(System.Int64 Id)
        {
            var obj = FindById(Id);
            try
            {
                if (obj == null)
                {
                    return false;
                }
                var Obj = _db.ObjApplication_Settings.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogError(e, obj.applicationID, 0);
                return false;
            }
        }

        public bool Insert(Application_Settings Settings)
        {
            try
            {
                if (Settings == null)
                {
                    return false;
                }
                var Obj = _db.ObjApplication_Settings.Add(Settings);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogError(e, Settings.applicationID, 0);
                return false;
            }
        }

        public Application_Settings FindById(Int64 Id)
        {
            Int64 applicationId = 0;
            try
            {
                if (Id == 0)
                {
                    return null;
                }
                var Obj = _db.ObjApplication_Settings.Where(m => m.id == Id).Single();
                applicationId = Obj == null ? 0 : Obj.applicationID;
                return Obj;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                return null;
            }
        }

        public IEnumerable<Application_Settings> GetList()
        {
            Int64 applicationId = 0;
            try
            {
                if (!_db.ObjApplication_Settings.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.ObjApplication_Settings.ToList();
                applicationId = asyncEnumerable.Any() ? asyncEnumerable.First().applicationID : 0;
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                return null;
            }
        }


        public IEnumerable<Application_Settings> GetListByApplicationId(Int64 Id)
        {
            try
            {
                if (!_db.ObjApplication_Settings.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.ObjApplication_Settings.Where(Entity => Entity.applicationID == Id).ToList();
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                LogError(e, Id, 0);
                return null;
            }
        }
        public Application_Settings Update(Application_Settings Settings)
        {
            try
            {
                if (Settings == null)
                {
                    return null;
                }
                _db.ObjApplication_Settings.Update(Settings);
                _db.SaveChanges();
                return Settings;
            }
            catch (Exception e)
            {
                LogError(e, Settings.applicationID, 0);
                return null;
            }
        }

        public Application_Settings GetByKey(string key)
        {
            try
            {
                if(string.IsNullOrEmpty(key))
                {
                    return null;
                }
                return _db.ObjApplication_Settings.Where(x=>x.key==key).FirstOrDefault();                
            }
            catch(Exception ex)
            {
                LogError(ex,0,0);
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