using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mpower.Data.Repository;
using Mpower.Data.Models;
using Mpower.CMS.Api.Models;
using Mpower.Models.ApplicationViewModel;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpower.CMS.Api.Controllers
{
    [Route("Application")]
    public class ApplicationController : Controller
    {
        private IApplicationRepository _applicationRepository;
        private IApplication_ViewModelRepository _application_ViewModelRepository;
        public ApplicationController(IApplicationRepository applicationRepository,IApplication_ViewModelRepository application_ViewModelRepository)
        {
            _applicationRepository = applicationRepository;
            _application_ViewModelRepository=application_ViewModelRepository;
        }       

        // GET: /Application/GetList/    
        [HttpGetAttribute]
        [RouteAttribute("GetList")]
        public IActionResult GetList()
        {
            IEnumerable<Application> application = _applicationRepository.GetList();
            if(application == null)
            {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=application });
        }

        [HttpGet]
        [Route("FindById/{id}")]
        public IActionResult FindById(long id)
        {
            Application application = _applicationRepository.FindById(id);
            if(application == null)
            {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=application });
        }

        [HttpGet]
        [Route("DeleteById/{id}")]
        public IActionResult DeleteById(long id)
        {
            if(_applicationRepository.DeleteById(id))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information removed", Status = "success" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not removed", Status = "failed" });            
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody]Application _application)
        {
            if (!ModelState.IsValid)
            {
                //check valid response code 
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
            }
            if (_applicationRepository.Insert(_application))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success", ResponseResult = _application });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not saved", Status = "failed" });
        }

        [HttpPostAttribute]
        [Route("Update")]
        public IActionResult Update([FromBody]Application _application)
        {
            if (!ModelState.IsValid)
            {
                //check valid response code 
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
            }
            Application application = _applicationRepository.Update(_application);
            if(application == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not saved", Status = "failed" }); 
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success",ResponseResult=application }); 
        }

        [HttpGetAttribute]
        [RouteAttribute("View/{id}")]
        public IActionResult Details(long id)
        {
            Application_ViewModel application_ViewModel = _application_ViewModelRepository.FindById(id);
            if(application_ViewModel == null)
            {
               return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=application_ViewModel });
        }
    }
}
