using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mpower.Rail.Api.Models;
using Mpower.Rail.Api.NGET.Request;
using Mpower.Rail.NGETSystem.Models.Response;
using Mpower.Rail.NGETSystem.Processor.BookingServices;
using Newtonsoft.Json;
using Mpower.Rail.Data.Repository;
using Mpower.Rail.Model.Rail;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Processor.Application;
using Mpower.Rail.Data;
using Microsoft.EntityFrameworkCore;
using Mpower.Rail.Model.Ticket;
using Mpower.Rail.NGETSystem.Models.Request;
using Mpower.Rail.Data.Repository.Master;
using System.Collections.Generic;

namespace Mpower.Rail.Api.Controllers
{
    [AllowAnonymous]
    [Route("Mpower/Rail/Booking")]
    public class BookingController : Controller
    {
        IBooking _booking;
        ILogProcessor _log;
        private ApplicationDbContext _applicationDbContext;
        private IConfigurationRepository _configuration;
        private string _irctcUrl;
        private string _irctc_userName;
        private string _irctc_password;
        private ITicketOrdersRepository _order;
        private string _authInfo;
        public BookingController(IBooking booking, DbContextOptions<ApplicationDbContext> applicationDBContext)
        {
            _booking = booking;
            _applicationDbContext = new ApplicationDbContext(applicationDBContext);
            _log = new LogProcessor(_applicationDbContext);
            _configuration = new ConfigurationRepository(_applicationDbContext);
            _irctcUrl = _configuration.FindBy(x => x.key == "IRCTC_ApiURL").FirstOrDefault().value;
            _irctc_userName = _configuration.FindBy(x => x.key == "IRCTC_Username").FirstOrDefault().value;
            _irctc_password = _configuration.FindBy(x => x.key == "IRCTC_Password").FirstOrDefault().value;
            _authInfo = _irctc_userName + ":" + _irctc_password;
            _order = new TicketOrdersRepository(_applicationDbContext);

        }


