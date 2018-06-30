using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class Rail_Merchant : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        
        [StringLength(500)]
        [RequiredAttribute]
        public string MerchantID { get; set; }
        
        [StringLength(50)]
        [RequiredAttribute]
        public string UserName { get; set; }
        
        [StringLength(25)]
        [RequiredAttribute]
        public string Password { get; set; }
        
        [RequiredAttribute]
        public DateTime DateCreated { get; set; }
        public DateTime ?DateUpdated { get; set; }
        public bool IsActive { get; set; }
        public Int64 Creater { get; set; }
    }
}