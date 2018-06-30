using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mpower.Data.Repository;
using Mpower.Data;
using Mpower.Data.Models;
using Mpower.CMS.Api.Models;
using Mpower.Models.ClientViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Mpower.CMS.Api.Controllers
{
    [Route("Configuration")]
    public class ClientConfigurationController : Controller
    {
        private IApplicationRepository _applicationRepository;
        private IClient_ConfigurationRepository _client_ConfigurationRepository;
        
        public ClientConfigurationController(IClient_ConfigurationRepository client_ConfigurationRepository,IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
            _client_ConfigurationRepository=client_ConfigurationRepository;
        }

         [HttpGetAttribute]
         [RouteAttribute("ApplicationSettings/{id}")]
         public IActionResult GetApplicationSettingsById(long id)
         {
            ApplicationConfigurationViewModel applicationConfigurationViewModel = _client_ConfigurationRepository.GetApplicationConfigurationId(id);
             if (applicationConfigurationViewModel == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = applicationConfigurationViewModel });
         }

         [HttpGetAttribute]
         [RouteAttribute("PageSetting/{id}")]
         public IActionResult GetPageSettingById(long id)
         {
             PageConfigurationViewModel pageConfigurationViewModel = _client_ConfigurationRepository.GetPagesConfigurationById(id);
             if (pageConfigurationViewModel == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = pageConfigurationViewModel });
         }

         [HttpGetAttribute]
         [RouteAttribute("ClientPageViewList/{id}")]
         public IActionResult GetMenuSettingByApplicationIdActionResult(long id)
         {
            IEnumerable<MenuConfigurationViewModel> menuConfigurationViewModel = _client_ConfigurationRepository.GetPagesListByApplicationId(id);
             if (menuConfigurationViewModel == null)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
            }
            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = menuConfigurationViewModel });
         }
    }
}  