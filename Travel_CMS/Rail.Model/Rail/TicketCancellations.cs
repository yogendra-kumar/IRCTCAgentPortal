using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class TicketCancellations : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [RequiredAttribute]
        public long bookedTickets { get; set; }

        [StringLength(50)]
        public string ticketNumber { get; set; }

        public bool status { get; set; }

        public Decimal amountCollected { get; set; }

        public Decimal refundedAmount { get; set; }
       
        public Decimal cashDeducted { get; set; }
        
        public Decimal cashCollected { get; set; }
        
        public Decimal amount { get; set; }

        public long paymentGateways { get; set; }
       
        public long sessions { get; set; }

        public DateTime date { get; set; }
        
        [StringLength(50)]
        public string cancelledDate { get; set; }
    }
}