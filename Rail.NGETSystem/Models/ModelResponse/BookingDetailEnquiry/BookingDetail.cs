namespace Mpower.Rail.NGETSystem.Models.Response
{
    public class BookingDetailResponse : TrainFare
    {
        public string timeStamp { get; set; }
        public string trainName { get; set; }
        public string serverId { get; set; }
        public string arrivalTime { get; set; }
        public string boardingDate { get; set; }
        public string boardingStn { get; set; }
        public string bookingDate { get; set; }
        public string departureTime { get; set; }
        public string destArrvDate { get; set; }
        public string destStn { get; set; }
        public string fromStn { get; set; }
        public string journeyClass { get; set; }
        public string journeyDate { get; set; }
        public string journeyQuota { get; set; }
        public string numberOfAdults { get; set; }
        public string numberOfChilds { get; set; }
        public string numberOfpassenger { get; set; }
        public string pnrNumber { get; set; }
        public string reasonIndex { get; set; }
        public string reasonType { get; set; }
        public string reservationId { get; set; }
        public string resvnUptoStn { get; set; }
        public string timeTableFlag { get; set; }
        public string trainNumber { get; set; }
        public object psgnDtlList { get; set; }//PassangerList of pnr will be consumed
    }
}