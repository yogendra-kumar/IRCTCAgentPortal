using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class TdrDetails : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLength(50)]
        [RequiredAttribute]
        public string mid { get; set; }

        [StringLength(50)]
        public string ticketNumber { get; set; }

        [StringLength(50)]
        [RequiredAttribute]
        public string pnr { get; set; }

        [StringLength(250)]
        public string reason { get; set; }

        [StringLength(1)]
        [RequiredAttribute]
        public string psgn1 { get; set; }

        [StringLength(1)]
        [RequiredAttribute]
        public string psgn2 { get; set; }

        [StringLength(1)]
        [RequiredAttribute]
        public string psgn3 { get; set; }

        [StringLength(1)]
        [RequiredAttribute]
        public string psgn4 { get; set; }

        [StringLength(1)]
        [RequiredAttribute]
        public string psgn5 { get; set; }

        [StringLength(1)]
        [RequiredAttribute]
        public string psgn6 { get; set; }

        [RequiredAttribute]
        public DateTime date { get; set; }

        [StringLength(1000)]
        public string status { get; set; }

        [StringLength(50)]
        public string amountRefunded { get; set; }

        public DateTime refundedDate { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [RequiredAttribute]
        public bool isMailSent { get; set; }

        public DateTime tdrSentDate { get; set; }

        [StringLength(50)]
        public string confirmed { get; set; }
    }
}
