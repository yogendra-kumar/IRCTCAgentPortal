using Mpower.Rail.NGETSystem.Models.Request;
namespace Mpower.Rail.Api.NGET.Request 
{ 

public class TrainScheduleRequest 
    { 
        public ScheduleEnquiryRequest request { get; set; }                        
        public long userSession { get; set; } 
 
    } 

    
}