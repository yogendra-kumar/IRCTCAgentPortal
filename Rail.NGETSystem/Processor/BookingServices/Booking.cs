using System;
using Mpower.Rail.NGETSystem.Models.Request;
using Mpower.Rail.NGETSystem.Models.Response;
namespace Mpower.Rail.NGETSystem.Processor.BookingServices
{
    public class Booking : IBooking
    {
        private ServiceProxy _service = new ServiceProxy();
        public object FindFareEnquiry(string apiUrl, FareEnquiry enquiryRequest, long? userSession,string authInfo)
        {
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(enquiryRequest).ToString();
            var response = _service.GetResponse(body, "POST", apiUrl, userSession,authInfo);
            return desirealizeObject_On_TypeBase(Convert.ToInt32(enquiryRequest.enquiryType), response);

        }
        public object FindTransactionalFareEnquiry(string apiUrl, TransactionalFareEnquiry enquiryRequest, long? userSession,string authInfo)
        {
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(enquiryRequest).ToString();
            var response = _service.GetResponse(body, "POST", apiUrl, userSession,authInfo);
            return desirealizeObject_On_TypeBase(Convert.ToInt32(enquiryRequest.enquiryType), response);

        }

        /// <summary>
        /// used to find the Train Between Station and its method type is get
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="Request"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public object FindTrainBtwnStation(string apiUrl, TrainBtwnStationRequest Request, long? userSession, string authInfo)
        {
            string FormatedApiUrl = apiUrl + "/" + Request.trainFrom + "/" + Request.trainTo + "/" + Request.enquiryForDate;
            var response = _service.GetResponse("", "GET", FormatedApiUrl, userSession,authInfo);
            return response;
        }


        /// <summary>
        /// used to Cancel the booked ticket of Indian Railways
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="Request"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public object CancelTicket(string apiUrl, CancelTicketRequest Request, long? userSession,string authInfo)
        {
            string FormatedApiUrl = apiUrl + "/" + Request.reservationId + "/" + Request.agentTxnId + "/" + Request.psgnToken;
            var response = _service.GetResponse("", "GET", FormatedApiUrl, userSession,authInfo);
            return response;
        }

        /// <summary>
        /// To desirealizeObject_On_TypeBase
        /// </summary>
        /// <param name="type"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        private object desirealizeObject_On_TypeBase(int type, object response)
        {
            try
            {
                switch (type)
                {
                    case 1:
                        {
                            FareEnquiry_Response_Type1 Response = (FareEnquiry_Response_Type1)response;
                            return Response;
                        }
                    case 2:
                        {
                            FareEnquiry_Response_Type2 Response = (FareEnquiry_Response_Type2)response;
                            return Response;
                        }
                    default:
                        {
                            FareEnquiry_Response_Type3 Response = (FareEnquiry_Response_Type3)response;
                            return Response;
                        }
                }
            }
            catch
            {
                return response;
            }
        }

        /// <summary>
        /// used to Change the Boarding point for ticket of Indian Railways
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="Request"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>
        public object BoardingPointChange(string apiUrl, ChangeBoardingPointRequest Request, long? userSession,string authInfo)
        {
            string FormatedApiUrl = apiUrl + Request.pnr + "/" + Request.boardingStation;
            var response = _service.GetResponse("", "GET", FormatedApiUrl, userSession,authInfo);
            return response;
        }
    }
}