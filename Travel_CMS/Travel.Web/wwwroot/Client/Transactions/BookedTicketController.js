App.controller("BookedTicketController", ['$scope', '$location', 'ShareData', 'BookedTicketService', '$sce', '$routeParams', '$filter', function ($scope, $location, ShareData, BookedTicketService, $sce, $routeParams, $filter) {
    $scope.title = 'Search Transactions';
    $scope.ticketNumber = "";
    $scope.pnrNumber = "";
    $scope.fromStation = "";
    $scope.toStation = "";
    $scope.journyDate = "";
    $scope.Detail = null;
    $scope.Passenger = null;
    $scope.ticketTypeItem = null;
    $scope.getBookedTickets = function() 
    {
        var date = $scope.journyDate.split("/");
        var filterRequest = {
            "agentAccountNo": "saf324",
            "pnr": $scope.pnrNumber,
            "ticketNumber": $scope.ticketNumber,
            "fromStation": $scope.fromStation,
            "toStation": $scope.toStation,
            "dateOfJourney": (date[2] + "/" + date[1] + "/" + date[0]),
            "ticketStatus": $scope.selectedticketType,
            "pageIndex": 1,
            "pages": 25
        };
        var PromiseBookedTicketList = BookedTicketService.GetBookedTickets(filterRequest,successCallBack, failureCallBack);
    };
    
    $scope.getDetail = function (id) {
        var PromiseGetDetail = BookedTicketService.GetDetails(id,successCallBack, failureCallBack);
    };

    $scope.changeBoardingPoint = function () {
        alert("Coming Soon");
    };
    $scope.cancelTicket= function () {
        alert("Coming Soon");
    };
    $scope.getPNRStatus= function () {
        alert("Coming Soon");
    };
    $scope.printETicket= function () {
        alert("Coming Soon");
    };
    $scope.getSMS= function () {
        alert("Coming Soon");
    };

    $scope.ResetAll = function () {
        $scope.ticketNumber = "";
        $scope.pnrNumber = "";
        $scope.fromStation = "";
        $scope.toStation = "";
        $scope.journyDate = "1900/01/01";
        $scope.ticketType = 2;
    };

    $scope.ticketTypeChanged = function()
    {
        console.log("Selected Type: " + $scope.selectedticketType);            
    };
   
    function successCallBack(call, data) {
            switch (call) {
                case 'getBookedTickets':
                debugger;
                    if(data.responseCode === "0")
                    {
                        $scope.isResponseFail = false;
                        $scope.BookedTicketList = data.responseResult;
                    }
                    else
                    {
                        $scope.isResponseFail = true;
                        $scope.responseMessage = data.responseMessage;
                        $scope.BookedTicketList = null;
                    }
                    break;

                case 'getDetails':
                debugger;
                    if(data.responseCode == 0)
                    {
                        var PromiseGetPassengerDetail = BookedTicketService.GetPassengerDetails(data.responseResult.id,successCallBack, failureCallBack);
                        $scope.Detail = data.responseResult;
                        $scope.Header = "Booked Ticket Details"
                        $scope.labelAlert = "";
                        $scope.disable = true;
                      
                    }
                    break;

                case 'getPassengerDetails':
                debugger;
                    data = data;
                    if(data.responseCode == 0)
                    {
                        $scope.Passenger = data.responseResult;
                        if($scope.Detail!=null && $scope.Passenger!=null)
                        {
                            $("#ticketModal").modal();
                        }
                    }
                    break;
            }
        };
        function failureCallBack(call, data) {
            switch (call) {
                case 'getBookedTickets':
                    console.log('getBookedTickets' + data);                 
                    break;
                case 'getDetails':
                    console.log('getDetails' +data);
                    break;
                case 'getPassengerDetails':
                    console.log('getPassengerDetails' + data);
                    break;
            }
        };

        activate();
        function activate() {
            $scope.ticketTypeItem = 
            [
                {"value":"2", "text":"Booked" },
                {"value":"3", "text":"Orderd" },
                {"value":"5", "text":"Cancelled/Modified" }
            ];
            $scope.ResetAll();
            $scope.selectedticketType = 2;
            $scope.getBookedTickets();
        };


    $(function () {
        $('div[name="datetimepicker1"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            // maxDate: new Date
        },
            function (start, end, label) {
                var years = moment().diff(start, 'years');
                $('input[name="date"]').val(this.startDate.format('DD/MM/YYYY'))
                $scope.journyDate = this.startDate.format('DD/MM/YYYY');
            });
    });

    $('#ticketModal').on('hidden.bs.modal', function () {
        $('body').removeClass('modal-open');
        $('body').removeAttr('style');
    });
    $('#ticketModal').on('shown.bs.modal', function () {
    $('body').addClass('modal-open');
    });
}]);

