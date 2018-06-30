namespace Mpower.Rail.NGETSystem.Models.Request
{
    /// <summary>
    /// passengerlist for BookingEnquiry related to booking purpose
    /// </summary>
    public class passengerList
    {
        public string concessionOpted { get; set; }
        public string passengerConcession { get; set; } //example as "NOCONC"
        public string passengerAge { get; set; }
        public string passengerBedrollChoice { get; set; }
        public string passengerGender { get; set; } //M OR F
        public string passengerIcardFlag { get; set; } //true or false
        public string passengerCardNumber { get; set; } = "";
        public string passengerCardType { get; set; } // for example DRIVING_LICENSE 
        public string passengerName { get; set; }
        public string passengerSerialNumber { get; set; }
        public string childBerthFlag { get; set; } //true or false
        public string passengerNationality { get; set; } // as IN
    }
}