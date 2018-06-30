using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class TrainsOnRoute : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLength(50)]
        public string fromStn { get; set; }

        [StringLength(50)]
        public string toStn { get; set; }

        public int day { get; set; }

        public int irctcXML { get; set; }

        public DateTime date { get; set; }

    }
}