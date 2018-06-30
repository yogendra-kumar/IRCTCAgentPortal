App.service("LoginService", function ($http, $q, ShareData) {
this.getLoginId = function () {
        var Route="/GetLoginId";
        return $http.get(Route);
    };
});