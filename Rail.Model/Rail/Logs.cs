using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class Logs : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLength(50)]
        public string type { get; set; }

        [StringLength(50)]
        public string source { get; set; }

        [StringLength(50)]
        public string host { get; set; }

        [StringLength(10000)]
        [RequiredAttribute]
        public string description { get; set; }

        [StringLength(50)]
        public string session { get; set; }

        public DateTime? logDate { get; set; }
    }
}