public class apiRequest
{
    public TrainBtwStationRequest request { get; set; }
    public string userSession { get; set; }

}
public class TrainBtwStationRequest
{
    public string trainFrom { get; set; }
    public string trainTo { get; set; }
    public string enquiryForDate { get; set; }
}