using System;
using System.Collections.Generic;
using System.Linq;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public class Application_FilesRepository : IApplication_FilesRepository, IDisposable
    {
        private ApplicationDbContext _db;
        private IApplication_ErrorsRepository _appError;
        public Application_FilesRepository(ApplicationDbContext db)
        {
            _db = db;
            _appError = new Application_ErrorsRepository(_db);
        }
        public Application_Files Insert(Application_Files FileDetail)
        {
            try
            {
                if (FileDetail == null)
                {
                    return null;
                }
                var Obj = _db.ObjApplicationFiles.Add(FileDetail);
                _db.SaveChanges();
                return FileDetail;
            }
            catch (Exception e)
            {
                LogError(e, FileDetail.applicationID, 0);
                return null;
            }
        }
        public Application_Files FindById(Int64 Id)
        {
            Int64 applicationId = 0;
            try
            {
                if (Id == 0)
                {
                    return null;
                }
                var Obj = _db.ObjApplicationFiles.Where(m => m.id == Id).Single();
                applicationId = Obj == null ? 0 : Obj.applicationID;
                return Obj;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                return null;
            }
        }

        public IEnumerable<Application_Files> GetList()
        {
            Int64 applicationId = 0;
            try
            {
                var asyncEnumerable = _db.ObjApplicationFiles.ToList();
                applicationId = asyncEnumerable.Any() ? asyncEnumerable.First().applicationID : 0;
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                LogError(e, applicationId, 0);
                return null;
            }
        }

        public IEnumerable<Application_Files> GetListByApplicationId(Int64 Id)
        {
            try
            {
                var asyncEnumerable = _db.ObjApplicationFiles.Where(Entity => Entity.applicationID == Id).ToList();
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                LogError(e, Id, 0);
                return null;
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
                var Obj = _db.ObjApplicationFiles.Remove(obj);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogError(e, obj.applicationID, 0);
                return false;
            }
        }

        public Application_Files Update(Application_Files Files)
        {
            try
            {
                if (Files == null)
                {
                    return null;
                }
                Files.updatedDate = DateTime.Now;
                _db.ObjApplicationFiles.Update(Files);
                _db.SaveChanges();
                return Files;
            }
            catch (Exception e)
            {
                LogError(e, Files.applicationID, 0);
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