
using Mpower.Rail.NGETSystem.Models.Request; 

namespace Mpower.Rail.Api.NGET.Request
{
    public class RDSRequest
    {
        public RDSEnquiryRequest request {get;set;}
        public long? userSession{get;set;}
    }
}