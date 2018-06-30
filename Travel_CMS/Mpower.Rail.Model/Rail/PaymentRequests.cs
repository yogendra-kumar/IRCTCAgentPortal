using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class PaymentRequests : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        public long ticketOrders { get; set; }

        [StringLength(50)]
        public string merchantAccount { get; set; }

        [StringLength(50)]
        public string transactionNumber { get; set; }

        public Decimal amount { get; set; }

        public long paymentGateways { get; set; }

        public long sessions { get; set; }

        public DateTime date { get; set; }

        [StringLength(50)]
        public string vId { get; set; }

        [StringLength(50)]
        public string vPassword { get; set; }

    }
}