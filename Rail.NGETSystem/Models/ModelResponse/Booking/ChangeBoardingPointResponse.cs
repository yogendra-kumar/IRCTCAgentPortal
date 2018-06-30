namespace Mpower.Rail.NGETSystem.Models.Response
{
    /// <summary>
    /// BookingConfiguration
    /// </summary>
public abstract class BoardingPoint
{
    public string newBoardingPoint { get; set; }
    public string pnr { get; set; }
    public string serverId { get; set; }
    public string success { get; set; }
    public string timeStamp { get; set; }
}
public class ChanageBoardingPointErrResponse:BoardingPoint
{
    public string error { get; set; }
    // public string newBoardingPoint { get; set; }
    // public string pnr { get; set; }
    // public string serverId { get; set; }
    // public string success { get; set; }
    // public string timeStamp { get; set; }
}


public class ChanageBoardingPointResponse:BoardingPoint
{
    public string newBoardingDate { get; set; }
   // public string newBoardingPoint { get; set; }
    public string oldBoardingPoint { get; set; }
   // public string pnr { get; set; }
   // public string serverId { get; set; }
    public string status { get; set; }
   // public string success { get; set; }
    //public string timeStamp { get; set; }
}
}