using System;
using System.ComponentModel.DataAnnotations;

namespace Mpower.Rail.Model.Request
{
    public class UserUpdate
    {        
        [RequiredAttribute]
        public string merchantId { get; set; }
        
        [RequiredAttribute]
        public string userId { get; set; }        
        
        [RequiredAttribute]
        public Boolean active { get; set; }
    }
    
}