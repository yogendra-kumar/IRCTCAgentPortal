using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class Ngetbresponse : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [RequiredAttribute]
        public long tktOrderID { get; set; }

        [StringLength(5000)]
        [RequiredAttribute]
        public string response { get; set; }

        [RequiredAttribute]
        public DateTime date { get; set; }

        public long sessions { get; set; }


    }
}