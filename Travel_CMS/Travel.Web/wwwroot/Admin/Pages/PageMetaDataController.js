mainApp.controller('PageMetaDataController', function ($scope, $location, ShareData, PageMetaDataService) {
    
    //-------For Popup Communication--------
    $scope.Message = "";
    $scope.Error = false;
    
    // For showing tolltip
    $scope.tooltipItems = ["Add Multiple keywords with coma seperated", "Max 170 characters for best result"];
    GetApplicationPages();
    $(document).ready(function () {
        $('[data-toggle="tooltip"]').tooltip();
    });

    //get Application view
    function GetApplicationPages() {
        var PageView = PageMetaDataService.FindApplicationPage(ShareData.PageId);
        PageView.then(function (pl) {
            $scope.Page = pl.data.responseResult;
            console.log(pl.data.responseResult);
        },
            function (errorPl) {
                $scope.error = 'Error occurred, Please try again.', errorPl;
            });
    }
    //Function  to Edit Page metadata & insert based upon id 
    $scope.Update = function () {
        if ($scope.PageMetaDataForm.$valid) {
            ShareData.MetaId = $scope.Page.metaId;
            if (ShareData.MetaId > 0) {
                var KeywordsData = {
                    Id: $scope.Page.metaId,
                    keyword: $scope.Page.keyword,
                    description: $scope.Page.description
                }
            }
            else {
                var KeywordsData = {
                    Id: $scope.Page.MetaId,
                    keyword: $scope.Page.keyword,
                    description: $scope.Page.description
                }
            }
            var promiseUpdateMetaData = PageMetaDataService.Save(KeywordsData)
            promiseUpdateMetaData.then(function (pl) {
                $scope.Error = false;
                $scope.Message = 'Successfully Updated';
                $scope.showModal = true;
            },
                function (errorPl) {
                    $scope.Error = true;
                    $scope.Message = 'failure in Page Update';;
                    $scope.showModal = true;
                });
        }
        else {
            $scope.Error = true;
            $scope.Message = 'Please fill mandatory fields.';
            $scope.showModal = true;
        }

    };

       $scope.Back = function () {               
                ShareData.PageId = 0;
                $location.path("/Admin/PageSetting");
        };
    window.onbeforeunload = function (e) {
        return false;
    };

});

