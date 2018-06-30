using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mpower.Rail.Model.EntityBase;

namespace Mpower.Rail.Model.Rail
{
    public class UserRegistration : IEntityBase, IDisposable
    {
        void IDisposable.Dispose()
        {
            GC.SuppressFinalize(this);
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public System.Int64 Id { get; set; }

        [StringLengthAttribute(20)]
        public string merchantAccount { get; set; }

        // public long transactions { get; set; }

        [StringLengthAttribute(50)]
        public string email { get; set; }

        public bool isActive { get; set; }

        [RequiredAttribute]
        [StringLengthAttribute(250)]
        public string merchantId { get; set; }

        [RequiredAttribute]
        public string UserId { get; set; }

        // public DateTime date { get; set; }

        //public long merchantType { get; set; }

        [StringLengthAttribute(20)]
        public string subUserId { get; set; }

        [StringLengthAttribute(20)]
        public string subUserPassword { get; set; }

        // [StringLengthAttribute(20)]
        // public string accountNumber { get; set; }

        // [StringLengthAttribute(20)]
        // public string accountPassword { get; set; }

        // public bool correct { get; set; }

        // public bool isB2B { get; set; }

        // [StringLengthAttribute(20)]
        // public string userPassword { get; set; }

        // [StringLengthAttribute(119)]
        // public string corporateName { get; set; }

        // [StringLengthAttribute(20)]
        // public string firstName { get; set; }

        // [StringLengthAttribute(20)]
        // public string middleName { get; set; }

        // [StringLengthAttribute(20)]
        // public string lastName { get; set; }

        // [StringLengthAttribute(10)]
        // public string gender { get; set; }

        // [StringLengthAttribute(10)]
        // public string maritalStatus { get; set; }

        // public Int16 dob_day { get; set; }

        // public Int16 dob_month { get; set; }

        // public Int16 dob_year { get; set; }

        // [StringLengthAttribute(50)]
        // public string occupation { get; set; }

        // [StringLengthAttribute(20)]
        // public string shippingAddressPref { get; set; }
        
        // [StringLengthAttribute(149)]
        // public string residAddress { get; set; }

        // [StringLengthAttribute(18)]
        // public string residCity { get; set; }

        // [StringLengthAttribute(18)]
        // public string residState { get; set; }

        // [StringLengthAttribute(25)]
        // public string residCountry { get; set; }

        // [StringLengthAttribute(6)]
        // public string residPin { get; set; }

        // [StringLengthAttribute(10)]
        // public string residPhone { get; set; }

        // [StringLengthAttribute(149)]
        // public string officeAddress { get; set; }

        // [StringLengthAttribute(18)]
        // public string officeCity { get; set; }

        // [StringLengthAttribute(18)]
        // public string officeState { get; set; }

        // [StringLengthAttribute(25)]
        // public string officeCountry { get; set; }

        // [StringLengthAttribute(6)]
        // public string officePin { get; set; }
        
        // [StringLengthAttribute(10)]
        // public string officePhone { get; set; }

        // public bool pnrEmailAlerts { get; set; }

        // [StringLengthAttribute(200)]
        // public string forgotPswdQn { get; set; }

        // [StringLengthAttribute(20)]
        // public string forgotPswdAns { get; set; }

        // [StringLengthAttribute(100)]
        // public string idType { get; set; }

        // [StringLengthAttribute(20)]
        // public string idNumber { get; set; }

        // public bool isOxiSmart { get; set; }

        [StringLengthAttribute(50)]
        public string digitalCertificate { get; set; }

        [StringLengthAttribute(50)]
        public string macId { get; set; }

        [StringLengthAttribute(12)]
        public string mobNo { get; set; }

        public bool isIRCTCActivated { get; set; }

        [StringLengthAttribute(20)]
        public string pancard { get; set; }

        [StringLengthAttribute(50)]
        public string deviceId { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

    }
}