using System.ComponentModel.DataAnnotations;

namespace Mpower.Rail.Model.Request
{
    public class UserRegistration
    {
        [RequiredAttribute]
        public string merchantAccount { get; set; }

        [RequiredAttribute]
        [EmailAddressAttribute]
        public string email { get; set; }
        
        [RequiredAttribute]
        public string password{ get; set; }
        
        [RequiredAttribute]
        public string merchantId { get; set; }
                
        public string userId { get; set; }
        
        [RequiredAttribute]
        public string subUserId { get; set; }
        
        [RequiredAttribute]
        public string subUserPassword { get; set; }
        
        [RequiredAttribute]
        public string digitalCertificate { get; set; }

        [RequiredAttribute]
        public string macId { get; set; }

        [RequiredAttribute]
        [Range(1000000000,999999999999)]

        public string mobileNo { get; set; }

        [RequiredAttribute]
        public string panCard { get; set; }

        [RequiredAttribute]
        public string deviceId { get; set; }
    }

}