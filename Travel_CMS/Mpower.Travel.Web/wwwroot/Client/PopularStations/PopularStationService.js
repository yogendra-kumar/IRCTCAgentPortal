App.service('PopularStationService', function ($http,ShareData) {
    this.GetPopularStations = function () {
        debugger;
        var request = $http({            
            method: "get",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/Irctc/Station/List",
        });
        debugger;
        console.log(request);
        return request;
    }
})