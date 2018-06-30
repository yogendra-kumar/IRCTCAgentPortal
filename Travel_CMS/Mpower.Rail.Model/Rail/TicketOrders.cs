using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class TicketOrders : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLengthAttribute(5)]
        public string srcStn { get; set; }

        [StringLengthAttribute(5)]
        public string destStn { get; set; }

        [StringLengthAttribute(50)]
        public string TrainNum { get; set; }

        [StringLengthAttribute(50)]
        public string TrainName { get; set; }

        [StringLengthAttribute(5)]
        public string Class { get; set; }

        [StringLengthAttribute(5)]
        public string Resvupto { get; set; }

        [StringLengthAttribute(5)]
        public string Brdpt { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        [StringLengthAttribute(5)]
        public string Quota { get; set; }

        [StringLengthAttribute(16)]
        public string PsgName1 { get; set; }

        [StringLengthAttribute(1)]
        public string PsgSex1 { get; set; }
        public int PsgAge1 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgBirthPref1 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgFoodPref1 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgConcession1 { get; set; }

        [StringLengthAttribute(16)]
        public string PsgName2 { get; set; }

        [StringLengthAttribute(1)]
        public string PsgSex2 { get; set; }
        public int PsgAge2 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgBirthPref2 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgFoodPref2 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgConcession2 { get; set; }

        [StringLengthAttribute(16)]
        public string PsgName3 { get; set; }

        [StringLengthAttribute(1)]
        public string PsgSex3 { get; set; }
        public int PsgAge3 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgBirthPref3 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgFoodPref3 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgConcession3 { get; set; }

        [StringLengthAttribute(16)]
        public string PsgName4 { get; set; }

        [StringLengthAttribute(1)]
        public string PsgSex4 { get; set; }
        public int PsgAge4 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgBirthPref4 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgFoodPref4 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgConcession4 { get; set; }

        [StringLengthAttribute(16)]
        public string PsgName5 { get; set; }

        [StringLengthAttribute(1)]
        public string PsgSex5 { get; set; }
        public int PsgAge5 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgBirthPref5 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgFoodPref5 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgConcession5 { get; set; }

        [StringLengthAttribute(16)]
        public string PsgName6 { get; set; }

        [StringLengthAttribute(1)]
        public string PsgSex6 { get; set; }
        public int PsgAge6 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgBirthPref6 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgFoodPref6 { get; set; }

        [StringLengthAttribute(15)]
        public string PsgConcession6 { get; set; }

        [StringLengthAttribute(15)]
        public string PmtGatewayName { get; set; }

        [StringLengthAttribute(15)]
        public string PmtCardType { get; set; }

        [StringLengthAttribute(20)]
        public string AccountNumber { get; set; }
        public decimal IRCTCServiceCharge { get; set; }

        [StringLengthAttribute(15)]
        public string OperatorCode { get; set; }

        [StringLengthAttribute(50)]
        public string MobileNo { get; set; }

        [StringLengthAttribute(1)]
        public string CarryPsg { get; set; }

        [StringLengthAttribute(16)]
        public string IdCardNumber { get; set; }

        [StringLengthAttribute(15)]
        public string IdCardType { get; set; }

        [StringLengthAttribute(15)]
        public string IdIssAth { get; set; }

        [StringLengthAttribute(50)]
        public string PaymentStatus { get; set; }
        public long PaymentGateways { get; set; }
        public long Sessions { get; set; }
        public System.DateTime Date { get; set; }
        public decimal TicketAmt { get; set; }
        public decimal TotalAmt { get; set; }
        public decimal OxigenServiceCharge { get; set; }

        [StringLengthAttribute(50)]
        public string Email { get; set; }

        [StringLengthAttribute(50)]
        public string LoginAccountNo { get; set; }
        public long MediatorID { get; set; }

        [StringLengthAttribute(50)]
        public string BookingSource { get; set; }

        [StringLengthAttribute(16)]
        public string FirstChildName { get; set; }
        public int FirstChildAge { get; set; }

        [StringLengthAttribute(1)]
        public string FirstChildSex { get; set; }

        [StringLengthAttribute(16)]
        public string SecondChildName { get; set; }
        public int SecondChildAge { get; set; }

        [StringLengthAttribute(1)]
        public string SecondChildSex { get; set; }

        [StringLengthAttribute(1)]
        public string ReservationChoice { get; set; }

        [StringLengthAttribute(1)]
        public string BookWithTatkal { get; set; }

        [StringLengthAttribute(15)]
        public string psgbed1 { get; set; }

        [StringLengthAttribute(15)]
        public string psgbed2 { get; set; }

        [StringLengthAttribute(15)]
        public string psgbed3 { get; set; }

        [StringLengthAttribute(15)]
        public string psgbed4 { get; set; }

        [StringLengthAttribute(15)]
        public string psgbed5 { get; set; }

        [StringLengthAttribute(15)]
        public string psgbed6 { get; set; }

        [StringLengthAttribute(500)]
        public string TicketVerificationkey { get; set; }

        [StringLengthAttribute(20)]
        public string TPRequest { get; set; }

        [StringLengthAttribute(20)]
        public string TrainType { get; set; }

        [StringLengthAttribute(100)]
        public string PsgAddress { get; set; }

        [StringLengthAttribute(50)]
        public string IdCardType1 { get; set; }

        [StringLengthAttribute(16)]
        public string IdCardNumber1 { get; set; }

        [StringLengthAttribute(50)]
        public string IdCardType2 { get; set; }

        [StringLengthAttribute(16)]
        public string IdCardNumber2 { get; set; }

        [StringLengthAttribute(50)]
        public string IdCardType3 { get; set; }

        [StringLengthAttribute(16)]
        public string IdCardNumber3 { get; set; }

        [StringLengthAttribute(50)]
        public string IdCardType4 { get; set; }

        [StringLengthAttribute(16)]
        public string IdCardNumber4 { get; set; }

        [StringLengthAttribute(20)]
        public string IRCTCTxId { get; set; }
    }
}