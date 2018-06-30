
App.service("PendingTicketsService", function ($http, ShareData) {
 this.GetPendingTickets = function (bookedTicketFilter) {
     debugger;
        var request = $http({            
            method: "post",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/History/Ticket/List",
            data:bookedTicketFilter
        });
        debugger;
        return request;
        
    };

    this.GetTicketDetail = function (ticketOrderId) {
        var request = $http({            
            method: "get",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/History/Ticket/Details?ticketOrderId="+ticketOrderId
        });
        return request;
        
    };


    this.GetTicketPassengerDetail = function (ticketOrderId) {
        var request = $http({            
            method: "get",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/History/Ticket/PassengerList?ticketOrderId="+ticketOrderId
        });
        return request;
        
    };

});
