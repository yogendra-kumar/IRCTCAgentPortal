using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Application
{
    public class Application_Errors : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [Required]
        public Int64 applicationID { get; set; }

        [Required]
        public Int64 pageID { get; set; }

        [StringLength(50)]
        [RequiredAttribute]
        public string errorType { get; set; }

        [StringLength(1000)]
        [RequiredAttribute]
        public string errorDescription { get; set; }

        [RequiredAttribute]
        public DateTime logDate { get; set; }

        [StringLength(50)]
        public string Source { get; set; }

    }


}