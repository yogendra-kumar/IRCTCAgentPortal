using System;
using Microsoft.AspNetCore.Mvc;
using Mpower.Rail.Data.IRepository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Mpower.Rail.Data;
using Microsoft.AspNetCore.Authorization;
using Mpower.Rail.Data.Repository;
using System.Collections.Generic;
using Mpower.Rail.Api.Models;
using Mpower.Rail.Model.Rail;
using Mpower.Rail.Model.Application;

namespace Mpower.Rail.Api
{
    [AllowAnonymous]
    [Route("Mpower/Rail/Configuration")]
    public class ConfigurationController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private IConfigurationRepository _configRepository;
        private IApplicationErrorRepository _errorRepository;
        public ConfigurationController(DbContextOptions<ApplicationDbContext> applicationDBContext)
        {
            _applicationDbContext = new ApplicationDbContext(applicationDBContext);
            _configRepository = new ConfigurationRepository(_applicationDbContext);
            _errorRepository = new ApplicationErrorsRepository(_applicationDbContext);
        }

        /// <summary>
        /// This Api will provide Configuration List
        /// </summary>
        /// <param name="applicationId">it accept application id as query string</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpGetAttribute]
        [RouteAttribute("GetConfiguration/{applicationId}")]
        public IActionResult Get(string applicationId)
        {
            try
            {
                List<Configuration> configuration = _configRepository.FindBy(m => m.applicationID == applicationId).ToList();
                if (configuration.Count() == 0)
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = configuration });
                }
            }
            catch (Exception ex)
            {
                _errorRepository.
                    Add(new Application_Errors
                    {
                        applicationID = 1,
                        errorDescription = ex.ToString(),
                        errorType = "Exception",
                        logDate = System.DateTime.Now,
                        pageID = 0,
                        Source = "Mpower/Rail/Configuration/GetConfiguration"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }

        }
    }
}