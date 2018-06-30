using System;
using Mpower.Models.ClientViewModels;
using Mpower.Data.Models;
using System.Linq;
using System.Collections.Generic;

namespace Mpower.Data.Repository
{
    public class Client_ConfigurationRepository : IClient_ConfigurationRepository
    {
        private ApplicationDbContext _db;
        private IApplication_ErrorsRepository _appError;

        public Client_ConfigurationRepository(ApplicationDbContext db)
        {
            _db= db;
            _appError=new Application_ErrorsRepository(_db);
        }

        public ApplicationConfigurationViewModel GetApplicationConfigurationId(Int64 Id)
        {
            try
            {               
                var applicationConfigurationViewModel = (from application in _db.ObjApplication 
                    join file in _db.ObjApplicationFiles on application.feviconFileId equals file.id into files 
                    from f in files.DefaultIfEmpty()
                    join layout in _db.ObjApplication_Layouts on application.layoutID equals layout.id into layouts
                    from l in layouts.DefaultIfEmpty()
                    join page_Pan in _db.ObjPage_Pans on application.footerId equals page_Pan.id into page_Pans
                    from p in page_Pans.DefaultIfEmpty()
                    where application.Id==Id
                    select new ApplicationConfigurationViewModel 
                    {
                        applicationName = application.applicationName,
                        feviconImageUrl = f == null ? "" : f.attachmentUrl,
                        logoImageUrl = application.logoFileId ==0 ? "" : _db.ObjApplicationFiles.Where(x=>x.id==application.logoFileId).Single().attachmentUrl,
                        footerContent = p == null ? "" : p.blockHtml,
                        layoutName = l==null ? "" : l.layoutName,
                        supportEmail = application.supportEmail
                    }).Single();
                return applicationConfigurationViewModel;
            }
            catch (Exception e)
            {
                LogError(e, Id, 0);
                return null;
            }
        }
        public PageConfigurationViewModel GetPagesConfigurationById(Int64 Id)
        {
            try
            {
                var pageConfigurationViewModel = (from page in _db.ObjPages 
                                    join file in _db.ObjApplicationFiles on page.scriptFileId equals file.id 
                                    into files from f in files.DefaultIfEmpty()
                                    join page_Pan in _db.ObjPage_Pans on page.leftPanId equals page_Pan.id 
                                    into page_Pans from p in page_Pans.DefaultIfEmpty()
                                    join metadata in _db.objPagesMetadata on page.metaId equals metadata.id
                                    into metadatas from m in metadatas.DefaultIfEmpty()
                                    where page.id == Id
                                    select new PageConfigurationViewModel {
                                        externalUrl = page.externalUrl,
                                        pageHtml=page.pageHtml,
                                        scriptFileUrl = f==null ? "" : f.attachmentUrl,
                                        scriptFileUrlType = f== null ? "" : f.attachmentUrl.Split('_')[2],
                                        title = page.title,
                                        viewName = page.viewName,
                                        leftPanHtmlContent = p == null ? "" : p.blockHtml,
                                        rightPanHtmlContent = page.rightPanId == 0 ? "" : _db.ObjPage_Pans.Where(x=>x.id==page.rightPanId).Single().blockHtml,
                                        keyword = m == null ? "" : m.keyword,
                                        description = m==null ? "" : m.description 
                                    }).Single();

                    return pageConfigurationViewModel;
            }
            catch (Exception e)
            {
                Int64 applicationId = _db.ObjPages.Where(x=>x.id==Id).SingleOrDefault()?.applicationID ?? 0;
                LogError(e, applicationId , Id);
                return null;
            }
        }

        public IEnumerable<MenuConfigurationViewModel> GetPagesListByApplicationId(Int64 Id)
        {
            try
            {
                var menuConfigurationViewModel = _db.ObjPages.Where(x=>x.applicationID==Id && x.IsActive==true)
                                .Select(x=> new MenuConfigurationViewModel{
                                    pageId = x.id,
                                    index = x.index,
                                    pageViewName = x.viewName,
                                    title = x.title,
                                    parentId=x.parentID
                                }).ToList();   
                    return menuConfigurationViewModel;                           
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