namespace Mpower.Rail.NGETSystem.Models.Request
{
    /// <summary>
    /// this request is for normal fare enquiry without transaction_no and  without booking purpose
    /// url as-----
    /// https://testngetjp.irctc.co.in/eticketing/webservices/taenqservices/avlFareenquiry/12560/20161218/NDLS/MUV/SL/GN/N
    /// </summary>
    public class FareEnquiry
    {
        public string masterId { get; set; } //it is IRCTC agent id
        public string enquiryType { get; set; } //there is 3 types 1 for only fare,2 for avaibility and 3 for both
        public string moreThanOneDay { get; set; } //true for more than one day and false for single day enquiry
        public string reservationChoice { get; set; } //as 99
    }
}