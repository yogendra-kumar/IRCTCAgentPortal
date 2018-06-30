using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mpower.Data;
using Mpower.Data.Repository;
using Mpower.CMS.Api.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpower.CMS.Api.Controllers
{
    [RouteAttribute("ApplicationLayouts")]
    public class ApplicationLayoutsController : Controller
    {
        private IApplication_LayoutsRepository _application_LayoutRepository;
        public ApplicationLayoutsController(IApplication_LayoutsRepository application_LayoutsRepository)
        {
            _application_LayoutRepository=application_LayoutsRepository;
        }

       [HttpGetAttribute]
       [RouteAttribute("GetList")]
        public IActionResult GetList()
        {
            IEnumerable<Mpower.Data.Models.Application_Layouts> application_Layouts = _application_LayoutRepository.GetList();
            if(application_Layouts==null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed"});
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=application_Layouts });
        }

        [HttpGetAttribute]
        [RouteAttribute("GetListByApplicationId/{id}")]
        public IActionResult GetListByApplicationId(long id)
        {
            IEnumerable<Mpower.Data.Models.Application_Layouts> application_Layouts = _application_LayoutRepository.GetListByApplicationId(id);
            if(application_Layouts==null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=application_Layouts });
        }

        [HttpGetAttribute]
        [RouteAttribute("DeleteById/{id}")]
        public IActionResult DeleteById(long id)
        {
            if(_application_LayoutRepository.DeleteById(id))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "information removed", Status = "success"});
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "information not removed", Status = "failed" });
        }

        [HttpPostAttribute]
        [RouteAttribute("Update")]
        public IActionResult Update([FromBodyAttribute]Mpower.Data.Models.Application_Layouts _application_Layouts)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
            }
            Mpower.Data.Models.Application_Layouts application_Layouts = _application_LayoutRepository.Update(_application_Layouts);
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success", ResponseResult = application_Layouts });
        }

        [HttpGetAttribute]
        [RouteAttribute("FindById/{id}")]
        public IActionResult FindById(long id)
        {
            Mpower.Data.Models.Application_Layouts application_Layouts = _application_LayoutRepository.FindById(id);
            if (application_Layouts == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = application_Layouts });
        }

        [HttpPostAttribute]
        [RouteAttribute("Insert")]
        public IActionResult Insert([FromBodyAttribute]Mpower.Data.Models.Application_Layouts _application_Layouts)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
            }
            if (_application_LayoutRepository.Insert(_application_Layouts))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not saved", Status = "failed" });
        }
    }
}
