using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Ticket
{
    public class TicketOrders : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(5)]
        public string sourceStation { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(5)]
        public string destStation { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(50)]
        public string trainNumber { get; set; }
        
        [RequiredAttribute]
        [StringLengthAttribute(50)]
        public string trainName { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(5)]
        public string Class { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(5)]
        public string reserveUpto { get; set; }

        [StringLengthAttribute(5)]
        public string bordingPoint { get; set; }

        [RequiredAttribute]
        public int journeyDay { get; set; }

        [RequiredAttribute]
        public int journeyMonth { get; set; }

        [RequiredAttribute]
        public int journeyYear { get; set; }

        [RequiredAttribute]
        public int quota { get; set; }

        [RequiredAttribute]      
        [StringLengthAttribute(15)]
        public string pmtGatewayName { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(15)]
        public string pmtCardType { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(20)]
        public string accountNumber { get; set; }

        [RequiredAttribute]
        public decimal irctcServiceCharge { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(15)]
        public string operatorCode { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(15)]
        public string mobileNo { get; set; }

        // [StringLengthAttribute(1)]
        // public string CarryPsg { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(16)]
        public string idCardNumber { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(15)]
        public int idCardType { get; set; }        

        [RequiredAttribute]
        public int ticketStatus { get; set; }

        [RequiredAttribute]
        public long paymentGateway { get; set; }

        [RequiredAttribute]
        public long session { get; set; }
        
        public System.DateTime bookingDate { get; set; } = DateTime.Now;
        
        [RequiredAttribute]
        public decimal ticketAmt { get; set; }

        [RequiredAttribute] 
        public decimal totalAmt { get; set; }      
        
        [RequiredAttribute]
        public decimal oxigenServiceCharge { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(50)]
        public string email { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(50)]
        public string loginAccountNo { get; set; }
        //public long MediatorID { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(50)]
        public string bookingSource { get; set; }
        
        [RequiredAttribute]
        [StringLengthAttribute(1)]
        public string reservationChoice { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(1)]
        public string isTatkal { get; set; }        
        
        [RequiredAttribute]
        [StringLengthAttribute(500)]
        public string ticketVerificationKey { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(100)]
        public string psgAddress { get; set; }
        
        [RequiredAttribute]
        [StringLengthAttribute(20)]
        public string irctcTxnNumber { get; set; }

        // [RequiredAttribute]
        // [StringLengthAttribute(25)]
        // public string bookingStatus { get; set; }

        // [RequiredAttribute]
        // [StringLengthAttribute(25)]
        // public string coach { get; set; }
        
        // [RequiredAttribute]
        // [StringLengthAttribute(25)]
        // public string seat { get; set; }
        
        // [RequiredAttribute]
        // [StringLengthAttribute(25)]
        // public string berth { get; set; }
        
        // [RequiredAttribute]
        // [StringLengthAttribute(25)]
        // public string currentStatus { get; set; }

    }
}