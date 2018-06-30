using System;
using Microsoft.AspNetCore.Mvc;
using Mpower.Rail.Data;
using Mpower.Rail.Data.Repository;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Mpower.Rail.Model.Application;
using Mpower.Rail.Processor.Master;

namespace Mpower.Rail.Api
{
     [AllowAnonymous]
    [Route("Mpower/Rail/Irctc/Station")]
    public class TrainController:Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private IApplicationErrorRepository _errorRepository;


        public TrainController(DbContextOptions<ApplicationDbContext> applicationDBContext)
        {
            _applicationDbContext=new ApplicationDbContext(applicationDBContext);
             _errorRepository = new ApplicationErrorsRepository(_applicationDbContext);
        }

        /// <summary>
        /// This API will return Station List
        /// </summary>
        /// <param name="key">station code</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>

        [HttpGet]
         [RouteAttribute("List/{key}")]
        public IActionResult StationsList(string key)
        {
            try
            {
                key=key.ToUpper();
                using (IStationCacheProcessor _stationCacheProcessor = new StationCacheProcessor(_applicationDbContext))
                {
                    var stationList = _stationCacheProcessor.GetStationsList(key);
                    if (stationList.Count <=0)
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Record not found", Status = "failed" });
                    }
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = stationList });
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
                        Source = "Mpower/Rail/Train/Station/List/{key}"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }
    
        /// <summary>
        /// This API will return Station List
        /// </summary>
        /// <param name=""></param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpGet]
         [RouteAttribute("List")]
        public IActionResult StationsList()
        {
            try
            {
                using (IStationCacheProcessor _stationCacheProcessor = new StationCacheProcessor(_applicationDbContext))
                {
                    var stationList = _stationCacheProcessor.GetStationsList();
                    if (stationList.Count <=0)
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Record not found", Status = "failed" });
                    }
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = stationList });
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
                        Source = "Mpower/Rail/Train/Station/List"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }

         /// <summary>
        /// This API will return Station List
        /// </summary>
        /// <param name="key">station code</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>

         [HttpPostAttribute]
         [RouteAttribute("List/Details")]
        public IActionResult StationsList([FromBodyAttribute] string[] StationCodes)
        {
            try
            {
                if(StationCodes.Length <=0)
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalide Model", Status = "failed" });
                }
                using (IStationCacheProcessor _stationCacheProcessor = new StationCacheProcessor(_applicationDbContext))
                {
                    var stationList = _stationCacheProcessor.GetStationsList(StationCodes);
                    if (stationList.Count <=0)
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Record not found", Status = "failed" });
                    }
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = stationList });
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
                        Source = "Mpower/Rail/Train/Station/List/StationCodes"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }
    }
}