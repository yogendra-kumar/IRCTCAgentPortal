//ChangeBoardingPointRequest

using System;

namespace Mpower.Rail.NGETSystem.Models.Request
{

    /// <summary>
    /// this request is for normal fare enquiry without transaction_no and  without booking purpose
    /// url as-----
    /// https://testagent.irctc.co.in/eticketing/webservices/taenqservices/trnscheduleEnq/11078?journeyDate=20150630&startingStationCode=JAT
    /// </summary>
public class ChangeBoardingPointRequest
{
        public string pnr { get; set; }        
        public string boardingStation { get; set; } 
}
}