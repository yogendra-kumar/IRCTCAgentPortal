PanApp.service("PanService", function ($http, ShareData) {
    //Fundction to Read Pans based upon id
    this.getPanView = function (id) {
        var request = $http(
            {
                method: "get",
                url: ShareData.ApiUrl + "/PageBlocks/FindById/" + ShareData.PanId
            });
            return request;
    };
    //Function  to Edit Pans based upon id 
    this.Save = function (PageBlocks) {
        if (ShareData.PanId > 0) {
            var request = $http({
                method: "post",
                url: ShareData.ApiUrl + "/PageBlocks/update",
                data: PageBlocks
            });
            return request;
        }
        else {
            var request = $http({
                method: "post",
                url: ShareData.ApiUrl + "/PageBlocks/Insert",
                data: PageBlocks
            });
            return request;
        }
    };

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