
namespace Mpower.Rail.NGETSystem.Models.Request
{
    /// <summary>
    /// this request is for fare enquiry with transaction_no and booking purpose
    /// url as------
    /// https://testngetjp.irctc.co.in/eticketing/webservices/taenqservices/avlFareenquiry/12488/20170126/ANVT/ALD/SL/GN/Y
    /// </summary>
    public class TransactionalFareEnquiry : FareEnquiry
    {
        public string wsUserLogin { get; set; }
        public string autoUpgradationSelected { get; set; }
        public string cache { get; set; }
        public string clientTransactionId { get; set; }
        public string clusterFlag { get; set; }
        public string ignoreChoiceIfWl { get; set; }
        public string mobileNumber { get; set; }
        public string onwardFlag { get; set; }
        public string agentDeviceId { get; set; }
        public string boardingStation { get; set; }
        public string travelInsuranceOpted { get; set; }
        public object passengerList{get;set;}

    }
}