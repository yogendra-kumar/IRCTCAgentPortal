mainApp.service("PageMetaDataService", function ($http, ShareData) {
 //Function to fetch the Page view betails by Page ID
    this.FindApplicationPage = function (id) {
        var request = $http({
            method: "get",
            url: ShareData.ApiUrl + "/pages/view/" + id
        });
        return request;
    };

    //Function  to Update Page metadata  
    this.Save = function (metadata) {
        //Update metadata page
        if (ShareData.MetaId > 0) {
            var request = $http({
                method: "post",
                url: ShareData.ApiUrl + "/pagesmetadata/Update",
                data: metadata
            });
            return request;
        }
        //Finction to Insert Pages metadata keywords
        else {
            var request = $http({
                method: 'post',
                url: ShareData.ApiUrl + "/pagesmetadata/insert/" + ShareData.PageId,
                data: metadata
            });
            return request;
        }
    };
});