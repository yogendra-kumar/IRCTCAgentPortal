namespace Mpower.Rail.NGETSystem.Models.Response
{
    /// <summary>
    /// train availability status on day basis
    /// </summary>
    public class TrainAvailabilityDay
    {
        public string availablityDate { get; set; }
        public string availablityStatus { get; set; }
        public string availablityType { get; set; }
        public string currentBkgFlag { get; set; }
        public string reason { get; set; }
        public string reasonType { get; set; }
    }
}