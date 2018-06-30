using System;
using System.Collections.Generic;
using System.Linq;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public class Application_ResponseCodesRepository : IApplication_ResponseCodesRepository
    {
        private ApplicationDbContext _db;
        private IApplication_ErrorsRepository _appError;
        public Application_ResponseCodesRepository(ApplicationDbContext db)
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
                var Obj = _db.ObjApplicationResponseCodes.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogError(e, obj.applicationID, 0);
                return false;
            }
        }

        public bool Insert(Application_ResponseCodes ResponseCode)
        {
            try
            {
                if (ResponseCode == null)
                {
                    return false;
                }
                var Obj = _db.ObjApplicationResponseCodes.Add(ResponseCode);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogError(e, ResponseCode.applicationID, 0);
                return false;
            }
        }

        public Application_ResponseCodes FindById(Int64 Id)
        {
            Int64 applicationId = 0;
            try
            {
                if (Id == 0)
                {
                    return null;
                }
                var Obj = _db.ObjApplicationResponseCodes.Where(m => m.id == Id).Single();
                applicationId = Obj == null ? 0 : Obj.applicationID;
                return Obj;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                throw e;
            }
        }

        public IEnumerable<Application_ResponseCodes> GetList()
        {
            Int64 applicationId = 0;
            try
            {
                if (!_db.ObjApplicationResponseCodes.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.ObjApplicationResponseCodes.ToList();
                applicationId = asyncEnumerable.Any() ? asyncEnumerable.First().applicationID : 0;
                return asyncEnumerable;

            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                return null;
            }
        }

        public IEnumerable<Application_ResponseCodes> GetListByApplicationId(Int64 Id)
        {
            try
            {
                if (!_db.ObjApplicationResponseCodes.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.ObjApplicationResponseCodes.Where(Entity => Entity.applicationID == Id).ToList();
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                LogError(e, Id, 0);
                return null;
            }
        }
        public Application_ResponseCodes Update(Application_ResponseCodes ObjApplicationResponseCode)
        {
            try
            {
                if (ObjApplicationResponseCode == null)
                {
                    return null;
                }
                _db.ObjApplicationResponseCodes.Update(ObjApplicationResponseCode);
                _db.SaveChanges();
                return ObjApplicationResponseCode;
            }
            catch (Exception e)
            {
                LogError(e, ObjApplicationResponseCode.applicationID, 0);
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