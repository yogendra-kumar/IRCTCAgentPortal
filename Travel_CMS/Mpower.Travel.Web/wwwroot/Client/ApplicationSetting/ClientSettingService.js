App.service("ClientSettingService", function ($http, $q, ShareData) {

    //Fundction to Read Application based upon id
    this.getApplicationView = function () {
        var Route = ShareData.ApiUrl + "/Configuration/ApplicationSettings/" + ShareData.AppId;
        return $http.get(Route);
        // return $http.get("http://localhost:5001/Application/View/" + id);

    };

    this.getPageSetting = function (id) {
        var Route = ShareData.ApiUrl + "/Configuration/PageSetting/" + id;
        return $http.get(Route);
    };

    this.getAppMenu = function () {
        var Route = ShareData.ApiUrl + "/Configuration/ClientPageViewList/" + ShareData.AppId;
        return $http.get(Route);
    };
    this.getTrainDetailwithN = function (request) {
        var response = $http({
            method: "post",
            url:ShareData.OxiRailApiUrl+"/Mpower/Rail/Booking/TrainFareEnquiry",
            data: request
        });
        return response;
    }
    this.getTrainDetailY = function (request) {
        var response = $http({
            method: "post",
            url:"Home/TrainDetailY",
            data: request
        });
        return response;
    }
    this.getTrainBtwnStations = function (request) {
        var response = $http({
            method: "post",
            url:"Home/TrainBtwnRoute",
            data: request
        });
        return response;
    }
    this.getStation=function(key)
    {
         var response = $http({
            method: "get",
            url:ShareData.OxiRailApiUrl+"/Mpower/Rail/Irctc/Station/List/"+key,
            data: key
        });
        return response;
    }
});