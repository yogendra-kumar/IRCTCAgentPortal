App.service("TDRHistoryService", function ($http, ShareData) {
//Function to Retrive Passenger details 
    this.GetTdrTickets = function (roid) {
        var request = $http({            
            method: "get",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/History/TDR/Details?RoId="+roid
        });
        return request;
        
    };
});