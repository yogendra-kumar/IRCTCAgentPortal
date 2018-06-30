using System;
using System.Collections.Generic;

namespace Mpower.Rail.NGETSystem.Models.Response
{
    /// <summary>
    /// FareEnquiry_Response where if Type1 means only fare, if Type2 then Only dayList 
    /// and if Type3 then both will be in Response
    /// </summary>
    public class FareEnquiry_Response_Type2 : TrainFare
    {
        public string reqEnqParam { get; set; }
        public string serverId { get; set; }
        public string timeStamp { get; set; }
        public string trainName { get; set; }
        public BookingConfiguration bkgCfg { get; set; }

    }
    public class FareEnquiry_Response_Type1
    {
        public string reqEnqParam { get; set; }
        public string serverId { get; set; }
        public string timeStamp { get; set; }
        public string trainName { get; set; }
        public object avlDayList { get; set; }
        public BookingConfiguration bkgCfg { get; set; }


    }
    public class FareEnquiry_Response_Type3 : TrainFare
    {
        public string reqEnqParam { get; set; }
        public string serverId { get; set; }
        public string timeStamp { get; set; }
        public string trainName { get; set; }
        public object avlDayList { get; set; }
        public BookingConfiguration bkgCfg { get; set; }
    }

    public class ErrorResponse: IDisposable
    {
        public string errorMessage { get; set; }
        public string reqEnqParam { get; set; }

         public string serverId { get; set; }
        public string timeStamp { get; set; }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }

}