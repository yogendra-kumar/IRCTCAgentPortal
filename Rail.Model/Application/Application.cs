using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Application
{
    public class Application: IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [StringLength(450)]
        [RequiredAttribute]
        public string guid { get; set; }

        [StringLength(150)]
        [RequiredAttribute]
        public string applicationName { get; set; }

        [StringLength(150)]
        public string domainUrl { get; set; }

        [StringLength(45)]
        public string domainIP { get; set; }

        [RequiredAttribute]
        public double layoutID { get; set; }

        [StringLength(50)]
        public string feviconImageUrl { get; set; }

        [StringLength(50)]
        [RequiredAttribute]
        public string logo { get; set; }

        [StringLength(25)]
        public string supportEmail { get; set; }        
    }
}