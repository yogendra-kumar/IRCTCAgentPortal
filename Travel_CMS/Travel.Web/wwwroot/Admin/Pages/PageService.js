mainApp.service("PageService", function ($http, ShareData) {
    //Function to get content based upon id 
    this.GetPageById = function (id) {
        var request = $http({
            method: "get",
            url: ShareData.ApiUrl + "/pages/FindById/" + id
        });
        return request;
    };

    //Function to Create/Update html content based upon id 
    this.UpdateApplication = function (Page) {
        if (ShareData.PageId > 0) {
            var request = $http({
                method: "post",
                url: ShareData.ApiUrl + "/application/update",
                data: Application
            });
            return request;
        }
        else {
            var request = $http({
                method: "post",
                url: ShareData.ApiUrl + "/application/Insert",
                data: Application
            });
            return request;
        }
    };

    //Function to Get Application Page details based upon id 
    this.FindApplicationPageView = function (id) {
        var request = $http({
            method: "get",
            url: ShareData.ApiUrl + "/pages/view/" + id
        });
        return request;
    };

    //Function to Get Application Page List based upon id
    this.GetPageListViewByApplictionId = function () {
        var request = $http({
            method: "get",
            url: ShareData.ApiUrl + "/configuration/ClientPageViewList/" + ShareData.AppId
        });
        return request;
    }

    //Function to Update applicationIndex
    this.UpdatePageIndex = function (pageId, Index) {
        if (pageId > 0 & Index >= 0) {
            var Postdata = {
                "pageId": pageId,
                "Index": Index
            };
            var xsrf = $.param(Postdata);
            var request = $http({
                method: "post",
                url: ShareData.ApiUrl + "/pages/UpdateIndex",
                data: xsrf,
                headers: {
                    'Content-Type': 'application/x-www-form-urlencoded' // Note the appropriate header
                }
            });
            return request;
        }
    }

    //Function to update application Page 
    this.UpdateApplicationPages = function (Page) {
        if (ShareData.PageId > 0) {
            var request = $http({
                method: "post",
                url: ShareData.ApiUrl + "/pages/update",
                data: Page
            });
            return request;
        }
        else {
            var request = $http({
                method: "post",
                url: ShareData.ApiUrl + "/pages/insert",
                data: Page
            });
            return request;
        }
    };

    this.RemoveApplicationPage = function (id) {
        var request = $http({
            method: "get",
            url: ShareData.ApiUrl + "/pages/DeleteById/" + id
        });
        return request;
    }

    //function to get application page view list
    this.GetApplicationPageList = function () {
        var request = $http({
            method: "get",
            url: ShareData.ApiUrl + "/pages/getlist",
        });
        return request;
    };

});