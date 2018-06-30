using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Ticket
{
    public class BookedTickets : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        
        //<summary> Relation with Id field of Ticket Order model.</summary>
        public Int64 ticketOrderId { get; set; }

        [RequiredAttribute]
        public Int64 transactionId { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(25)]
        public string ticketNumber { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(25)]
        public string pnrNumber { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(25)]
        public string bordingPoint { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(50)]
        public string departure { get; set; }

        [RequiredAttribute]
        public int bordingDay { get; set; }

        [RequiredAttribute]
        public int bordingMonth { get; set; }

        [RequiredAttribute]
        public int bordingYear { get; set; }

        [RequiredAttribute]
        public int distance { get; set; }

        [RequiredAttribute]
        public decimal ticketFare { get; set; }

        [RequiredAttribute]
        public decimal totalFare { get; set; }

        [RequiredAttribute]
        public decimal irctcServiceCharge { get; set; }

        [RequiredAttribute]
        public decimal oxigenBalance { get; set; }

        [RequiredAttribute]
        public DateTime bookedOn { get; set; } = DateTime.Now;

        [RequiredAttribute]
        [StringLengthAttribute(500)]
        public string slipRouteMessage { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(100)]
        public string serverIP { get; set; }

        [RequiredAttribute]
        public DateTime serverDateTime { get; set; } = DateTime.Now;

        [RequiredAttribute]
        public decimal pgCharges { get; set; }

        [RequiredAttribute]
        public decimal roCommissionOnPGCharge { get; set; }

        [RequiredAttribute]
        //<summary> true if Identity card is submitted.</summary>
        public bool idIssueAuth { get; set; }
    }
}