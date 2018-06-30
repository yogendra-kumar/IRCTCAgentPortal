
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mpower.Rail.Model.Request
{
    public class TravelList
    {
        [RequiredAttribute]
        public string loginAccount { get; set; }
        [RequiredAttribute]
        public string  listName { get; set; }
        [RequiredAttribute]
        public string description { get; set; }
        [RequiredAttribute]
        public List<long> passengerIds { get; set; }
    }   
}