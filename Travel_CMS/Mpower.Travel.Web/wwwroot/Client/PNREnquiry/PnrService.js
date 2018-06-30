App.service("PnrService",function ($http, $q, ShareData) {

this.GetPnrStatus=function(pnrNo)
    {
        var request=$http({
            method : "get",
            url : "/GetStatus/"+pnrNo,
        });
        return request;
    }
});