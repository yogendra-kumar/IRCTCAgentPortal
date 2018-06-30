mainApp.service("ApplicationService", function ($http, $q, ShareData) {

    //Fundction to Read Application based upon id
    this.getApplicationView = function (id) {

        return $http.get(ShareData.ApiUrl + "/Application/View/" + ShareData.AppId);
    };


    //Function to create new Application
    this.CreateApplication = function (Application) {
        var request = $http({
            method: "post",
            url: ShareData.ApiUrl + "/Application",
            data: Application,

        });
        return request;
    };

    //Function  to Edit Employee based upon id 
    this.Update = function (Application) {
        var request = $http({
            method: "post",
            url: ShareData.ApiUrl + "/Application/Update",
            data: Application
        });
        return request;
    };
    //Update file-----------
    this.UpdateFile = function (file) {
        var xhttp = new XMLHttpRequest();
        xhttp.open("POST", ShareData.ApiUrl + "/ApplicationFiles/Update", false);
        xhttp.onreadystatechange = function () {
            if (xhttp.readyState == XMLHttpRequest.DONE) {
                ShareData.Shared = xhttp.response;
            }
        }
        xhttp.send(file);
    };
    //Save file-----------
    this.SaveFile = function (file) {
        var xhttp = new XMLHttpRequest();
        xhttp.open("POST", ShareData.ApiUrl + "/ApplicationFiles/Save", false);
        xhttp.onreadystatechange = function () {
            if (xhttp.readyState == XMLHttpRequest.DONE) {
                ShareData.Shared = xhttp.response;
            }
        }
        xhttp.send(file);
    }
});