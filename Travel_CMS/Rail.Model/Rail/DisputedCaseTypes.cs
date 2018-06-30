
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class DisputedCaseTypes : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
         
         [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLength(50)]
        public string  identifier { get; set; }
        
        [StringLength(100)]
        public string  Description { get; set; }

        [StringLength(50)]
        public string  ReferenceTable { get; set; }
    }
}