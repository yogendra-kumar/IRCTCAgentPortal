using System;
using System.ComponentModel.DataAnnotations;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.ViewModel
{
    public class BookedTicketViewModel : IEntityBase, IDisposable
    {
         void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [RequiredAttribute]
        public Int64 Id {get;set;}

        [RequiredAttribute]
        public System.Int64 ticketOrderId { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(25)]
        public string ticketNumber { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(25)]
        public string pnrNumber { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(5)]
        public string sourceStation { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(5)]
        public string destStation { get; set; }

          [RequiredAttribute]
        public int journeyDay { get; set; }

        [RequiredAttribute]
        public int journeyMonth { get; set; }

        [RequiredAttribute]
        public int journeyYear { get; set; }

        [RequiredAttribute]
        public DateTime bookedOn { get; set; } = DateTime.Now;

        [RequiredAttribute]
        public string status {get;set;}

        [RequiredAttribute]
        public string irctcTxnNumber {get;set;}
    }
}