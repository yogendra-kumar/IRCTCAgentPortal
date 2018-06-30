
using System.ComponentModel.DataAnnotations;

namespace Mpower.Rail.Model.Request
{
    public class Passenger
    {
        public long passengerId { get; set; }
        [RequiredAttribute]
        public string loginAccount { get; set; }
        [RequiredAttribute]
        public string name { get; set; }
        [RequiredAttribute]
        [Range(1,31)]
        public int bDay { get; set; }
        [RequiredAttribute]
        [Range(1,12)]
        public int bMonth { get; set; }
        [RequiredAttribute]
        [Range(1900,2200)]
        public int bYear { get; set; }
        [RequiredAttribute]
        public string sex { get; set; }
        [RequiredAttribute]
        public string birthPreferance { get; set; }
        [RequiredAttribute]
        public string foodPreferance { get; set; }

        [RequiredAttribute]
        public long idCardTypeId{get;set;}

       
        public string idCardNumber{get;set;}
        [RequiredAttribute]
        public bool senior { get; set; }
    }
}