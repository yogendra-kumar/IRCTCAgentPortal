using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class Configuration : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLength(100)]
        [RequiredAttribute]
        public string description { get; set; }

        [StringLength(50)]

        public string key { get; set; }

        [StringLength(500)]

        public string value { get; set; }
        
        [StringLength(450)]

        public string applicationID { get; set; }
        

        public bool active { get; set; }


        public DateTime? date { get; set; }
    }
}