using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mpower.Rail.NGETSystem.Models.Response;

namespace Mpower.Rail.NGETSystem.Models.Response
{
public class StationList
    {
         public string arrivalTime { get; set; }
        public string dayCount { get; set; }
        public string departureTime { get; set; }
        public string distance { get; set; }
        public string haltTime { get; set; }
        public string routeNumber { get; set; }
        public string stationCode { get; set; }
        public string stationName { get; set; }
        public string stnSerialNumber { get; set; }
       
        
    }

    public class TrainScheduleResponse
    {
        public string serverId { get; set; }
        public string stationFrom { get; set; }
        public IList<StationList> stationList { get; set; }
    }

}