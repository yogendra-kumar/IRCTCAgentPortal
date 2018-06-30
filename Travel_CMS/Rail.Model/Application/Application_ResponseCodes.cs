using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Application
{
    public class Application_ResponseCodes : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [RequiredAttribute]
        public double applicationID { get; set; }

        [StringLength(10)]
        [RequiredAttribute]
        public string responseCode { get; set; }

        [StringLength(450)]
        public string responseMessage { get; set; }

    }
}