mainApp.service("PageLayoutService", function ($http,ShareData) {
    

    //Function to Get Page List for All PageLaout
    this.GetAllPanes = function () {
        var request = $http({
            method: "get",
            url: ShareData.ApiUrl+"/pageblocks/GetList"
        });
        return request;
    };

    //Function to Update Page For PageLaout
    this.UpdatePageLayoutPans = function (Pans) {
        var request = $http({
            method: "post",
            url: ShareData.ApiUrl+"/pages/Update",
            data: Pans
        });
        return request;
    };

    //Function to fetch Pages by page ID

    this.FindApplicationPage = function (id) {
        var request = $http({
            method: "get",
            url: ShareData.ApiUrl+ "/pages/view/" + id
        });
        return request;
    };

    //Function to GetList By Application ID For PageLaout
    this.GetListByApplicationId = function (id) {
        var request = $http({
            method: "post",
            url: ShareData.ApiUrl+"/GetListByApplicationId/"+id,
            data: Application
        });
        return request;
    };

});