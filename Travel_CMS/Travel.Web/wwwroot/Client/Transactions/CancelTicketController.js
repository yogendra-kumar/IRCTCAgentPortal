
App.controller('CancelTicketController', function ($scope, $location, ShareData, CancelTicketService) {

    $scope.title = 'Cancel Ticket';

    debugger;
    var ticketFilterRequest = {
        agentAccountNo: ShareData.LoginAccount,
        pnr: "",
        ticketNumber: "",
        fromStation: "",
        toStation: "",
        dateOfJourney: "1900/01/01",
        ticketStatus: 4,
        pageIndex: 1,
        pages: 20

    };

    var response = CancelTicketService.GetCancelTicketList(ticketFilterRequest);
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

    var GetTicketDetail = function (ticketOrderId) {
        // debugger;               
        var response = CancelTicketService.GetTicketDetail(ticketOrderId);
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
        var response = CancelTicketService.GetTicketPassengerDetail(ticketOrderId);
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

