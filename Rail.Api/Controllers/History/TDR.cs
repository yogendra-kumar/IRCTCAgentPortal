using System;
using Microsoft.AspNetCore.Mvc;
using Mpower.Rail.Data;
using Mpower.Rail.Data.Repository;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Processor.Ticket;
using Mpower.Rail.Api.Models;
using Mpower.Rail.Model.ViewModel.Filters;
using Mpower.Rail.Model.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Mpower.Rail.Processor.Application;


namespace Mpower.Rail.Api
{
      [AllowAnonymous]
     [Route("Mpower/Rail/History/TDR")]
 
    public class TDRController:Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private IApplicationErrorRepository _errorRepository;

        public TDRController(DbContextOptions<ApplicationDbContext> applicationDBContext)
        {
            _applicationDbContext=new ApplicationDbContext(applicationDBContext);
             _errorRepository = new ApplicationErrorsRepository(_applicationDbContext);
        }

        
         /// <summary>
        /// This API will return TDR details for RoId
        /// </summary>
        /// <param name="RoId">id of Ro</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>

        [HttpGetAttribute]
         [RouteAttribute("Details")]
        public IActionResult TdrDetails(string RoId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(RoId.ToString()))
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }
                using (ITdrProcessor _tdrProcessor = new TdrProcessor(_applicationDbContext))
                {
                    var tdrs = _tdrProcessor.GetTdrDetailList(RoId.ToString());
                    if(tdrs == null)
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Record not found", Status = "failed" });
                    }
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = tdrs });
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
                        Source = "Mpower/Rail/History/TDR/Details"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }

        /// <summary>
        /// This API will return ticket order details
        /// </summary>
        /// <param name="RoId">id of ticket order class</param>
        /// <returns>This Api will rerurn [HttpGetAttribute]
        

        [HttpGetAttribute]
        //public IActionResult Get([FromBody] OperatorData operatorData)
         [RouteAttribute("Reasons")]
        public IActionResult GetTdrReasons()
        {
            try
            {
                using (ITdrProcessor _tdrProcessor = new TdrProcessor(_applicationDbContext))
                {
                    var reasonsList = _tdrProcessor.GetTdrReasonsList();
                    if(reasonsList.Count ==0)
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Record not found", Status = "failed" });
                    }
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = reasonsList });
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
                        Source = "Mpower/Rail/History/TDR/Reasons"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }          
    }    
}