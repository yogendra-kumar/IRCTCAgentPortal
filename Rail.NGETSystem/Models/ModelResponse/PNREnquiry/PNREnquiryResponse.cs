using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mpower.Rail.NGETSystem.Models.Response
{
    public class PNREnquiryResponse
    {
   public string boardingPoint { get; set; }
   public string bookingFare { get; set; }
   public string chartStatus { get; set; }
   public string dateOfJourney { get; set; }
   public string destinationStation { get; set; }
   public List<string> informationMessage { get; set; }
   public string journeyClass { get; set; }
   public string numberOfpassenger { get; set; }
   public PassengerList passengerList { get; set; }
   public string pnrNumber { get; set; }
   public string quota { get; set; }
   public string reasonType { get; set; }
   public string reservationUpto { get; set; }
   public string serverId { get; set; }
   public string sourceStation { get; set; }
   public string ticketFare { get; set; }
   public string timeStamp { get; set; }
   public string trainName { get; set; }
   public string trainNumber { get; set; }
    }
}