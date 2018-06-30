using System;
using System.Collections.Generic;
using System.Linq;
using Mpower.Data.Models;
using Mpower.Models.ApplicationViewModel;


namespace Mpower.Data.Repository
{
    public class Application_PageViewModelRepository : IApplication_PageViewModelRepository
    {
        private ApplicationDbContext _db;
         private IApplication_ErrorsRepository _appError;

        public Application_PageViewModelRepository(ApplicationDbContext db)
        {
            _db=db;
            _appError = new Application_ErrorsRepository(_db);
        }

        public PageViewModel FindById(Int64 Id)
        {
            try
            {
                if (_db.ObjPages.Any() && Id > 0)
                {
                    IApplication_PagesRepository _applicationPagesRepository = new Application_PagesRepository(_db);

                    IPages_MetadataRepository _pagesMetadataRepository = new Pages_MetadataRepository(_db);

                    var pageViewModels = (from pages in _db.ObjPages
                                          join layout in _db.ObjApplication_Layouts on pages.layoutID equals layout.id
                                          into layouts
                                          from l in layouts.DefaultIfEmpty()
                                          join metadata in _db.objPagesMetadata on pages.metaId equals metadata.id
                                          into pageMetadata
                                          from m in pageMetadata.DefaultIfEmpty()
                                          where pages.id == Id
                                          select new PageViewModel
                                          {
                                              applicationID = pages.applicationID,
                                              title = pages.title,
                                              isActive = pages.IsActive,
                                              layoutID = pages.layoutID,
                                              createdDate = pages.createdDate,
                                              modifiedDate = pages.modifiedDate,
                                              viewName = pages.viewName,
                                              layoutName = l == null ? "" : l.layoutName,
                                              id = pages.id,
                                              guid = pages.guid,
                                              index=pages.index,
                                              parentID = pages.parentID,
                                              externalUrl = pages.externalUrl,
                                              scriptFileId = pages.scriptFileId,
                                              pageHtml = pages.pageHtml,
                                              leftPanId = pages.leftPanId,
                                              rightPanId = pages.rightPanId,
                                              metaId = pages.metaId,
                                              keyword = m == null ? "" : m.keyword,
                                              description = m == null ? "" : m.description,
                                              pageList=_applicationPagesRepository.GetList().Select(x=>new SelectList{key=x.id,value=x.title})
                                              .Where(x=>x.key != Id)
                                              .OrderBy(x=>x.value)
                                          }).SingleOrDefault();

                    return pageViewModels;
                }
                return null;
            }
            catch (Exception e)
            {
              Int64 applicationId = _db.ObjPages.Where(x=>x.id==Id).SingleOrDefault()?.applicationID ?? 0;
              LogError(e, applicationId, Id);
                return null;
            }
        }

        public IEnumerable<PageViewModel> GetListByApplicationId(Int64 Id)
        {
            try
            {
                if (_db.ObjPages.Any() && Id > 0)
                {
                    var pageViewModels = (from pages in _db.ObjPages
                                          join layout in _db.ObjApplication_Layouts 
                                          on pages.layoutID equals layout.id into layouts
                                          from l in layouts.DefaultIfEmpty()                                         
                                          join metadata in _db.objPagesMetadata
                                          on pages.metaId equals metadata.id into metadatas
                                          from m in metadatas.DefaultIfEmpty()
                                          where pages.applicationID == Id
                                          select new PageViewModel
                                          {
                                              applicationID = pages.applicationID,
                                              title = pages.title,
                                              isActive = pages.IsActive,
                                              layoutID = pages.layoutID,
                                              createdDate = pages.createdDate,
                                              modifiedDate = pages.modifiedDate,
                                              viewName = pages.viewName,
                                              layoutName = l==null ? "" : l.layoutName,
                                              id = pages.id,
                                              guid = pages.guid,
                                              parentID = pages.parentID,
                                              externalUrl = pages.externalUrl,
                                              scriptFileId = pages.scriptFileId,
                                              pageHtml = pages.pageHtml,
                                              leftPanId = pages.leftPanId,
                                              rightPanId = pages.rightPanId,
                                              metaId = pages.metaId,
                                              //pageList=_applicationPagesRepository.GetList().Select(x=>new SelectList{key=x.id,value=x.title})
                                              keyword = m == null ? "" : m.keyword,
                                              description = m == null ? "" : m.description
                                          }).ToList();                                        
                    return pageViewModels;
                }                
                 return null;
            }
            catch (Exception e)
            {
               LogError(e, Id, 0);
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

    }
}

 