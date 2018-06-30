using Mpower.Rail.NGETSystem.Models.Request;
using Mpower.Rail.NGETSystem.Models.Response;

namespace Mpower.Rail.NGETSystem.Processor.BookingServices
{
    public interface IBooking
    {
        /// <summary>
        /// with FindFareEnquiry find fare and it's availability of train where parameter will be /N in url
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="enquiryRequest">Request To send</param>
        /// <param name="userSession">User Session Id</param>
        /// <returns></returns>
        object FindFareEnquiry(string apiUrl, FareEnquiry enquiryRequest, long? userSession,string authInfo);

        /// <summary>
        /// FindTransactionalFareEnquiry find fare and it's availability of train where parameter will be /Y
        /// for transaction scenario in url
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="enquiryRequest">Request To send</param>
        /// <param name="userSession">User Session Id</param>
        /// <returns></returns>
        object FindTransactionalFareEnquiry(string apiUrl, TransactionalFareEnquiry enquiryRequest, long? userSession,string authInfo);

        /// <summary>
        /// To find the train between station enquiry during train ticket booking service.
        /// </summary>
        /// <param name="apiUrl"> Irctc api Url for TrainBtwnStns</param>
        /// <param name="Request">Request To send</param>
        /// <param name="userSession">User Session Id</param>
        /// <returns></returns>

        object FindTrainBtwnStation(string apiUrl, TrainBtwnStationRequest Request, long? userSession,string authInfo);

        

        /// <summary>
        /// to cancel the booked ticket of Indian Railways
        /// </summary>
        /// <param name="apiUrl">Irctc api Url for cancel ticket</param>
        /// <param name="Request">Request To send</param>
        /// <param name="long?userSession">User Session Id</param>
        /// <returns></returns>
        object CancelTicket(string apiUrl, CancelTicketRequest Request, long? userSession,string authInfo);


        /// <summary>
        /// To request change of boarding station during ticket booking service.
        /// </summary>
        /// <param name="apiUrl"> Irctc api Url for TrainBtwnStns</param>
        /// <param name="Request">Request To send</param>
        /// <param name="userSession">User Session Id</param>
        /// <returns></returns>status of the request

        object BoardingPointChange(string apiUrl, ChangeBoardingPointRequest Request, long? userSession,string authInfo);


    }
}