using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mpower.Data;
using Mpower.Data.Models;
using Mpower.Data.Repository;
using Mpower.CMS.Api.Models;
using Mpower.Models.ApplicationViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpower.CMS.Api.Controllers
{
    [RouteAttribute("Pages")]
    public class PagesController : Controller
    {
       private  IApplication_PagesRepository _pagesRepository;
       private ApplicationDbContext _applicationDbContext;

       private IApplication_PageViewModelRepository _application_PageViewModelRepository;
       public PagesController (IApplication_PagesRepository pagesRepository, ApplicationDbContext applicationDbContext, IApplication_PageViewModelRepository application_PageViewModelRepository)
       {
           _applicationDbContext = applicationDbContext;
           _pagesRepository = pagesRepository;
           _application_PageViewModelRepository=application_PageViewModelRepository;
       }

       [HttpGetAttribute]
       [RouteAttribute("GetList")]
       public IActionResult GetList()
       {
           IEnumerable<Application_Pages> pages = _pagesRepository.GetList();
           if(pages==null)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
           }
           return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=pages });
       }

       [HttpGetAttribute]
       [RouteAttribute("GetListByApplicationId/{id}")]
       public IActionResult GetListByApplicationId(long id)
       {
           IEnumerable<Application_Pages> pages = _pagesRepository.GetListByApplicationId(id);
           if(pages==null)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
           }
           return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=pages });
       }

       [HttpPostAttribute]
       [RouteAttribute("Insert")]
       public IActionResult Insert([FromBodyAttribute]Application_Pages pages)
       {
            pages.guid=Guid.NewGuid().ToString();
            if (_pagesRepository.Insert(pages))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success", ResponseResult = pages });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not saved", Status = "failed" });
       }

       [HttpPostAttribute]
       [RouteAttribute("Update")]
       public IActionResult Update([FromBodyAttribute]Application_Pages _pages)
       {
           if(!ModelState.IsValid)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "invalid model", Status = "failed" });
           }
           Application_Pages pages = _pagesRepository.Update(_pages);
           return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success",ResponseResult=pages });
       }

       [HttpPostAttribute]
       [RouteAttribute("UpdateIndex")]
       public IActionResult UpdateIndex(Int64 pageId,Int32 Index)
       {
           if(pageId==0 || Index<0)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "invalid model", Status = "failed" });
           }
           Application_Pages pages = _pagesRepository.FindById(pageId);
           if(pages==null)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "invalid model", Status = "failed" });
           }
           pages.index=Index;
           Application_Pages pageUpdated = _pagesRepository.Update(pages);
           return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success",ResponseResult=pages });
       }

       [HttpGetAttribute]
       [RouteAttribute("FindById/{id}")]
       public IActionResult FindById(long id)
       {
           Application_Pages pages = _pagesRepository.FindById(id);
           if(pages==null)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
           }
           return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=pages });
       }

       [HttpGetAttribute]
       [RouteAttribute("DeleteById/{id}")]
       public IActionResult DeleteById (long id)
       {
           if(_pagesRepository.DeleteById(id))
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information removed", Status = "success" });
           }
           return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not removed", Status = "failed" });
       }

       [HttpGetAttribute]
       [RouteAttribute("View/{id}")]
       public IActionResult DetailsByApplicationId(Int64 id)
       {
            PageViewModel pageViewModel = _application_PageViewModelRepository.FindById(id);
            if (pageViewModel == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = pageViewModel });
           
       }

       [HttpGetAttribute]
       [RouteAttribute("ViewListByApplicationId/{applicationId}")]
       public IActionResult ListByApplicationId(Int64 applicationId)
       {
           IEnumerable<PageViewModel> pageViewModel = _application_PageViewModelRepository.GetListByApplicationId(applicationId);
            if (pageViewModel == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = pageViewModel });
       }
        
    }
}
