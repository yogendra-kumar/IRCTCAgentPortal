PanApp.service("panListService", function ($http, ShareData) {

    //Function to Read Pans list based on application id---------
    this.getPanList = function (id) {
        var request = $http(
            {
                method: "get",
                url: ShareData.ApiUrl + "/PageBlocks/GetListByApplicationId/" + ShareData.AppId
            });
        return request;
    };

    //Function to delete pans based on id--------------
    this.deletePan= function(id){
        var request=$http(
            {
                method: "get",
                url: ShareData.ApiUrl+ "/PageBlocks/DeleteById/" + ShareData.PanId
            }
        );
        return request;
    }
});