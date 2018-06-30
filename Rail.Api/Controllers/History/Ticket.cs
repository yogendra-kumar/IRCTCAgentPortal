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
    [Route("Mpower/Rail/History/Ticket")]
    public class TicketController:Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private IApplicationErrorRepository _errorRepository;


        public TicketController(DbContextOptions<ApplicationDbContext> applicationDBContext)
        {
            _applicationDbContext=new ApplicationDbContext(applicationDBContext);
             _errorRepository = new ApplicationErrorsRepository(_applicationDbContext);
        }

        /// <summary>
        /// This API will return ticket order details
        /// </summary>
        /// <param name="ticketOrderId">id of ticket order class</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>

        [HttpGet]
         [RouteAttribute("Details")]
        public IActionResult TicketOrderDetails(Int64 ticketOrderId)
        {
             try
            {
                if (ticketOrderId == 0)
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }
                using (ITicketProcessor _ticketProcessor = new TicketProcessor(_applicationDbContext))
                {
                    var ticketOrder = _ticketProcessor.GetTicketDetail(ticketOrderId);
                    if(ticketOrder == null)
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Record not found", Status = "failed" });
                    }
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = ticketOrder });
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
                        Source = "Mpower/Rail/History/Ticket/Details"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }

         /// <summary>
        /// This API will return ticket order list
        /// </summary>
        /// <param name="BookedTicketFilter">bookedTicketFilter is an object type of bookedTicketFilter class</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
       
        [HttpPostAttribute]
        [RouteAttribute("List")]
        public IActionResult BookedTicketList([FromBodyAttribute] BookedTicketFilter filter)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }
                using (IApplicationProcessor _processor = new ApplicationProcessor(_applicationDbContext))
                {
                    string pagesSize = _processor.GetApplicationSettingByKey("displayList_Count");
                    if (!string.IsNullOrEmpty(pagesSize) || int.Parse(Convert.ToString(pagesSize)) <= 0)
                    {
                        filter.pages = int.Parse(pagesSize);
                    }
                    else
                    {
                        _errorRepository.Add(new Application_Errors
                        {
                            applicationID = 1,
                            errorDescription = "Page size value not found.",
                            errorType = "Log",
                            logDate = System.DateTime.Now,
                            pageID = 0,
                            Source = "Mpower/Rail/History/Ticket/List"
                        });
                        _errorRepository.Commit();
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1013", ResponseMessage = "Page Size is not found!", Status = "failed" });
                    }
                }
                using (ITicketProcessor _ticketProcessor = new TicketProcessor(_applicationDbContext))
                {
                    var list = _ticketProcessor.BookedTicketList(filter);
                    if (list == null || list.Count == 0)
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Record not found", Status = "failed" });
                    }
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = list });
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
                        Source = "Mpower/Rail/History/Ticket/List"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        }


         /// <summary>
        /// This API will receive ticket passenger list
        /// </summary>
        /// <param name="ticketOrderId">This Api Accept ticketOrderId As a query string</param>
        /// <returns>This Api will rerurn object of Application_ResponseWrapper class</returns>
        [HttpGetAttribute]
        [RouteAttribute("PassengerList")]
        public IActionResult PassengerList(Int64 ticketOrderId)
        {
            try
            {
                if (ticketOrderId == 0)
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }
                using (ITicketProcessor _ticketProcessor = new TicketProcessor(_applicationDbContext))
                {
                    var passengerList = _ticketProcessor.PassengerList(ticketOrderId);
                    if(passengerList == null || passengerList.Count==0)
                    {
                        return Ok(new Application_ResponseWrapper() { ResponseCode = "1004", ResponseMessage = "Record not found", Status = "failed" });
                    }
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = passengerList });
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
                        Source = "Mpower/Rail/History/Ticket/PassengerList"
                    });
                _errorRepository.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "An error has occured", Status = "failed" });
            }
        } 

            
    }    
}