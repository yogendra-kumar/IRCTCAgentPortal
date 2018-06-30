using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class Rail_UserAgent : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [StringLength(20)]
        [RequiredAttribute]
        public string merchantAccount { get; set; }
       
        [RequiredAttribute]
        public Int64 transactions { get; set; }

        [StringLength(50)]
        [RequiredAttribute]
        public string email { get; set; }

        [StringLength(50)]
        [RequiredAttribute]
        public bool isActive { get; set; }

        [StringLength(50)]
        [RequiredAttribute]
        public string merchantId { get; set; }

        [RequiredAttribute]
        public Int64 merchanrType { get; set; }

        [StringLength(20)]
        [RequiredAttribute]
        public string subUserId { get; set; }

        [StringLength(20)]
        [RequiredAttribute]
        public string subUserPassword { get; set; }

        [StringLength(20)]
        [RequiredAttribute]
        public string accountNumber { get; set; }

        [StringLength(15)]
        [RequiredAttribute]
        public string panCard { get; set; }

        [StringLength(50)]
        [RequiredAttribute]
        public string digitalCertificate { get; set; }

        [StringLength(20)]
        [RequiredAttribute]
        public string deviceId { get; set; }

        [StringLength(20)]
        [RequiredAttribute]
        public string mobileNumber { get; set; }

        [RequiredAttribute]
        public bool isIrctcCardActiveted { get; set; }

        [RequiredAttribute]
        public bool IsB2B { get; set; }
           
        [RequiredAttribute]
        public bool IsOxiSmart { get; set; }
        
        [StringLength(50)]
        [RequiredAttribute]
        public string macId { get; set; }

    }
}