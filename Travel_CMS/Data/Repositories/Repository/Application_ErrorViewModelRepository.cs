using System;
using System.Linq;
using Mpower.Data.Models;
using System.Collections.Generic;
using Mpower.Models.ApplicationViewModel;

namespace Mpower.Data.Repository
{
    public class Application_ErrorViewModelRepository : IApplication_ErrorViewModelRepository
    {
         private ApplicationDbContext _db;

         public Application_ErrorViewModelRepository(ApplicationDbContext db)
         {
             _db = db;
         }

        public ApplicationErrorViewModel GetPagedList(int pageIndex, int pages)
        {
            try
            {
                var applicationError = _db.ObjApplicationErrors.Select(
                                            x => new ApplicationError
                                            {
                                                id = x.id,
                                                applicationID = x.applicationID,
                                                pageID = x.pageID,
                                                errorType = x.errorType,
                                                errorDescription = x.errorDescription,
                                                logDate = x.logDate
                                            })
                                            .Skip((pageIndex - 1) * pages).Take(pages).ToList();
                return new ApplicationErrorViewModel
                {
                    applicationError = applicationError,
                    totalCount = _db.ObjApplicationErrors.Count()
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ApplicationErrorViewModel GetPagedListByApplicationId(Int64 id, int pageIndex, int pages)
        {
            try
            {
                var applicationError = _db.ObjApplicationErrors.Select(
                     x => new ApplicationError
                     {
                         id = x.id,
                         applicationID = x.applicationID,
                         pageID = x.pageID,
                         errorType = x.errorType,
                         errorDescription = x.errorDescription,
                         logDate = x.logDate
                     })
                    .Where(Entity => Entity.applicationID == id).Skip((pageIndex - 1) * pages).Take(pages).ToList();
                return new ApplicationErrorViewModel
                {
                    applicationError = applicationError,
                    totalCount = _db.ObjApplicationErrors.Count()
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}