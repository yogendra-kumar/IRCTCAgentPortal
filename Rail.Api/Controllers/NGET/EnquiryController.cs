
//EnquiryController
using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mpower.Rail.Data;
using Mpower.Rail.NGETSystem.Processor.EnquiryServices;
using Microsoft.EntityFrameworkCore;
using Mpower.Rail.Processor.Application;
using Mpower.Rail.Api.Models;
using Mpower.Rail.Model.Rail;
using Mpower.Rail.NGETSystem.Models.Response;
using Newtonsoft.Json;
using Mpower.Rail.Data.IRepository;
using Mpower.Rail.Data.Repository;
using System.Linq;
using Mpower.Rail.Api.NGET.Request;

namespace Mpower.Rail.Api.Controllers
{
    [AllowAnonymous]
    [Route("Mpower/Rail/Irctc")]
    public class EnquiryController : Controller
    {
        private IEnquiry _enquiry;

        private ILogProcessor _logProcessor;

        private ApplicationDbContext _applicationDbContext;

        private IConfigurationRepository _configuration;

        private string _irctcUrl;

        private string _irctc_userName;
        private string _irctc_password;

        private string _authInfo;
        public EnquiryController(IEnquiry enquiry, DbContextOptions<ApplicationDbContext> applicationDbContext)
        {
            _enquiry = enquiry;
            _applicationDbContext = new ApplicationDbContext(applicationDbContext);
            _logProcessor = new LogProcessor(_applicationDbContext);
            _configuration = new ConfigurationRepository(_applicationDbContext);
            _irctcUrl = _configuration.FindBy(x => x.key == "IRCTC_ApiURL").FirstOrDefault().value;
            _irctc_userName = _configuration.FindBy(x => x.key == "IRCTC_Username").FirstOrDefault().value;
            _irctc_password = _configuration.FindBy(x => x.key == "IRCTC_Password").FirstOrDefault().value;
            _authInfo = _irctc_userName + ":" + _irctc_password;
        }

