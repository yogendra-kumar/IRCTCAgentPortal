using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mpower.Data;
using Mpower.Data.Models;
using Mpower.Data.Repository;
using Mpower.CMS.Api.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpower.CMS.Api.Controllers
{
    [RouteAttribute("ApplicationErrors")]
    public class ApplicationErrorsController : Controller
    {
        private IApplication_ErrorsRepository _applicationErrorRepository;
        private IApplication_ErrorViewModelRepository _applicationErrorViewModelRepository;

        public ApplicationErrorsController(IApplication_ErrorsRepository applicationErrorsRepository,IApplication_ErrorViewModelRepository application_ErrorViewModelRepository)
        {
            _applicationErrorRepository=applicationErrorsRepository;
            _applicationErrorViewModelRepository=application_ErrorViewModelRepository;
        }

        [HttpGetAttribute]
        [RouteAttribute("GetList")]
       public IActionResult GetList()
       {
            IEnumerable<Application_Errors> applicationErrors = _applicationErrorRepository.GetList();
            if (applicationErrors == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = applicationErrors });
        }
        [HttpGetAttribute]
        [RouteAttribute("GetListByApplicationId/{id}")]
       public IActionResult GetListByApplicationId(long id)
       {
           IEnumerable<Application_Errors> applicationErrors = _applicationErrorRepository.GetListByApplicationId(id);
           if(applicationErrors==null)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
           }
           return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = applicationErrors });
       }

       [HttpGetAttribute]
       [RouteAttribute("FindById/{id}")]
       public IActionResult FindById(long id)
       {
           Application_Errors applicationErrors = _applicationErrorRepository.FindById(id);
           if(applicationErrors==null)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
           }
           return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = applicationErrors });
       }

       [HttpPostAttribute]
       [RouteAttribute("Insert")]
       public IActionResult Insert([FromBodyAttribute]Application_Errors _applicationErrors)
       {
           if(!ModelState.IsValid)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid Models", Status = "failed"});
           }
           if(_applicationErrorRepository.Insert(_applicationErrors))
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success", ResponseResult = _applicationErrors });
           }
           return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not saved", Status = "failed" });
       }

        [HttpGetAttribute]
        [RouteAttribute("DeleteById/{id}")]

        public IActionResult DeleteById(long id)
        {
            if (_applicationErrorRepository.DeleteById(id))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information removed", Status = "success" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not removed", Status = "failed" });
        }

         [HttpGetAttribute]
        [RouteAttribute("GetPagedList/{pageIndex}/{pages}")]
       public IActionResult GetPagedList(int pageIndex,int pages)
       {
            var applicatinErrorViewModel = _applicationErrorViewModelRepository.GetPagedList(pageIndex,pages);
            if (applicatinErrorViewModel == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = applicatinErrorViewModel });
        }
        [HttpGetAttribute]
        [RouteAttribute("GetPagedListByApplicationId/{id}/{pageIndex}/{pages}")]
       public IActionResult GetPagedListByApplicationId(long id,int pageIndex,int pages)
       {
           var applicationErrorViewModel = _applicationErrorViewModelRepository.GetPagedListByApplicationId(id,pageIndex,pages);
           if(applicationErrorViewModel==null)
           {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
           }
           return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = applicationErrorViewModel });
       }

    }
}
