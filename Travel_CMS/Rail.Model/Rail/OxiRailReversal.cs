
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class OxiRailReversal : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
         
         [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLength(50)]
        [RequiredAttribute]
        public string gatewayTxnNumber { get; set; }

        [RequiredAttribute]
        public DateTime txnDate { get; set; }

        [RequiredAttribute]
        public bool isReverse { get; set; }

        public DateTime reversalDate { get; set; }
    }
}