using System;
using System.Collections.Generic;
using System.Linq;
using Mpower.Data.Models;

namespace Mpower.Data.Repository
{
    public class Pages_MetadataRepository : IPages_MetadataRepository
    {
        private ApplicationDbContext _db;
        private IApplication_ErrorsRepository _appError;
        public Pages_MetadataRepository(ApplicationDbContext db)
        {
            _db = db;
            _appError = new Application_ErrorsRepository(_db);
        }

        public bool DeleteById(Int64 Id, Int64 PageId)
        {
            Int64 applicationId = 0;
            var obj = FindById(Id);
            try
            {
                if (obj == null)
                {
                    return false;
                }
                var Obj = _db.objPagesMetadata.Remove(obj);
                _db.SaveChanges();

                //update the meta id in pages primary table by reseting with 0
                Application_Pages application_Pages = _db.ObjPages.Where(x => x.id == PageId).SingleOrDefault();
                if (application_Pages.applicationID > 0)
                {
                    applicationId = application_Pages.applicationID;
                    application_Pages.metaId =0;
                    _db.ObjPages.Update(application_Pages);
                    _db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
               LogError(e, applicationId, PageId);
                return false;
            }
        }

        public bool Insert(Pages_Metadata pages_Metadata,Int64 pageId)
        {
            Int64 applicationId = 0;
            try
            {
                if (pages_Metadata == null)
                {
                    return false;
                }
                var Obj = _db.objPagesMetadata.Add(pages_Metadata);
                _db.SaveChanges();
                Application_Pages application_Pages = _db.ObjPages.Where(x=>x.id==pageId).SingleOrDefault();
                if (application_Pages.applicationID > 0)
                {
                    applicationId = application_Pages.applicationID;
                    application_Pages.metaId =pages_Metadata.id;
                    _db.ObjPages.Update(application_Pages);
                    _db.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception e)
            {
              LogError(e, applicationId, pageId);
                return false;
            }
        }

        public Pages_Metadata FindById(Int64 Id)
        {
            try
            {
                if (Id == 0)
                {
                    return null;
                }
                var Obj = _db.objPagesMetadata.Where(m => m.id == Id).Single();
                return Obj;
            }
            catch (Exception e)
            {
                var applicationPages = _db.ObjPages.Where(x => x.metaId == Id);
                if (applicationPages == null)
                    LogError(e, 0, 0);
                else
                    LogError(e, applicationPages.First().applicationID, applicationPages.First().id);
                return null;
            }
        }

        public IEnumerable<Pages_Metadata> GetList()
        {
            Int64 metaId = 0;
            try
            {
                if (!_db.objPagesMetadata.Any())
                {
                    return null;
                }
                var asyncEnumerable = _db.objPagesMetadata.ToList();
                metaId = asyncEnumerable.Any() ? asyncEnumerable.First().id : 0;
                return asyncEnumerable;
            }
            catch (Exception e)
            {
                if (metaId > 0)
                {
                    var applicatinPages = _db.ObjPages.Where(x => x.metaId == metaId);
                    if (applicatinPages == null)
                        LogError(e, 0, 0);
                    else
                        LogError(e, applicatinPages.First().applicationID, applicatinPages.First().id);
                }
                else
                {
                    LogError(e, 0, 0);
                }
                return null;
            }
        }

        public Pages_Metadata Update(Pages_Metadata pages_Metadata)
        {
            try
            {
                if (pages_Metadata == null)
                {
                    return null;
                }
                _db.objPagesMetadata.Update(pages_Metadata);
                _db.SaveChangesAsync();
                return pages_Metadata;
            }
            catch (Exception e)
            {
                var applicationPages = _db.ObjPages.Where(x=>x.metaId==pages_Metadata.id);
                if(applicationPages == null)
                 LogError(e,applicationPages.First().applicationID,applicationPages.First().id); 
                else
                 LogError(e,0,0);
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