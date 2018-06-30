var mainApp = angular.module("mainApp", ['factories','ngMaterial']);
mainApp.service("AccountSettingService", function ($http, $q, ShareData) {

    //Fundction to Read UserList based upon id
    this.getApplicationSettings = function (mechantId) {

        return $http.get("/getUserList");
    };

     //Function  to Edit Agent Record
    this.Update = function (Agent) {
        var request = $http({
            method: "post",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/UserRegistration/Update",
            data: Agent
        });
        return request;
    };

 //Function to get UserId
    this.getCurrentMerchant = function () {
            var request = $http({
                method: "get",
                url: "/getUser"
            });
        return request;
    };
     //Function to Insert Agent Detail
    this.Create = function (Agent) {
        var request = $http({
            method: "post",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/UserRegistration/Create",
            data: Agent
        });
        return request;
    };

    //Change Status
    this.ChangeStatus = function (userId,status,merchantId) {
        var request = $http({
            method: "get",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/UserRegistration/ChangeStatus/"+merchantId+"/"+userId+"/"+status
        });
        return request;
    };
   
});