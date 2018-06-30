using System;
using System.Linq;
using Mpower.Data.Models;
using Mpower.Models.ApplicationViewModel;

namespace Mpower.Data.Repository
{
    public class Application_ViewModelRepository : IApplication_ViewModelRepository
    {
        private ApplicationDbContext _db;
         private IApplication_ErrorsRepository _appError;

         public Application_ViewModelRepository(ApplicationDbContext db)
         {
             _db=db;
             _appError = new Application_ErrorsRepository(_db);
         }

         public Application_ViewModel FindById (System.Int64 Id)
         {
             try
            {
                if (Id > 0)
                {
                    IApplication_LayoutsRepository _applicationLayoutRepository = new Application_LayoutsRepository(_db);
                    IApplication_PagePansRepository _applicationPagePansRepository = new Application_PagePansRepository(_db);
                    Application_ViewModel obj = new Application_ViewModel();
                    var application = _db.ObjApplication.Where(x => x.Id == Id).SingleOrDefault();
                    if (application == null)
                    {
                        return null;
                    }
                    var favicon = _db.ObjApplicationFiles.Where(x => x.id == application.feviconFileId).SingleOrDefault();
                    var logo = _db.ObjApplicationFiles.Where(x => x.id == application.logoFileId).SingleOrDefault();
                    var layoutList = _applicationLayoutRepository.GetList();
                    obj.domainIP = application.domainIP;
                    obj.domainUrl = application.domainUrl;
                    obj.favIconImageId = application.feviconFileId;
                    if (favicon == null)
                    {
                        obj.favIconImageUrl = "";
                    }
                    else
                    {
                        obj.favIconImageUrl = favicon.attachmentUrl;
                    }
                    obj.layoutId = application.layoutID;
                    if (layoutList == null)
                    {
                        obj.layoutList = null;
                    }
                    else
                    {
                        obj.layoutList = layoutList.Select(x => new SelectList { key = x.id, value = x.layoutName });
                    }
                    obj.logoImageId = application.logoFileId;
                    if (logo == null)
                    {
                        obj.logoImageUrl = "";
                    }
                    else
                    {
                        obj.logoImageUrl = logo.attachmentUrl;
                    }
                    obj.name = application.applicationName;
                    obj.Id = application.Id;
                    obj.guid = application.guid;
                    obj.supportEmail = application.supportEmail;
                    obj.footerId = application.footerId;
                    var pageBlockList = _applicationPagePansRepository.GetList();
                    if (pageBlockList == null)
                    {
                        obj.pageBlockList = null;
                    }
                    else
                    {
                        obj.pageBlockList = pageBlockList.Select(x => new SelectList { key = x.id, value = x.title });
                    }
                    return obj;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _appError.Insert(new Application_Errors
                {
                    applicationID = Id,
                    errorType = e.GetType().ToString(),
                    errorDescription = e.Message,
                    logDate = DateTime.Now.Date,
                    pageID = 0
                });
                return null;
            }
         }

    }
}