App.controller('PopularStationController', function ($scope, $location, ShareData, PopularStationService) {
    debugger;
    $scope.getPopularStationResponse = null;
    var PopularStationResponse = PopularStationService.GetPopularStations();

    PopularStationResponse.then(function (getData) {
        debugger;
        console.log(getData);
        $scope.getPopularStationResponse=getData.data.responseResult;
    })
    
})