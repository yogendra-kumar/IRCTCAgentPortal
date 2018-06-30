using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Ticket
{
    public class TicketCancellationRequests : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        public long bookedTicketId { get; set; }

        [StringLength(25)]
        public string ticketNumber { get; set; }

        public bool passenger1 { get; set; }

        public bool passenger2 { get; set; }

        public bool passenger3 { get; set; }

        public bool passenger4 { get; set; }

        public bool passenger5 { get; set; }

        public bool passenger6 { get; set; }

        public DateTime date { get; set; }

        public long sessions { get; set; }

    }
}