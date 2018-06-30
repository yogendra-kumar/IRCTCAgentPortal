using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Ticket
{

    public class TicketPassengers : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }
        
        [RequiredAttribute]
        public System.Int64 ticketOrderId { get; set; }
        
        [RequiredAttribute]
        [StringLengthAttribute(16)]
        public string name { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(1)]        
        public string sex { get; set; }
        
        [RequiredAttribute]
        public int age { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(15)]
        public string berthPreference { get; set; }

        [StringLengthAttribute(15)]
        public string foodPreference { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(15)]
        public string concessionCode { get; set; }        
        
        [RequiredAttribute]
        public int idCardType { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(16)]
        public string idCardNumber { get; set; }

        [StringLengthAttribute(16)]
        public string firstChildName { get; set; }
        
        public int firstChildAge { get; set; }

        [StringLengthAttribute(1)]
        public string firstChildSex { get; set; }

        [StringLengthAttribute(16)]
        public string secondChildName { get; set; }
        
        public int secondChildAge { get; set; }

        [StringLengthAttribute(1)]
        public string secondChildSex { get; set; }
        
        [StringLengthAttribute(15)]
        public string bedRoll { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(25)]
        public string bookingStatus { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(25)]
        public string coach { get; set; }
        
        [RequiredAttribute]
        [StringLengthAttribute(25)]
        public string seat { get; set; }
        
        [RequiredAttribute]
        [StringLengthAttribute(25)]
        public string berth { get; set; }
        
        [RequiredAttribute]
        [StringLengthAttribute(25)]
        public string currentStatus { get; set; }
  
    }
}