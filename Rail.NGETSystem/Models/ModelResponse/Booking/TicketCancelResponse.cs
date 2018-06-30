using System;

namespace Mpower.Rail.NGETSystem.Models.Response
{
    public class CancelTicketResponse
    {
        public string amountCollected { get; set; }
        public string cancellationId { get; set; }
        public string cancelledDate { get; set; }
        public string cashDeducted { get; set; }
        public string currentStatus { get; set; }
        public string message { get; set; }
        public string name { get; set; }
        public string noOfPsgn { get; set; }
        public string pnrNo { get; set; }
        public string refundAmount { get; set; }
        public string serverId { get; set; }
        public string success { get; set; }
        public string timeStamp { get; set; }
    }
    public class ErrorResponseOnCencel : IDisposable
    {
        public string message { get; set; }
        public string success { get; set; }
        public string serverId { get; set; }
        public string timeStamp { get; set; }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}