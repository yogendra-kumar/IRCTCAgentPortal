
using System;

namespace Mpower.Rail.NGETSystem.Models.Request
{

    /// <summary>
    /// this request is for normal fare enquiry without transaction_no and  without booking purpose
    /// url as-----
    /// https://testagent.irctc.co.in/eticketing/webservices/taenqservices/trnscheduleEnq/11078?journeyDate=20150630&startingStationCode=JAT
    /// </summary>
public class ScheduleEnquiryRequest
{
        public string trainNo { get; set; } 
        public string journeyDate { get; set; } 
        public string startingStationCode { get; set; } 
}
}