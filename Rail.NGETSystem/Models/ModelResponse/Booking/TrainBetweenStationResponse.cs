using System.Collections.Generic;

namespace Mpower.Rail.NGETSystem.Models.Response
{
    public class TrainBetweenStationResponse
    {
        public string[] quotaList { get; set; }
        public string serverId { get; set; }
        public List<TrainList> trainBtwnStnsList { get; set; }
    }

    public class TrainList
    {
        public string arrivalTime { get; set; }
        public object avlClasses { get; set; }
        public string departureTime { get; set; }
        public string distance { get; set; }
        public string duration { get; set; }
        public string fromStnCode { get; set; }
        public string runningFri { get; set; }
        public string runningMon { get; set; }
        public string runningSat { get; set; }
        public string runningSun { get; set; }
        public string runningThu { get; set; }
        public string runningTue { get; set; }
        public string runningWed { get; set; }
        public string toStnCode { get; set; }
        public string trainName { get; set; }
        public string trainNumber { get; set; }
        public string trainType { get; set; }
    }
}