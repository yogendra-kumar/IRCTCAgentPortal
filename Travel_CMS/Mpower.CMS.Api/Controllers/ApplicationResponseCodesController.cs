using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mpower.Data;
using Mpower.Data.Models;
using Mpower.Data.Repository;
using Mpower.CMS.Api.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpower.CMS.Api.Controllers
{
    [RouteAttribute("ApplicationResponseCodes")]
    public class ApplicationResponseCodesController : Controller
    {
        private IApplication_ResponseCodesRepository _application_ResponseCodesRepository;

        public ApplicationResponseCodesController(IApplication_ResponseCodesRepository application_ResponseCodeRepository)
        {
            _application_ResponseCodesRepository=application_ResponseCodeRepository;
        }

        [HttpGetAttribute]
        [RouteAttribute("GetList")]
        public IActionResult GetList()
        {
            IEnumerable<Application_ResponseCodes> applicationResponseCodes = _application_ResponseCodesRepository.GetList();
            if(applicationResponseCodes == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=applicationResponseCodes });
        }

        [HttpGetAttribute]
        [RouteAttribute("GetListByApplicationId/{id}")]
        public IActionResult GetListByApplicationId(long id)
        {
            IEnumerable<Application_ResponseCodes> applicationResponseCodes = _application_ResponseCodesRepository.GetListByApplicationId(id);
            if(applicationResponseCodes==null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=applicationResponseCodes });
        }

        [HttpPostAttribute]
        [RouteAttribute("Insert")]
        public IActionResult Insert([FromBodyAttribute]Application_ResponseCodes applicationResponseCodes)
        {
            if(!ModelState.IsValid)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
            }
            if(_application_ResponseCodesRepository.Insert(applicationResponseCodes))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "information saved", Status = "success",ResponseResult=applicationResponseCodes });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "information not saved", Status = "failed" });
        }

        [HttpPostAttribute]
        [RouteAttribute("Update")]
        public IActionResult Update([FromBodyAttribute]Application_ResponseCodes _applicationResponseCodes)
        {
            if(!ModelState.IsValid)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
            }
            Application_ResponseCodes applicationResponseCodes = _application_ResponseCodesRepository.Update(_applicationResponseCodes);
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "information saved", Status = "success",ResponseResult=applicationResponseCodes });
        }

        [HttpGetAttribute]
        [RouteAttribute("FindById/{id}")]
        public IActionResult FindById(long id)
        {
            Application_ResponseCodes applicationResponseCodes = _application_ResponseCodesRepository.FindById(id);
            if(applicationResponseCodes==null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=applicationResponseCodes });
        }

        [HttpGetAttribute]
        [RouteAttribute("DeleteById/{id}")]
        public IActionResult DeleteById(long id)
        {
            if(_application_ResponseCodesRepository.DeleteById(id))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information removed", Status = "success" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not removed", Status = "failed" });
        }      
       
    }
}
