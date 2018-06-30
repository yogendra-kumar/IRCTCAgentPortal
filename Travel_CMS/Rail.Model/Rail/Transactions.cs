using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class Transactions : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        public long transactionTypes { get; set; }

        [StringLengthAttribute(50)]
        public string transactionNumber { get; set; }

        [StringLengthAttribute(50)]
        public string merchantAccount { get; set; }

        public long ticketOrders { get; set; }

        public long ticketCancellations { get; set; }

        public long paymentGateways { get; set; }

        public decimal amount { get; set; }

        [StringLengthAttribute(200)]
        public string remark { get; set; }

        public long sessions { get; set; }

        public DateTime date { get; set; }
        
        [StringLengthAttribute(50)]
        public string pgTransactionNumber { get; set; }
    }
}
