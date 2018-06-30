using System;
using Microsoft.AspNetCore.Mvc;
using Mpower.Rail.Data;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Mpower.Rail.Api.Models;
using Mpower.Rail.Model.Request;
using Mpower.Rail.Processor.Travel;
//using Mpower.Rail.Model.Rail;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Mpower.Rail.Model.Application;

namespace Mpower.Rail.Api
{
    [AllowAnonymous]
    [Route("Mpower/Rail/Journey")]
    public class TravelListController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private IApplicationErrorRepository _errorRepository;
        public TravelListController(DbContextOptions<ApplicationDbContext> applicationDBContext)
        {
            _applicationDbContext = new ApplicationDbContext(applicationDBContext);
            _errorRepository = new ApplicationErrorsRepository(_applicationDbContext);
        }

        /// <summary>
        /// This API will create Travel List
        /// </summary>
        /// <param name="req"></param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpPostAttribute]
        [RouteAttribute("CreateTravelList")]
        public IActionResult CreatePaggengerList([FromBodyAttribute]TravelList req)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (TravelListProcessor processor = new TravelListProcessor(_applicationDbContext))
                    {
                        Mpower.Rail.Model.Rail.TravelList travellst = processor.CreateTravelList(req);
                        if (travellst != null)
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = travellst });
                        else
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
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
                        Source = "Mpower/Rail/Journey/CreateTravelList"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }

        /// <summary>
        /// This API will Delete existing travel list.
        /// </summary>
        /// <param name="travelListId">it will accept travel list id as Query string.</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpGetAttribute]
        [RouteAttribute("DeleteTravelList/{travelListId}")]
        public IActionResult DeleteTravelList(long travelListId)
        {
            try
            {
                if (travelListId > 0)
                {
                    using (TravelListProcessor processor = new TravelListProcessor(_applicationDbContext))
                    {
                        bool isdeleted = processor.DeleteTravelList(travelListId);
                        if (isdeleted)
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = "Travel list deleted." });
                        }
                        else
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed", ResponseResult = "Invalid Travel List Id" });
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
                        Source = "Mpower/Rail/Journey/DeleteTravelList"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }


        /// <summary>
        /// This API will return travel list Details.
        /// </summary>
        /// <param name="travelListId">it will accept travel list id as Query string.</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>

        [HttpGetAttribute]
        [RouteAttribute("GetTravelList/{travelListId}")]
        public IActionResult GetTravelList(long travelListId)
        {
            try
            {
                if (travelListId > 0)
                {
                    using (TravelListProcessor processor = new TravelListProcessor(_applicationDbContext))
                    {
                        bool isexists = processor.CheckTravelList(travelListId);
                        if (isexists)
                        {
                            object obj = processor.GetTravelList(travelListId);
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = obj });
                        }
                        else
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Result not found.", Status = "failed" });
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
                        Source = "Mpower/Rail/Journey/GetTravelList"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }


        /// <summary>
        /// this method provide all travellist of given loginaccount id
        /// </summary>
        /// <param name="loginAccount">accept loginaccount as a query string</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpGetAttribute]
        [RouteAttribute("GetAllTravelList/{loginAccount}")]
        public IActionResult GetAllTravelList(string loginAccount)
        {
            try
            {
                if (!string.IsNullOrEmpty(loginAccount))
                {
                    using (TravelListProcessor processor = new TravelListProcessor(_applicationDbContext))
                    {
                        List<Mpower.Rail.Model.Rail.TravelList> lst = processor.GetAllTravelList(loginAccount);
                        if (lst.Count > 0)
                        {
                            return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = lst });
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
                        Source = "Mpower/Rail/Journey/GetAllTravelList"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }
    }

}