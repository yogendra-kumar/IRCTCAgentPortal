using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class MerchantType : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
         
         [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLength(450)]
        public string merchantId { get; set; }

        public Decimal nonACCharge { get; set; }

        public Decimal aCCharge { get; set; }

        public int packageId { get; set; }

        [StringLength(50)]
        public string pgName { get; set; }

        public bool isOxiCashLogin { get; set; }

        [StringLength(1000)]
        public string loginUrl { get; set; }

        [StringLength(50)]
        public string partnerID { get; set; }

        [StringLength(5)]
        public string pgChargeUnderCondition { get; set; }

        [StringLengthAttribute(5)]
        public string pgChargeBeyondCondition {get;set;}

        public decimal conditionalAmount { get; set; }

        public int acPackageID { get; set; }

        public decimal oxigenServiceCharge {get;set;}

        public decimal merchantCommission {get;set;}
    }
}