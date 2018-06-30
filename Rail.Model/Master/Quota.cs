using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail.Master
{
    /// <summary>
    /// This model is mapped with Rail_Quota table for ticket booking.
    /// </summary>
    public class Quota : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLengthAttribute(50)]
        [RequiredAttribute]
        public string fullName { get; set; }

        [StringLengthAttribute(5)]
        [RequiredAttribute]
        public string shortName { get; set; }

        [StringLengthAttribute(100)]
        [RequiredAttribute]
        public string description { get; set; }
                
        public bool isActive { get; set; }

    }
}