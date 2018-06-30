
App.controller('PendingTicketsController', function ($scope, $location, ShareData, PendingTicketsService) {

    $scope.status = 'Pending';
    $scope.roid = 1122334400;
    $scope.ticketNumber = 21000004176;
    $scope.TicketList = null;
    $scope.pageSize = 5;
    $scope.totalRecordCount = 16;
    $scope.pageIndex = 1;

    $scope.ticketListmodal = null;
    $scope.ticketDetailData = null;
    $scope.passengerDetailData = null;

    //   //Function to get dummy Pending Tickets
    //  $scope.tabledata=[{"From":"NDLS","To":"BSB","BookedTime":"9/11/2016","Txnno":"343434341","Pnr":"22222222222211","journeydate":"23/11/2016","status":"Pending"},
    //                          
    //   ]

    //   $scope.ticketDetailData={"PNRNumber":"2532222457","TransactionID":"22222222222211","TrainNumber":"12560","TrainName":"Shiv Ganga Express",
    //                             "From":"NDLS","To":"BSB","JourneyDate":"23/11/2016","Class":"3A","Boarding":"NDLS","ReservationUpto":"BSB",
    //                             "Quota":"General","BoardingDate":"23/11/2016","Status":"Booked","TravelInsuranceOpted":"Yes",
    //                             "PolicyStatus":"In process","PolicyIssueDate":"","InsuranceCompany":"Shriram General Insurance",
    //                             "InsuranceCompanyURL":"https://www.shriramgeneralinsurance.in/sgi/insurance-irctc-nominee.html?TXNId=200000056884265",
    //                             "PaymentOption":"Oxigen","TicketCharge":"260.00","IRCTCServiceCharge":"23.00","BankPGCharge":"2.12",
    //                             "ROCommission":"1.42","OxirailServiceCharge":"20.00","TravelInsuranceCharge":"0.01","Total":"303.71"};

    //   $scope.passengerDetailData=[{"Name":"Ravi Prakash Rai","Age":"26","Sex":"M","CoachNo":"B3/46/CNF","Bearth":"UB"},
    //                               {"Name":"Jai Singh","Age":"28","Sex":"M","CoachNo":"B3/47/CNF","Bearth":"SL"},
    //                               {"Name":"Alok","Age":"34","Sex":"M","CoachNo":"B3/48/CNF","Bearth":"SU"},
    //                               {"Name":"Saurabh","Age":"27","Sex":"M","CoachNo":"B3/49/CNF","Bearth":"LB"}
    //   ];
    debugger;
    var bookedTicketFilter = {
        agentAccountNo: ShareData.LoginAccount,
        pnr: "",
        ticketNumber: "",
        fromStation: "",
        toStation: "",
        dateOfJourney: "1900/01/01",
        ticketStatus: 1,
        pageIndex: 1,
        pages: 20

    };

    //  getPendingTicket();
    // var getPendingTicket=function () { 
    //debugger;
    var response = PendingTicketsService.GetPendingTickets(bookedTicketFilter);
    response.then(function (pl) {
        debugger;
        if (pl.data.responseResult != null) {
            $scope.tabledata = pl.data.responseResult;
        }
        else {
            $scope.Error = true;
            $scope.Message = 'No Record Found!';
            $scope.showModal = true;
        }
    },
        function (errorPl) {
            $scope.error = 'Error occurred, Please try again.', errorPl;
        });

    //  }

    var GetTicketDetail = function (ticketOrderId) {
        // debugger;               
        var response = PendingTicketsService.GetTicketDetail(ticketOrderId);
        response.then(function (pl) {
            //  debugger;
            if (pl.data.responseResult != null) {
                $scope.ticketDetailData = pl.data.responseResult;
            }
            else {
                $scope.Error = true;
                $scope.Message = 'No Record Found!';
                $scope.showModal = true;
            }
        },
            function (errorPl) {
                $scope.error = 'Error occurred, Please try again.', errorPl;
            });

    }

    var GetTicketPassengerDetail = function (ticketOrderId) {
        // debugger;               
        var response = PendingTicketsService.GetTicketPassengerDetail(ticketOrderId);
        response.then(function (pl) {
            //  debugger;
            if (pl.data.responseResult != null) {
                $scope.passengerDetailData = pl.data.responseResult;
            }
            else {
                $scope.Error = true;
                $scope.Message = 'No Record Found!';
                $scope.showModal = true;
            }
        },
            function (errorPl) {
                $scope.error = 'Error occurred, Please try again.', errorPl;
            });

    }
    $scope.OpenPopup = function (ticket) {
        $scope.Header = "Ticket Details"
        $scope.labelAlert = "";
        $scope.disable = true;
        GetTicketDetail(ticket.ticketOrderId);
        GetTicketPassengerDetail(ticket.ticketOrderId);
        $("#ticketModal").modal();
    }
});