        /// <summary>
        /// This API will receive ticket pnr details
        /// </summary>
        /// <param name="PNREnquiryRequest">This Api Accept PNREnquiryRequest As a post</param>
        /// <returns>This Api will rerurn object of PNR Enquiry Response class</returns>
        [HttpPostAttribute]
        [RouteAttribute("PNREnquiry")]
        public IActionResult GetPNRStatus([FromBodyAttribute] PNREnquiryRequest request)
        {
            try
            {
                if (!ModelState.IsValid || request.pnrNo <= 0)
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }
                if (string.IsNullOrEmpty(_irctcUrl) || string.IsNullOrEmpty(_irctc_userName) || string.IsNullOrEmpty(_irctc_password))
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "No IRCTC configuration found", Status = "failed" });
                }
                string uri = _irctcUrl + "taenqservices/pnrenquiry/" + request.pnrNo;

                CommunicationLog("OxiRail", "PNREnquiry IN->>:" + uri + ":" + JsonConvert.SerializeObject(request).ToString(), Convert.ToInt64(request.userSession));
                var response = _enquiry.GetPNRStatus(uri, request.userSession, _authInfo);
                CommunicationLog("OxiRail", "PNREnquiry OUT->>:" + uri + ":" + JsonConvert.SerializeObject(response).ToString(), Convert.ToInt64(request.userSession));

                var pnrEnquiryResponse = JsonConvert.DeserializeObject<PNREnquiryResponse>(response.ToString());
                if (pnrEnquiryResponse.passengerList == null)
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.ToString());
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = errorResponse.errorMessage, Status = "Error", ResponseResult = "" });
                }
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = pnrEnquiryResponse });
            }
            catch (Exception ex)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "Api_Error", Status = "Error", ResponseResult = ex });
            }
        }

        /// <summary>
        /// This API will receive ticket pnr details
        /// </summary>
        /// <param name="trainNo">This Api Accept Train No As a query string</param>
        /// <param name="journeyDate">This Api Accept Journey date As a query string</param>
        /// <param name="startingStationCode">This Api Accept starting station As a query string</param>
        /// <param name="userSession">This Api Accept useSession As a query string</param>
        /// <returns>This Api will rerurn object of PNR Enquiry Response class</returns>

        [HttpPostAttribute]
        [RouteAttribute("TrainScheduleEnquiry")]
        public IActionResult GetTrainSchedule([FromBody]TrainScheduleRequest Request) //string trainNo,string station, DateTime date,long? userSession)
        {
            try
            {
                string uri = string.Empty;
                if (!ModelState.IsValid || string.IsNullOrEmpty(Request.request.trainNo))
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }               

                if (string.IsNullOrEmpty(_irctcUrl) || string.IsNullOrEmpty(_irctc_userName) || string.IsNullOrEmpty(_irctc_password))
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "No IRCTC configuration found", Status = "failed" });

                }
                if (!string.IsNullOrEmpty(Convert.ToString(Request.request.journeyDate)) || !string.IsNullOrEmpty(Request.request.startingStationCode))
                {

                     uri = _irctcUrl + "taenqservices/trnscheduleEnq/" + Request.request.trainNo + "?" + "journeyDate=" + Convert.ToString(Request.request.journeyDate) + "&&" + "startingStationCode=" + Request.request.startingStationCode;

                }
                else
                {
                    uri = _irctcUrl + "taenqservices/trnscheduleEnq/" + Request.request.trainNo;
                }

                CommunicationLog("OxiRail", "TrainSceduleEnquiry IN->>:" + uri + ":" + JsonConvert.SerializeObject(Request.request).ToString(), Request.userSession);
                var response = _enquiry.GetTrainSchedule(uri, Request.userSession, _authInfo);
                CommunicationLog("OxiRail", "TrainSceduleEnquiry OUT->>" + JsonConvert.SerializeObject(response).ToString(), Request.userSession);

                var trainScheduleResponse = JsonConvert.DeserializeObject<TrainScheduleResponse>(response.ToString());
                if (trainScheduleResponse.stationList == null)
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.ToString());
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = errorResponse.errorMessage, Status = "Error", ResponseResult = "" });
                }
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = trainScheduleResponse });
            }
            catch (Exception ex)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "Api_Error", Status = "Error", ResponseResult = ex });
            }
        }


        [HttpPostAttribute]
        [RouteAttribute("BoardingStationEnquiry")]
        public IActionResult GetBoardingStation([FromBody]BoardingStationRequest Request) //string trainNo,string station, DateTime date,long? userSession)
        {
            try
            {//boardingstationenq/ trainNo/jrnyDate/frmStation/toStation/jrnClass
                 string uri=string.Empty;
                if (!ModelState.IsValid || string.IsNullOrEmpty(Request.request.trainNo))
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }
                 
                 uri = _irctcUrl + "taenqservices/boardingstationenq/" + Request.request.trainNo+"/"+Convert.ToString(Request.request.journeyDate)+"/"+Request.request.frmStation+"/"+Request.request.toStation+"/"+Request.request.jrnClass;
                if(string.IsNullOrEmpty(_irctcUrl) || string.IsNullOrEmpty(_irctc_userName) || string.IsNullOrEmpty(_irctc_password))
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "No IRCTC url configuration found", Status = "failed" });
                }
                CommunicationLog("OxiRail", "BoardingSationEnquiry IN->>:" +uri+":"+ JsonConvert.SerializeObject(Request.request).ToString(), Request.userSession);
                var response = _enquiry.GetBoardingStation(uri, Request.userSession,_authInfo);               
               CommunicationLog("OxiRail", "BoardingSationEnquiry OUT->>" + JsonConvert.SerializeObject(response).ToString(), Request.userSession);

                var boardingResponse = JsonConvert.DeserializeObject<BoardingStationResponse>(response.ToString());
                if (boardingResponse.boardingStationList == null)
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.ToString());
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = errorResponse.errorMessage, Status = "Error", ResponseResult = "" });
                }
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = boardingResponse });
            }
            catch (Exception ex)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "Api_Error", Status = "Error", ResponseResult = ex });
            }
        }

        [HttpPostAttribute]
        [RouteAttribute("RDSEnquiry")]
        public IActionResult GetRdsDetails([FromBodyAttribute] RDSRequest request)
        {
            try
            {
                if (!ModelState.IsValid || request.request == null || string.IsNullOrEmpty(request.request.masterUserId))
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1000", ResponseMessage = "Invalid model", Status = "failed" });
                }
                if (string.IsNullOrEmpty(_irctcUrl) || string.IsNullOrEmpty(_irctc_userName) || string.IsNullOrEmpty(_irctc_password))
                {
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "1001", ResponseMessage = "No IRCTC configuration found", Status = "failed" });
                }
                string uri = _irctcUrl + "taenqservices/rdsaccdetailsenq";

                CommunicationLog("OxiRail", "RDSEnquiry IN->>:" + uri + ":" + JsonConvert.SerializeObject(request).ToString(), Convert.ToInt64(request.userSession));
                var response = _enquiry.GetRDSDetails(uri, request.request, request.userSession, _authInfo);
                CommunicationLog("OxiRail", "RDSEnquiry OUT->>:" + uri + ":" + JsonConvert.SerializeObject(response).ToString(), Convert.ToInt64(request.userSession));

                var rdsEnquiryResponse = JsonConvert.DeserializeObject<RDSEnquiryResponse>(response.ToString());
                if (rdsEnquiryResponse.balanceLeft == null)
                {
                    var errorResponse = JsonConvert.DeserializeObject<ErrorResponse>(response.ToString());
                    return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = errorResponse.errorMessage, Status = "Error", ResponseResult = "" });
                }
                return Ok(new Application_ResponseWrapper() { ResponseCode = "0", ResponseMessage = "success", Status = "success", ResponseResult = rdsEnquiryResponse });
            }
            catch (Exception ex)
            {
                return Ok(new Application_ResponseWrapper() { ResponseCode = "1005", ResponseMessage = "Api_Error", Status = "Error", ResponseResult = ex });
            }
        }
        private void CommunicationLog(string Type, string Desc, long session)
        {
            _logProcessor.InsertLogs(
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
    }
}