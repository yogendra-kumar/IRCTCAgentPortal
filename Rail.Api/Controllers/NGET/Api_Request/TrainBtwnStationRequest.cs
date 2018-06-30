using Mpower.Rail.NGETSystem.Models.Request;
namespace Mpower.Rail.Api.NGET.Request
{
    public class TrainBtwnStnsRequest
    {
        public TrainBtwnStationRequest request{get;set;}
        public long userSession { get; set; }

    }
}