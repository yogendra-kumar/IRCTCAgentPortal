using System;
using System.ComponentModel.DataAnnotations;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.ViewModel
{
    public class TdrViewModel: IEntityBase, IDisposable
    {
         void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }
        [RequiredAttribute]
        public Int64 Id {get;set;}


        [StringLength(25)]
        [RequiredAttribute]
        public string pnr { get; set; }

        public Int64 reasonId { get; set; }

        [RequiredAttribute]
        public Int64 passengerId { get; set; }

        [RequiredAttribute]
        public DateTime date { get; set; } = DateTime.Now;

        [StringLength(500)]
        public string status { get; set; }

        [RequiredAttribute]
        public decimal amountRefunded { get; set; }

        public DateTime refundedDate { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [RequiredAttribute]
        public bool isMailSent { get; set; }

        public DateTime tdrSentDate { get; set; }

        [StringLength(25)]
        public string confirmed { get; set; }
    }
}