        /// <summary>
        /// it returns non transactional ticket fare information from IRCTC
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost]
        [RouteAttribute("TrainFareEnquiry")]
        public IActionResult Fetch_NonTransact_TrainFare([FromBody]TrainFareRequest Request) //parameter will be as like /N
        {
            object response = "";
            ErrorResponse error = new ErrorResponse();
            if (!ModelState.IsValid || string.IsNullOrEmpty(Request.trainNo) || string.IsNullOrEmpty(Request.journyDate) || string.IsNullOrEmpty(Request.trainFrom) || string.IsNullOrEmpty(Request.trainTo) || string.IsNullOrEmpty(Request.trainClass) || string.IsNullOrEmpty(Request.quota))
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed", ResponseResult = "" });
            if (string.IsNullOrEmpty(_irctcUrl) || string.IsNullOrEmpty(_irctc_userName) || string.IsNullOrEmpty(_irctc_password))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "No IRCTC configuration found", Status = "failed" });
            }
            _irctcUrl = _irctcUrl + "taenqservices/avlFareenquiry/" + Request.trainNo + "/" + Request.journyDate + "/" + Request.trainFrom + "/" + Request.trainTo + "/" + Request.trainClass + "/" + Request.quota + "/N";
            try
            {
                CommunicationLog("OxiRail", "availableFareenquiry OUT->>" + JsonConvert.SerializeObject(Request.request).ToString(), Request.userSession);
                switch (Convert.ToInt32(Request.request.enquiryType))
                {
                    case 1:
                        {
                            FareEnquiry_Response_Type1 Response = new FareEnquiry_Response_Type1();
                            response = _booking.FindFareEnquiry(_irctcUrl, Request.request, Request.userSession, _authInfo);
                            Response = (FareEnquiry_Response_Type1)JsonConvert.DeserializeObject(response.ToString(), typeof(FareEnquiry_Response_Type1));
                            if (Response.bkgCfg != null)
                            {
                                error.Dispose();
                                CommunicationLog("OxiRail", "availableFareenquiry IN->>" + JsonConvert.SerializeObject(response).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = Response });
                            }
                            else
                            {
                                error = (ErrorResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(ErrorResponse));
                                CommunicationLog("OxiRail", "availableFareenquiry IN->>" + JsonConvert.SerializeObject(error).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = error.errorMessage, Status = "Error", ResponseResult = "" });
                            }
                        }
                    case 2:
                        {
                            FareEnquiry_Response_Type2 Response = new FareEnquiry_Response_Type2();
                            response = _booking.FindFareEnquiry(_irctcUrl, Request.request, Request.userSession, _authInfo);
                            Response = (FareEnquiry_Response_Type2)JsonConvert.DeserializeObject(response.ToString(), typeof(FareEnquiry_Response_Type2));
                            if (Response.bkgCfg != null)
                            {
                                error.Dispose();
                                CommunicationLog("OxiRail", "availableFareenquiry IN->>" + JsonConvert.SerializeObject(Response).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = Response });
                            }
                            else
                            {
                                error = (ErrorResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(ErrorResponse));
                                CommunicationLog("OxiRail", "availableFareenquiry IN->>" + JsonConvert.SerializeObject(error).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = error.errorMessage, Status = "Error", ResponseResult = "" });
                            }
                        }
                    default:
                        {
                            FareEnquiry_Response_Type3 Response = new FareEnquiry_Response_Type3();
                            response = _booking.FindFareEnquiry(_irctcUrl, Request.request, Request.userSession, _authInfo);
                            Response = (FareEnquiry_Response_Type3)JsonConvert.DeserializeObject(response.ToString(), typeof(FareEnquiry_Response_Type3));
                            if (Response.bkgCfg != null)
                            {
                                error.Dispose();
                                CommunicationLog("OxiRail", "availableFareenquiry IN->>" + JsonConvert.SerializeObject(Response).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = Response });
                            }
                            else
                            {
                                error = (ErrorResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(ErrorResponse));
                                CommunicationLog("OxiRail", "availableFareenquiry IN->>" + JsonConvert.SerializeObject(error).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = error.errorMessage, Status = "Error", ResponseResult = "" });
                            }
                        }
                }
            }
            catch (Exception e)
            {
                CommunicationLog("OxiRail", e.ToString(), Request.userSession);
                error.Dispose();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Api_Error or IRCTC Service Down", Status = "Error", ResponseResult = e });
            }
        }

        /// <summary>
        /// Transactional TrainFareEnquiry it is call when passenger list is given for ticket booking
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost]
        [RouteAttribute("PayTrainFareEnquiry")]
        public IActionResult Transactional_TrainFare_Enquiry([FromBody]TransactionalTrainFareRequest Request) //parameter will be as like /N
        {
            object response = "";
            ErrorResponse error = new ErrorResponse();
            if (!ModelState.IsValid || string.IsNullOrEmpty(Request.trainNo) || string.IsNullOrEmpty(Request.journyDate) || string.IsNullOrEmpty(Request.trainFrom) || string.IsNullOrEmpty(Request.trainTo) || string.IsNullOrEmpty(Request.trainClass) || string.IsNullOrEmpty(Request.quota))
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed", ResponseResult = "" });
            if (string.IsNullOrEmpty(_irctcUrl) || string.IsNullOrEmpty(_irctc_userName) || string.IsNullOrEmpty(_irctc_password))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "No IRCTC configuration found", Status = "failed" });
            }
            _irctcUrl = _irctcUrl + "taenqservices/avlFareenquiry/" + Request.trainNo + "/" + Request.journyDate + "/" + Request.trainFrom + "/" + Request.trainTo + "/" + Request.trainClass + "/" + Request.quota + "/Y";
            try
            {
                CommunicationLog("OxiRail", "availablePayFareenquiry OUT->>" + JsonConvert.SerializeObject(Request).ToString(), Request.userSession);
                switch (Convert.ToInt32(Request.request.enquiryType))
                {
                    case 1:
                        {
                            FareEnquiry_Response_Type1 Response = new FareEnquiry_Response_Type1();
                            response = _booking.FindTransactionalFareEnquiry(_irctcUrl, Request.request, Request.userSession, _authInfo);
                            Response = (FareEnquiry_Response_Type1)JsonConvert.DeserializeObject(response.ToString(), typeof(FareEnquiry_Response_Type1));
                            if (Response.bkgCfg != null)
                            {
                                error.Dispose();

                                CommunicationLog("OxiRail", "availablePayFareenquiry IN->>" + JsonConvert.SerializeObject(Response).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = Response });
                            }
                            else
                            {
                                error = (ErrorResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(ErrorResponse));
                                CommunicationLog("OxiRail", "availableFareenquiry IN->>" + JsonConvert.SerializeObject(error).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = error.errorMessage, Status = "Error", ResponseResult = "" });
                            }
                        }
                    case 2:
                        {
                            FareEnquiry_Response_Type2 Response = new FareEnquiry_Response_Type2();
                            response = _booking.FindFareEnquiry(_irctcUrl, Request.request, Request.userSession, _authInfo);
                            Response = (FareEnquiry_Response_Type2)JsonConvert.DeserializeObject(response.ToString(), typeof(FareEnquiry_Response_Type2));
                            if (Response.bkgCfg != null)
                            {
                                error.Dispose();
                                CommunicationLog("OxiRail", "availablePayFareenquiry IN->>" + JsonConvert.SerializeObject(Response).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = Response });
                            }
                            else
                            {
                                error = (ErrorResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(ErrorResponse));
                                CommunicationLog("OxiRail", "availableFareenquiry IN->>" + JsonConvert.SerializeObject(error).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = error.errorMessage, Status = "Error", ResponseResult = "" });
                            }
                        }
                    default:
                        {
                            long orderId = 0;
                            FareEnquiry_Response_Type3 Response = new FareEnquiry_Response_Type3();
                            response = _booking.FindFareEnquiry(_irctcUrl, Request.request, Request.userSession, _authInfo);
                            Response = (FareEnquiry_Response_Type3)JsonConvert.DeserializeObject(response.ToString(), typeof(FareEnquiry_Response_Type3));
                            if (Response.bkgCfg != null)
                            {
                                error.Dispose();
                                if (Request.payment_Flag == "1")
                                {
                                    var myObject = (passengerList)JsonConvert.DeserializeObject(Request.request.passengerList.ToString(), typeof(passengerList));
                                    if (myObject == null)
                                    {
                                        passengerList[] psngerArray = (passengerList[])Request.request.passengerList;
                                        passengerList p = psngerArray[0];
                                        orderId = Order(p, Response, Request, psngerArray);

                                    }
                                    else
                                    {
                                        orderId = Order(myObject, Response, Request, null);
                                    }
                                }
                                CommunicationLog("OxiRail", "availablePayFareenquiry IN->>" + JsonConvert.SerializeObject(Response).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success with OrderId:(" + orderId + ")", Status = "success", ResponseResult = Response });
                            }
                            else
                            {
                                error = (ErrorResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(ErrorResponse));
                                CommunicationLog("OxiRail", "availableFareenquiry IN->>" + JsonConvert.SerializeObject(error).ToString(), Request.userSession);
                                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = error.errorMessage, Status = "Error", ResponseResult = "" });
                            }
                        }
                }
            }
            catch (Exception e)
            {
                error.Dispose();
                CommunicationLog("OxiRail", "availablePayFareenquiry IN->>" + e.ToString(), Request.userSession);
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Api_Error  or IRCTC Service Down", Status = "Error", ResponseResult = e });
            }
        }


        /// <summary>
        /// To get Enquiry from Indian Railway about train between stations.
        /// </summary>
        /// <param name="Request">object of type trainBtwnStnsRequest class</param>
        /// <returns></returns>
        [HttpPostAttribute]
        [RouteAttribute("TrainBtwnStations")]
        public IActionResult TrainBtwnStations([FromBodyAttribute]TrainBtwnStnsRequest Request)
        {
            ErrorResponse error = new ErrorResponse();
            if (!ModelState.IsValid || string.IsNullOrEmpty(Request.request.enquiryForDate) || string.IsNullOrEmpty(Request.request.trainTo) || string.IsNullOrEmpty(Request.request.trainFrom))
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed", ResponseResult = "" });
            if (string.IsNullOrEmpty(_irctcUrl) || string.IsNullOrEmpty(_irctc_userName) || string.IsNullOrEmpty(_irctc_password))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "No IRCTC configuration found", Status = "failed" });
            }
            _irctcUrl = _irctcUrl + "taenqservices/tatwnstns";
            try
            {
                CommunicationLog("OxiRail", "trainBtwnStation OUT->>" + JsonConvert.SerializeObject(Request).ToString(), Request.userSession);
                TrainBetweenStationResponse Response = new TrainBetweenStationResponse();
                var response = _booking.FindTrainBtwnStation(_irctcUrl, Request.request, Request.userSession, _authInfo);
                try{
                Response = (TrainBetweenStationResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(TrainBetweenStationResponse));
                }
                catch(Exception e)
                {
                Response = (TrainBetweenStationResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(TrainBetweenStationResponse));
                }
                if (Response.quotaList != null)
                {
                    CommunicationLog("OxiRail", "trainBtwnStation IN->>" + JsonConvert.SerializeObject(Response).ToString(), Request.userSession);
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = Response });
                }
                else
                {
                    error = (ErrorResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(ErrorResponse));
                    CommunicationLog("OxiRail", "trainBtwnStation IN->>" + JsonConvert.SerializeObject(error).ToString(), Request.userSession);
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = error.errorMessage, Status = "Error", ResponseResult = "" });
                }
            }
            catch (Exception e)
            {
                CommunicationLog("OxiRail", e.ToString(), Request.userSession);
                error.Dispose();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Api_Error or IRCTC Service Down", Status = "Error", ResponseResult = e });
            }
        }

        /// <summary>
        /// Post Api method to cancel booked ticket
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPostAttribute]
        [RouteAttribute("CancelTicket")]
        public IActionResult CancelTicket([FromBodyAttribute]cancelTicketRequest Request)
        {
            ErrorResponseOnCencel error = new ErrorResponseOnCencel();
            if (!ModelState.IsValid || string.IsNullOrEmpty(Request.request.agentTxnId) || string.IsNullOrEmpty(Request.request.psgnToken) || string.IsNullOrEmpty(Request.request.reservationId))
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed", ResponseResult = "" });
            if (string.IsNullOrEmpty(_irctcUrl) || string.IsNullOrEmpty(_irctc_userName) || string.IsNullOrEmpty(_irctc_password))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "No IRCTC configuration found", Status = "failed" });
            }
            _irctcUrl = _irctcUrl + "tatktservices/cancel";
            try
            {
                CommunicationLog("OxiRail", "cancelBookedTicket OUT->>" + JsonConvert.SerializeObject(Request).ToString(), Request.userSession);
                CancelTicketResponse Response = new CancelTicketResponse();
                var response = _booking.CancelTicket(_irctcUrl, Request.request, Request.userSession, _authInfo);
                Response = (CancelTicketResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(CancelTicketResponse));
                if (Response.cancellationId != null)
                {
                    CommunicationLog("OxiRail", "cancelBookedTicket IN->>" + JsonConvert.SerializeObject(Response).ToString(), Request.userSession);
                    ITicketCancellationRepository objCan = new TicketCancellationRepository(_applicationDbContext);
                    int[] psngrIndex = common.FindcharIndexInString(Request.request.psgnToken, 'Y');
                    IBookedTicketsRepository objBooked = new BookedTicketsRepository(_applicationDbContext);
                    ITicketPassengersRepository psnger = new TicketPassengersRepository(_applicationDbContext);
                    ICancelledTicketPassengersRepository PsngrLog = new CancelledTicketPassengersRepository(_applicationDbContext);
                    var BookedTicket = objBooked.FindBy(x => x.pnrNumber == Response.pnrNo).SingleOrDefault();
                    List<TicketPassengers> PsngrList = psnger.FindBy(x => x.ticketOrderId == BookedTicket.ticketOrderId).OrderBy(x => x.Id).ToList<TicketPassengers>();
                    TicketCancellations objTicketCancel = objCan.AddCancelTicket(new TicketCancellations()
                    {
                        bookedTicketId = BookedTicket.Id,
                        noOfPassenger = Convert.ToInt32(Response.noOfPsgn),
                        ticketNumber = BookedTicket.ticketNumber,
                        status = true,
                        amountCollected = Convert.ToDecimal(Response.amountCollected),
                        refundedAmount = Convert.ToDecimal(Response.refundAmount),
                        cashDeducted = Convert.ToDecimal(Response.cashDeducted),
                        cashCollected = Convert.ToDecimal(Response.amountCollected),
                        amount = Convert.ToDecimal(Response.refundAmount),
                        paymentGateways = 1,
                        sessions = Request.userSession,
                        date = DateTime.Now,
                        cancelledDate = Response.cancelledDate
                    });
                    objCan.Commit();
                    foreach (int index in psngrIndex)
                    {
                        long psngrId = PsngrList[index].Id;// To be inserted in Cancelled ticket Passenger objTicketCancel
                        PsngrLog.Add(new CancelledTicketPassengers
                        {
                            ticketId = objTicketCancel.Id,
                            passengerId = psngrId

                        });
                    }
                    PsngrLog.Commit();
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = Response });
                }
                else
                {
                    error = (ErrorResponseOnCencel)JsonConvert.DeserializeObject(response.ToString(), typeof(ErrorResponseOnCencel));
                    CommunicationLog("OxiRail", "cancelBookedTicket IN->>" + JsonConvert.SerializeObject(error).ToString(), Request.userSession);
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = error.message, Status = "Error", ResponseResult = "" });
                }
            }
            catch (Exception e)
            {
                CommunicationLog("OxiRail", e.ToString(), Request.userSession);
                error.Dispose();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Api_Error", Status = "Error or IRCTC Service Down", ResponseResult = e });
            }
        }
        /// <summary>
        /// To Update Order Status in various phase of ticket booking
        /// </summary>
        /// <param name="Request"></param>
        /// <returns>TicketOrder model</returns>
        [HttpPostAttribute]
        [RouteAttribute("UpdateOrder")]
        public IActionResult UpdateOrder([FromBodyAttribute]TicketOrderUpdate Request)
        {
            if (!ModelState.IsValid || Request.OrderId == 0 || string.IsNullOrEmpty(Request.status))
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "failed", Status = "Error in Api while Update in OrderTicket ", ResponseResult = "Request is Invalid" });

            try
            {
                ITicketStatusRepository _status = new TicketStatusRepository(_applicationDbContext);
                var Status = _status.FindBy(x => x.fullName == Request.status).FirstOrDefault().Id;
                TicketOrders order = _order.FindBy(x => x.Id == Request.OrderId).SingleOrDefault();
                order.ticketStatus = Convert.ToInt32(Status);
                _order.Update(order);
                _order.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = order });
            }
            catch (Exception e)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "failed", Status = "Error in update", ResponseResult = e });
            }
        }

        /// <summary>
        /// To get booked ticket details
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPostAttribute]
        [RouteAttribute("GetBookedTicketDetail")]
        public IActionResult GetBookedTicketDetail([FromBodyAttribute]BookedTickets Request)
        {
            // GetTicketBookingDetail
            return Ok();
        }

        /// <summary>
        /// Used to Insert in booked ticket when ticket confirm msg is found from IRCTC Api
        /// </summary>
        /// <param name="Request"></param>
        /// <returns>Success msg(string type)</returns>
        [HttpPostAttribute]
        [RouteAttribute("Booked")]
        public IActionResult Booked([FromBodyAttribute]BookedTickets Request)
        {
            if (!ModelState.IsValid)
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "failed", Status = "Error in Api while insert in BookedTickets ", ResponseResult = "Request is Invalid" });
            try
            {
                IBookedTicketsRepository _BookedTicket = new BookedTicketsRepository(_applicationDbContext);
                _BookedTicket.Add(Request);
                _BookedTicket.Commit();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = "Booked Successfully" });
            }
            catch (Exception e)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "failed", Status = "Error in Api while insert in BookedTickets ", ResponseResult = e });
            }
        }

        /// <summary>
        /// it returns the information for boarding station reuest from IRCTC
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        [HttpPost]
        [RouteAttribute("ChangeBoardingStation")]
        public IActionResult ChangeBoardingStation([FromBody]ChangeBoardingRequest Request) //parameter will be as like /N
        {
            object response = "";
            BoardingPoint Response = null;
            ErrorResponse error = new ErrorResponse();
            if (!ModelState.IsValid || string.IsNullOrEmpty(Request.request.pnr) || string.IsNullOrEmpty(Request.request.boardingStation))
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed", ResponseResult = "" });
            if (string.IsNullOrEmpty(_irctcUrl) || string.IsNullOrEmpty(_irctc_userName) || string.IsNullOrEmpty(_irctc_password))
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "No IRCTC configuration found", Status = "failed" });
            }
            _irctcUrl = _irctcUrl + "tatktservices/changeBoardingPoint/";// + Request.request.pnr + "/" + Request.request.boardingStation;
            try
            {
                CommunicationLog("OxiRail", "ChangeBoardingStation OUT->>" + _irctcUrl + ":" + JsonConvert.SerializeObject(Request.request).ToString(), Request.userSession);

                response = _booking.BoardingPointChange(_irctcUrl, Request.request, Request.userSession, _authInfo);
                if (Convert.ToString(response).Contains("error"))
                {
                    Response = (ChanageBoardingPointErrResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(ChanageBoardingPointErrResponse));
                }
                else
                {
                    Response = (ChanageBoardingPointResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(ChanageBoardingPointResponse));

                }
                if (Response.pnr != null)
                {
                    error.Dispose();
                    CommunicationLog("OxiRail", "changeBoardingPoint IN->>" + JsonConvert.SerializeObject(response).ToString(), Request.userSession);
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = Response });
                }
                else
                {
                    error = (ErrorResponse)JsonConvert.DeserializeObject(response.ToString(), typeof(ErrorResponse));
                    CommunicationLog("OxiRail", "changeBoardingPoint IN->>" + JsonConvert.SerializeObject(error).ToString(), Request.userSession);
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = error.errorMessage, Status = "Error", ResponseResult = "" });
                }


            }
            catch (Exception e)
            {
                CommunicationLog("OxiRail", e.ToString(), Request.userSession);
                error.Dispose();
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Api_Error", Status = "Error", ResponseResult = e });
            }
        }

        /// <summary>
        /// Api communication log between oxirail Api and Indian railway api
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="Desc"></param>
        /// <param name="session"></param>
        private void CommunicationLog(string Type, string Desc, long session)
        {
            _log.InsertLogs(
                            new Logs
                            {
                                type = Type,
                                source = "IRCTCCommunication",
                                host = HttpContext.Connection.RemoteIpAddress.ToString(),
                                description = Desc,
                                session = Convert.ToString(session),
                                logDate = DateTime.Now
                            }
                                );
        }

        private long Order(passengerList p, FareEnquiry_Response_Type3 Response, TransactionalTrainFareRequest Request, passengerList[] psngerArray)
        {
            IIDCardTypeRepository _card = new IDCardTypeRepository(_applicationDbContext);
            IQuotaRepository _quota = new QuotaRepository(_applicationDbContext);
            IUserRegistrationRepository _user = new UserRegistrationRepository(_applicationDbContext);
            ITicketStatusRepository _status = new TicketStatusRepository(_applicationDbContext);
            IMerchantTypeRepository _merchantType = new MerchantTypeRepository(_applicationDbContext);
            var Status = _status.FindBy(x => x.fullName == "ORDERD").FirstOrDefault().Id;
            var QuotaId = _quota.FindBy(x => x.shortName == Request.quota).FirstOrDefault().Id;
            var Icardno = _card.FindBy(x => x.name == p.passengerCardType).FirstOrDefault().Id;
            var User = _user.FindBy(x => x.merchantAccount == Request.loginAccountNo).SingleOrDefault();
            var merchantType = _merchantType.FindBy(x => x.merchantId == User.merchantId).SingleOrDefault();
            // Dynamic Pgcharges
            decimal PgCharge = merchantType.conditionalAmount >= Convert.ToDecimal(Response.totalFare) ?decimal.Divide(decimal.Multiply(Convert.ToDecimal(Response.totalFare), Convert.ToDecimal(merchantType.pgChargeUnderCondition)),100)
            :decimal.Divide(decimal.Multiply(Convert.ToDecimal(Response.totalFare), Convert.ToDecimal(merchantType.pgChargeBeyondCondition)),100);
            
            TicketOrders Order = _order.AddTicketOrder(new TicketOrders()
            {
                sourceStation = Request.trainFrom,
                destStation = Request.trainTo,
                trainNumber = Request.trainNo,
                trainName = Response.trainName,
                Class = Request.trainClass,
                reserveUpto = Request.trainTo,
                bordingPoint = Request.request.boardingStation,
                ticketAmt = Convert.ToDecimal(Response.totalFare),
                totalAmt = Convert.ToDecimal(Response.totalFare) + merchantType.oxigenServiceCharge - merchantType.merchantCommission, //from database from --
                //merchantConfigTable(totalfare+oxigenServiceCharge-MerchantCommission)
                journeyDay = Convert.ToInt32(Request.journyDate.Substring(6)),
                journeyMonth = Convert.ToInt32(Request.journyDate.Substring(4, 2)),
                journeyYear = Convert.ToInt32(Request.journyDate.Substring(0, 4)),
                quota = Convert.ToInt32(QuotaId),                //from data base based on quota id     
                pmtGatewayName = merchantType.pgName,
                pmtCardType = Request.pmtCardType,
                accountNumber = Request.accountNumber,
                irctcServiceCharge = Convert.ToDecimal(Response.wpServiceCharge) + Convert.ToDecimal(Response.wpServiceTax),
                operatorCode = Request.operatorCode,
                mobileNo = Request.request.mobileNumber,
                idCardNumber = p.passengerCardNumber,
                idCardType = Convert.ToInt32(Icardno),              //will get by p.passengerCardType from database
                ticketStatus = Convert.ToInt32(Status),                     //ticketstatus from database
                paymentGateway = (long)(PgCharge),   //payment gateway charges from database in percent
                session = Request.userSession,
                bookingDate = DateTime.Now,
                oxigenServiceCharge = merchantType.oxigenServiceCharge,            //from database from merchantConfigTable
                email = Request.PassengerEmail,                        //from database using login account no
                loginAccountNo = Request.loginAccountNo,
                bookingSource = HttpContext.Connection.RemoteIpAddress.ToString(),
                reservationChoice = (Request.request.reservationChoice) == "FALSE" ? "N" : "Y",
                isTatkal = Request.isTatkal,
                ticketVerificationKey = Request.loginAccountNo + ":" + Request.trainNo + ":" + Request.trainFrom + ":" + Request.trainTo + ":" + Request.userSession + ":" + Request.journyDate + ":" + Request.request.mobileNumber,
                psgAddress = Request.psgAddress,
                irctcTxnNumber = Request.request.clientTransactionId
            });
            _order.Commit();
            if (psngerArray != null)
            {
                foreach (passengerList p1 in psngerArray)
                {
                    long NewpsngrId = addPassenger(Icardno, Order.Id, p1, Response, Request);
                }
            }
            else
            {
                long NewpsngrId = addPassenger(Icardno, Order.Id, p, Response, Request);
            }
            return Order.Id;
        }


        private long addPassenger(long Icardno, long ordrId, passengerList p, FareEnquiry_Response_Type3 Response, TransactionalTrainFareRequest Request)
        {
            ITicketPassengersRepository _passenger = new TicketPassengersRepository(_applicationDbContext);
            TicketPassengers psngr = _passenger.AddPassenger(new TicketPassengers()
            {
                ticketOrderId = ordrId,
                name = p.passengerName,
                sex = p.passengerGender,
                age = Convert.ToInt32(p.passengerAge),
                berthPreference = Request.berthPreference,
                foodPreference = "",
                concessionCode = "",
                idCardType = Convert.ToInt32(Icardno),
                idCardNumber = p.passengerCardNumber,
                firstChildName = Request.firstChildName,
                firstChildAge = Request.firstChildAge,
                firstChildSex = Request.firstChildSex,
                secondChildName = Request.secondChildName,
                secondChildAge = Request.secondChildAge,
                secondChildSex = Request.secondChildSex,
                bedRoll = Request.bedRoll,
                bookingStatus = "",
                coach = "",
                seat = "",
                berth = "",
                currentStatus = ""
            });
            _passenger.Commit();
            return psngr.Id;
        }
    }
}
