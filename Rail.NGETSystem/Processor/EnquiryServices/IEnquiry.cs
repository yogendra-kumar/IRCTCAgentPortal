

using Mpower.Rail.NGETSystem.Models.Request;

namespace Mpower.Rail.NGETSystem.Processor.EnquiryServices
{
    public interface IEnquiry
    {
       /// <summary>
        ///To get the pnr Status from Indian Railway 
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="pnrNo"></param>
        /// <param name="userSession"></param>
        /// <returns></returns>

        object GetPNRStatus(string apiUrl, long? userSession,string authInfo);
        object GetTrainSchedule(string apiUrl, long? userSessio,string authInfo);
        object GetBoardingStation(string apiUrl, long? userSessio,string authInfo);
        object GetRDSDetails(string apiUrl, RDSEnquiryRequest request, long? userSession, string authInfo);
        object GetTicketBookingDetail(string apiUrl, long? userSession,string authInfo);
    }
}