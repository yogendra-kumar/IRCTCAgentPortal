using System;
using Microsoft.AspNetCore.Mvc;
using Mpower.Rail.Data;
using Microsoft.EntityFrameworkCore;
using Mpower.Rail.Api.Models;
using Mpower.Rail.Model.Request;
using Mpower.Rail.Model.Rail;
using Microsoft.AspNetCore.Authorization;
using Mpower.Rail.Processor.Travel;
using System.Collections.Generic;
using Mpower.Rail.Model.Application;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;
namespace Mpower.Rail.Api
{
    [AllowAnonymous]
    [Route("Mpower/Rail/Passenger")]
    public class PassengerController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private IApplicationErrorRepository _errorRepository;

        public PassengerController(DbContextOptions<ApplicationDbContext> applicationDBContext)
        {
            _applicationDbContext = new ApplicationDbContext(applicationDBContext);
            _errorRepository = new ApplicationErrorsRepository(_applicationDbContext);
        }


        /// <summary>
        /// This API will Create New Passenger 
        /// </summary>
        /// <param name="req"> req is an object type of Passenger class </param>
        /// <returns>This will return Passengers object</returns>
        [HttpPostAttribute]
        [RouteAttribute("CreatePassenger")]
        public IActionResult CreatePassenger([FromBodyAttribute]Passenger req)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (PassengerProcessor processor = new PassengerProcessor(_applicationDbContext))
                    {
                        Passengers _passenger = processor.CreatePassenger(req);
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = _passenger });
                    }
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
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
                        Source = "Mpower/Rail/Passenger/CreatePassenger"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }


        /// <summary>
        /// This API will Update Existing Passenger
        /// </summary>
        /// <param name="req">req is an object type of Passenger class</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpPostAttribute]
        [RouteAttribute("UpdatePassenger")]
        public IActionResult UpdatePassenger([FromBodyAttribute]Passenger req)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (PassengerProcessor processor = new PassengerProcessor(_applicationDbContext))
                    {
                        Passengers _passenger = processor.UpdatePassenger(req);
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = _passenger });
                    }
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
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
                        Source = "Mpower/Rail/Passenger/UpdatePassenger"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }

        /// <summary>
        /// This API will Delete Existing Passenger 
        /// </summary>
        /// <param name="passengerId">It will accept prssengerId in request as Querystring</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpGetAttribute]
        [RouteAttribute("DeletePassenger/{passengerId}")]
        public IActionResult UpdatePassenger(long passengerId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (PassengerProcessor processor = new PassengerProcessor(_applicationDbContext))
                    {
                        bool status = processor.DeletePassenger(passengerId);
                        if (status)
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = "Passenger Deleted." });
                        else
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed", ResponseResult = "Invalid Passenger Id" });
                    }
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
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
                        Source = "Mpower/Rail/Passenger/DeletePassenger"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }


        /// <summary>
        /// This api will provide the all passenger list of that RO account
        /// </summary>
        /// <param name="loginAccount"></param>
        /// <returns></returns>
        [HttpGetAttribute]
        [RouteAttribute("GetAllPassenger/{loginAccount}")]
        public IActionResult GetAllPassenger(string loginAccount)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginAccount))
                {
                    using (PassengerProcessor processor = new PassengerProcessor(_applicationDbContext))
                    {
                        List<Passengers> lstpassengers = processor.GetAllPassenger(loginAccount);
                        if (lstpassengers.Count > 0)
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = lstpassengers });
                        }
                        else
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Result not found", Status = "failed" });
                        }
                    }
                }
                else
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
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
                        Source = "Mpower/Rail/Passenger/GetAllPassenger"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }


    }

}