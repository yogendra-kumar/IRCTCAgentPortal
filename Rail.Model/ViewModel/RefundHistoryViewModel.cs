using System;
using System.ComponentModel.DataAnnotations;
using Mpower.Rail.Model.EntityBase;


namespace Mpower.Rail.Model.ViewModel
{
    public class RefundHistoryViewModel : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [RequiredAttribute]
        public Int64 Id { get; set; }

        public Int64 ticketOrderId {get;set;}
        
        public decimal amount {get;set;}

        public string date {get;set;}

        public string cancelledPassenger {get;set;}
    }
}