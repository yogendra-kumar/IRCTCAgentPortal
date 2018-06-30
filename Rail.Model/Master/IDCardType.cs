using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail.Master
{
    /// <summary>
    /// This model is mapped with Rail_IDCardType table for Identity card types.
    /// </summary>
    public class IDCardType : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLengthAttribute(50)]
        [RequiredAttribute]
        public string name { get; set; }

        [StringLengthAttribute(100)]
        [RequiredAttribute]
        public string description { get; set; }
    }
}