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
using Mpower.Rail.Data.Repository.Master;
using Mpower.Rail.Processor.Master;
using Mpower.Rail.Model.Rail.Master;

namespace Mpower.Rail.Api
{
    [AllowAnonymous]
    [Route("Mpower/Rail/Master")]
    public class MasterController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        //private IIDCardTypeRepository _idCardTypeRepository;
        private IApplicationErrorRepository _errorRepository;
        public MasterController(DbContextOptions<ApplicationDbContext> applicationDBContext)
        {
            _applicationDbContext = new ApplicationDbContext(applicationDBContext);
            //_idCardTypeRepository = new IDCardTypeRepository(_applicationDbContext);
            _errorRepository = new ApplicationErrorsRepository(_applicationDbContext);
        }

        #region ID Card Method(s)
        
        /// <summary>
        //  This Method is used to fetch the IDCardTypes. 
        /// </summary>        
        /// <returns>this will return the list of IDCardTypes</returns>
        [HttpGetAttribute]
        [RouteAttribute("GetIDCardType")]
        public IActionResult GetIDCardType()
        {
            try
            {
                using(IIDCardTypeProcessor idCardProcessor=new IDCardTypeProcessor(_applicationDbContext))
                {
                    List<IDCardType> idCardTypes = idCardProcessor.GetIDCardType();
                    if (idCardTypes.Count() == 0)
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
                    }
                    else
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = idCardTypes });
                    }
                }
            }
            catch (Exception ex)
            {
                _errorRepository.
                    Add(new Application_Errors
                    {
                        applicationID = 1,
                        errorDescription = ex.StackTrace,
                        errorType = "Exception",
                        logDate = System.DateTime.Now,
                        pageID = 0,
                        Source = "Mpower/Rail/Master/GetIDCardType"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }

        }
        
        #endregion

        #region Berth Types Method(s)
        
        /// <summary>
        //  This Method is used to fetch the BerthTypes. 
        /// </summary>        
        /// <returns>this will return the list of BerthTypes</returns>
        [HttpGetAttribute]
        [RouteAttribute("GetBerthType")]
        public IActionResult GetBerthType()
        {
            try
            {
                using(IBerthTypeProcessor berthTypeProcessor=new BerthTypeProcessor(_applicationDbContext))
                {
                    List<BerthType> berthTypes = berthTypeProcessor.GetBerthType();
                    if (berthTypes.Count() == 0)
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "Result not found", Status = "failed" });
                    }
                    else
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = berthTypes });
                    }
                }
            }
            catch (Exception ex)
            {
                _errorRepository.
                    Add(new Application_Errors
                    {
                        applicationID = 1,
                        errorDescription = ex.StackTrace,
                        errorType = "Exception",
                        logDate = System.DateTime.Now,
                        pageID = 0,
                        Source = "Mpower/Rail/Master/GetBerthType"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }

        }
        
        #endregion


    }
}