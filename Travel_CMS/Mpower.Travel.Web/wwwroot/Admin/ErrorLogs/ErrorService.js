mainApp.service("ErrorService", function ($http, ShareData) {

    //Function to Read Pans list based on application id---------
    this.getErrorList = function (id) {
        var request = $http(
            {
                method: "get",
                url: ShareData.ApiUrl + "/ApplicationErrors/GetListByApplicationId/" + ShareData.AppId
            });
        return request;
    };

    //Function to delete pans based on id--------------
    this.deleteError = function (id) {
        var request = $http(
            {
                method: "get",
                url: ShareData.ApiUrl + "/ApplicationErrors/DeleteById/" + ShareData.PageId
            }
        );
        return request;
    }
    
    // Function to define the PageSize--------
    this.PageRecords = function (pageIndex, pageSize) {
        var request = $http(
            {
                method: "get",
                url: ShareData.ApiUrl + "/ApplicationErrors/GetPagedList/" + pageIndex + "/" + pageSize
            }
        );
        return request;
    }
});
