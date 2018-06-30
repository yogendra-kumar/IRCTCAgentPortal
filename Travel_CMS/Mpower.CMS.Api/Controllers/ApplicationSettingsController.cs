using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mpower.Data;
using Mpower.Data.Repository;
using Mpower.Data.Models;
using Mpower.CMS.Api.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpower.CMS.Api.Controllers
{
    [RouteAttribute("ApplicationSettings")]
    public class ApplicationSettingsController : Controller
    {
        private IApplication_SettingsRepository _application_SettingsRepository;
        
         public ApplicationSettingsController(IApplication_SettingsRepository application_SettingsRepository)
         {
             _application_SettingsRepository=application_SettingsRepository;
         }

         [HttpGetAttribute]
         [RouteAttribute("GetList")]
         public IActionResult GetList()
         {
             IEnumerable<Application_Settings> application_Settings = _application_SettingsRepository.GetList();
             if(application_Settings==null)
             {
                 return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
             }
             return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=application_Settings });
         }

         [HttpGetAttribute]
         [RouteAttribute("GetListByApplicationId/{id}")]
         public IActionResult GetListByApplicationId(long id)
         {             
            IEnumerable<Application_Settings> application_Settings=_application_SettingsRepository.GetListByApplicationId(id);
            if(application_Settings==null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=application_Settings });
         }

         [HttpPostAttribute]
         [RouteAttribute("Insert")]
         public IActionResult Insert([FromBodyAttribute]Application_Settings application_Settings)
         {
             if(!ModelState.IsValid)
             {
                 return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
             }
             if(_application_SettingsRepository.Insert(application_Settings))
             {
                 return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information saved", Status = "success",ResponseResult=application_Settings });
             }
             return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "information not saved", Status = "failed" });
         }

         [HttpPostAttribute]
         [RouteAttribute("Update")]
         public IActionResult Update([FromBodyAttribute]Application_Settings _application_Settings)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
            }
            Application_Settings application_Settings = _application_SettingsRepository.Update(_application_Settings);           
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "information saved", Status = "success", ResponseResult = application_Settings });
        }

        [HttpGetAttribute]
        [RouteAttribute("FindById/{id}")]
         public IActionResult FindById(long id)
         {
             Application_Settings application_Settings = _application_SettingsRepository.FindById(id);
             if(application_Settings==null)
             {
                 return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
             }
             return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success",ResponseResult=application_Settings });
         }

         [HttpGetAttribute]
         [RouteAttribute("DeleteById/{id}")]
         public IActionResult DeleteById(long id)
         {
          if(_application_SettingsRepository.DeleteById(id))
          {
              return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "Information removed", Status = "success" });
          }
          return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Information not removed", Status = "failed" });
         }

    }
}