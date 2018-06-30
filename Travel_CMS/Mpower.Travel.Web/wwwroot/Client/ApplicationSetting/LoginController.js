App.controller("LoginController", ['$scope', '$location', 'ShareData', 'LoginService', '$sce', '$routeParams', function ($scope, $location, ShareData, LoginService, $sce, $routeParams) {
    $scope.enable=ShareData.PageId;
    $(document).ready(function () {
        $(".div_left_nav_bar ul li#distribution-li ul ").hide();
        $(".div_left_nav_bar ul li#distribution-li div.expand").click(function () {
            $(this).removeClass("collapse");
            $(".div_left_nav_bar ul li#distribution-li ul ").slideUp();
            if (!$(this).next().is(":visible")) {
                $(this).parent().addClass("no-space");
                $(this).addClass("collapse");
                $(this).next().slideDown();
            };
           
        });
    });
     getLoginId();
    function getLoginId(){
        var promiseGetLoginId = LoginService.getLoginId();
        promiseGetLoginId.then(function(pl) {
            ShareData.LoginAccount = pl.data.responseResult;
        });
    }
 $scope.planTravel=function()
   {
        ShareData.PageId=0;
        $location.path("/Home/Dashboard");
   }
}]);