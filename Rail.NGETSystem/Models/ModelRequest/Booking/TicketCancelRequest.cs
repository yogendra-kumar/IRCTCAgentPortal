namespace Mpower.Rail.NGETSystem.Models.Request
{
    public class CancelTicketRequest
    {
        public string reservationId { get; set; }
        public string agentTxnId { get; set; }
        public string psgnToken { get; set; }
    }
}