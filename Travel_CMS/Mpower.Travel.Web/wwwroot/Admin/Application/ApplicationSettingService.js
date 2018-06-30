mainApp.service("ApplicationSettingService", function ($http, $q, ShareData) {

    //Fundction to Read Application based upon id
    this.getApplicationSettings = function (id) {

        return $http.get(ShareData.ApiUrl + "/ApplicationSettings/getlistByApplicationId/" + ShareData.AppId);
    };
   
    //Function  to Edit Employee based upon id 
    this.Update = function (Application) {
        var request = $http({
            method: "post",
            url: ShareData.ApiUrl + "/ApplicationSettings/Update",
            data: Application
        });
        return request;
    };
});