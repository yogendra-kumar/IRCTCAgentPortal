(function () {
'use strict';
    var BookedTicketServiceId = 'BookedTicketService';
    angular.module('App').service(BookedTicketServiceId, ['$rootScope', 'ShareData', '$http', '$q', BookedTicketServiceFunc]);

    function BookedTicketServiceFunc($rootScope, ShareData, $httpProvider, $q) {
        var service = {
            GetBookedTickets : null,
            GetDetails: null,
            GetPassengerDetails:null
        };

        service.GetBookedTickets = function (requestData,successCallBack, failureCallBack) 
        {
            var deferred = $q.defer();
            $httpProvider({
                method: 'POST',
                url: ShareData.OxiRailApiUrl + "/Mpower/Rail/History/Ticket/List",
                data: requestData
            }).success(function (data, status, headers, config) {
                successCallBack('getBookedTickets', data);
                deferred.resolve();
            }).error(function (data, status, headers, config) {
                if (data) {
                    failureCallBack('getBookedTickets', data);
                    return;
                }
                failureCallBack("An internal processing error occurred.");
                deferred.resolve();
            });
            return deferred.promise;
       };

       service.GetDetails = function (id,successCallBack, failureCallBack) {
            var deferred = $q.defer();
            $httpProvider({
                method: 'GET',
                url: ShareData.OxiRailApiUrl+"/Mpower/Rail/History/Ticket/Details?ticketOrderId="+id,
                data: {
                   
                }
            }).success(function (data, status, headers, config) {
               if (data) {
                    successCallBack('getDetails', data);
                }
                else {
                  
                }
               deferred.resolve();

            }).error(function (data, status, headers, config) {
                if (data) {
                     failureCallBack('getDetails', data);
                }
                deferred.resolve();
            });

            return deferred.promise;
        };

        service.GetPassengerDetails = function (id,successCallBack, failureCallBack) {
            var deferred = $q.defer();
            $httpProvider({
                method: 'GET',
                url: ShareData.OxiRailApiUrl+"/Mpower/Rail/History/Ticket/PassengerList?ticketOrderId="+ id,
                data: {
                   
                }
            }).success(function (data, status, headers, config) {
               if (data) {
                    successCallBack('getPassengerDetails', data);
                }
                else {
                  
                }
               deferred.resolve();

            }).error(function (data, status, headers, config) {
                if (data) {
                     failureCallBack('getPassengerDetails', data);
                }
                deferred.resolve();
            });

            return deferred.promise;
        };

        return service;
    }

})();