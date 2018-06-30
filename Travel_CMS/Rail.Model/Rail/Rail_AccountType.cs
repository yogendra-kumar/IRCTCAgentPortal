using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class Rail_AccountType : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLength(50)]
        [RequiredAttribute]
        public string name { get; set; }
       
        [RequiredAttribute]
        public decimal margin { get; set; }

        [RequiredAttribute]
        public bool isDeleted { get; set; }
    }
}