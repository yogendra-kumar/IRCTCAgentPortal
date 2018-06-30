using Mpower.Rail.NGETSystem.Models.Request;

namespace Mpower.Rail.Api.NGET.Request
{
    public class cancelTicketRequest
    {
        public CancelTicketRequest request { get; set; }
        public long userSession { get; set; }
    }
}