mainApp.controller('PageLayoutController', function ($scope, $location, ShareData, PageLayoutService) {

        //-------For Popup Communication--------
        $scope.Message = "";
        $scope.Error = false;
        getAppPans();
        //Fetch PageLaout panes
        function getAppPans() {

                var applicationPanes = PageLayoutService.GetAllPanes();

                applicationPanes.then(function (p) {
                        $scope.PagePanes = p.data.responseResult;
                },
                        function (errorPl) {
                                $scope.error = 'Error occurred, Please try again.', errorPl;
                        });
        };
        //FindApplicationPage
        GetApplicationPages();
        function GetApplicationPages() {

                var PageView = PageLayoutService.FindApplicationPage(ShareData.PageId);

                PageView.then(function (pl) {
                        $scope.Page = pl.data.responseResult;
                        if ($scope.Page != null) {
                                if (parseInt($scope.Page.leftPanId) > 0) {
                                        $scope.checkedLeft = true;
                                        $scope.layoutddlLeft = $scope.Page.leftPanId;
                                }
                                else
                                        $scope.checkedLeft = false;
                        }
                        else
                                $scope.checkedLeft = false;

                        if ($scope.Page != null) {
                                if (parseInt($scope.Page.rightPanId) > 0) {
                                        $scope.checkedRight = true;
                                        $scope.layoutddlRight = $scope.Page.rightPanId;

                                }
                                else
                                        $scope.checkedRight = false;
                        }
                        else
                                $scope.checkedRight = false;


                },
                        function (errorPl) {
                                $scope.error = 'Error occurred, Please try again.', errorPl;
                        });
        }
        //update the Page database
        $scope.Save = function () {

                var d = new Date();
                var lPaneid = parseInt($scope.layoutddlLeft);
                if (!$scope.checkedLeft) {
                        lPaneid = 0;
                }

                var rPaneid = parseInt($scope.layoutddlRight);
                if (!$scope.checkedRight) {
                        rPaneid = 0;
                }
                var Pans = {

                        id: $scope.Page.id,
                        guid: $scope.Page.guid,
                        applicationID: $scope.Page.applicationID,
                        externalUrl: $scope.Page.externalUrl,
                        layoutID: $scope.Page.layoutID,
                        createdDate: $scope.Page.createdDate,
                        modifiedDate: d,
                        scriptFileId: $scope.Page.scriptFileId,
                        pageHtml: $scope.Page.pageHtml,
                        leftPanId: lPaneid,
                        rightPanId: rPaneid,
                        title: $scope.Page.title,
                        viewName: $scope.Page.viewName,
                        parentID: $scope.ddlPage,
                        IsActive: $scope.ddlIsActive
                };
                var pageUpdated = PageLayoutService.UpdatePageLayoutPans(Pans);
                pageUpdated.then(function (pl) {
                        $scope.Error = false;
                        $scope.Message = 'Successfully Updated';
                        $scope.showModal = true;
                },
                        function (errorPl) {
                                $scope.Error = true;
                                $scope.Message = 'failure in Page Update';;
                                $scope.showModal = true;
                        });
        };
        $scope.Back = function () {               
                ShareData.PageId = 0;
                $location.path("/Admin/PageSetting");
        };
        window.onbeforeunload = function (e) {
                return false;
        };
});
