
using Mpower.Rail.NGETSystem.Models.Request;
using Mpower.Rail.NGETSystem.Models.Response;

namespace Mpower.Rail.NGETSystem.Processor.EnquiryServices
{
    public class Enquiry : IEnquiry
    {
        private ServiceProxy _service = new ServiceProxy();
        public object GetPNRStatus(string apiUrl, long? userSession,string authInfo)
        {
             return _service.GetResponse("","GET", apiUrl, userSession,authInfo);          
        }
        public object GetTrainSchedule(string apiUrl, long? userSession,string authInfo)
        {
             return _service.GetResponse("", "GET", apiUrl, userSession,authInfo);          
        }

        //GetBoardingStation
        public object GetBoardingStation(string apiUrl, long? userSession,string authInfo)
        {
             return _service.GetResponse("", "GET", apiUrl, userSession,authInfo);          
        }

        public object GetRDSDetails(string apiUrl,RDSEnquiryRequest request, long? userSession, string authInfo)
        {
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(request).ToString();
            return _service.GetResponse(body,"POST",apiUrl,userSession,authInfo);
        }
       public object GetTicketBookingDetail(string apiUrl, long? userSession,string authInfo)
        {
             return _service.GetResponse("", "GET", apiUrl, userSession,authInfo);          
        }
    }
}
