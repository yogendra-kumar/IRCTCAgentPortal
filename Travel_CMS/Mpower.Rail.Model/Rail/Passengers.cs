using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class Passengers : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLength(20)]
        public string loginAccount { get; set; }

        [StringLength(16)]
        public string name { get; set; }

        public int bDay { get; set; }

        public int bMonth { get; set; }

        public int bYear { get; set; }

        [StringLength(10)]
        public string sex { get; set; }

        [StringLength(20)]
        public string birthPf { get; set; }

        [StringLength(16)]
        public string foodPf { get; set; }

         public long idCardTypeId { get; set; }
         
         [StringLength(25)]
         public string idCardNumber { get; set; }

        public bool senior { get; set; }
    }
}