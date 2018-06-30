using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Application
{
    public class Application_Settings : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLength(25)]
        [RequiredAttribute]
        public string key { get; set; }

        [StringLength(150)]
        [RequiredAttribute]
        public string value { get; set; }
        
        [RequiredAttribute]
        public double applicationID { get; set; }

    }
}
