using Mpower.Rail.NGETSystem.Models.Request;

namespace Mpower.Rail.Api.NGET.Request
{
    public class ChangeBoardingRequest
    {
       public  ChangeBoardingPointRequest  request { get; set; } 
        public long userSession { get; set; }

    }
      
}