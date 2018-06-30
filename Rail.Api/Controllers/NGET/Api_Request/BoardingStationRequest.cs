using Mpower.Rail.NGETSystem.Models.Request;
namespace Mpower.Rail.Api.NGET.Request 
{ 

public class BoardingStationRequest 
    { 
        public BoardingEnquiryRequest request { get; set; }                        
        public long userSession { get; set; } 
 
    } 

    
}