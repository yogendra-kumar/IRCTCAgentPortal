using System;

namespace Mpower.Rail.NGETSystem.Models.Request
{

    /// <summary>
    /// this request is for normal fare enquiry without transaction_no and  without booking purpose
    /// url as-----trainNo/jrnyDate/frmStation/toStation/jrnClass
    /// https://testagent.irctc.co.in/eticketing/webservices/taenqservices/boardingstationenq/11078/20150621/JAT/NDLS/SL
    /// </summary>
public class BoardingEnquiryRequest
{
        public string trainNo { get; set; } 
        public string journeyDate { get; set; } 
        public string frmStation { get; set; } 
        public string toStation { get; set; } 
        public string jrnClass { get; set; }
        
}
}