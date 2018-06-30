
namespace Mpower.Rail.NGETSystem.Models.Response
{
    public class PassengerList
    {
        public string bookingBerthCode { get; set; }
        public string bookingBerthNo { get; set; }
        public string bookingCoachId { get; set; }
        public string bookingStatus { get; set; }
        public string bookingStatusIndex { get; set; }
        public string concessionOpted { get; set; }
        public string currentBerthNo { get; set; }
        public string currentCoachId { get; set; }
        public string currentStatus { get; set; }
        public string currentStatusIndex
        { get; set; }
        public string passengerBedrollChoice { get; set; }
        public string passengerBerthChoice { get; set; }
        public string passengerIcardFlag { get; set; }
        public string passengerSerialNumber { get; set; }
    }
}