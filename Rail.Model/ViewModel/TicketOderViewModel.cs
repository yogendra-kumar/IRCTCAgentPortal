using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Mpower.Rail.Model.EntityBase;
using Mpower.Rail.Model.Ticket;

namespace Mpower.Rail.Model.ViewModel
{
    public class TicketOrderViewModel : IEntityBase, IDisposable
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
        public string pnrNumber { get; set; } 

         [RequiredAttribute]
        public string transactionId { get; set; }

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
        public int bordingDay { get; set; }

        [RequiredAttribute]
        public int bordingMonth { get; set; }

        [RequiredAttribute]
        public int bordingYear { get; set; }

        [RequiredAttribute]
        public string ticketStatus {get;set;}

        [RequiredAttribute]
        public string quota {get;set;}

        public InsuranceDetails ticketInsurance {get;set;}

        [RequiredAttribute]
        public string paymentGatewayName { get; set; }

        [RequiredAttribute]
        public decimal ticketFare { get; set; }

         [RequiredAttribute]
        public decimal irctcServiceCharge { get; set; }

         [RequiredAttribute]
        public decimal pgCharges { get; set; }

          [RequiredAttribute]
        public decimal roCommissionOnPGCharge { get; set; }

         [RequiredAttribute]
        public decimal oxigenServiceCharge { get; set; }

         [RequiredAttribute]
        public decimal totalFare { get; set; }

        public List<RefundHistoryViewModel> refundHistoryList {get;set;}

    }
}