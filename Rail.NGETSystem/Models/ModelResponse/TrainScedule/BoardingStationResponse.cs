using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mpower.Rail.NGETSystem.Models.Response;

namespace Mpower.Rail.NGETSystem.Models.Response
{


public class BoardingStationList
{
    public string stnNameCode { get; set; }
}

public class BoardingStationResponse
{
    public List<BoardingStationList> boardingStationList { get; set; }
    public string serverId { get; set; }
    public string timeStamp { get; set; }
}
}