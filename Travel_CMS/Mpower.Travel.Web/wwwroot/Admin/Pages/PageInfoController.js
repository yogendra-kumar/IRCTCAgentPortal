mainApp.controller('PageInfoController', function ($scope, $location, ShareData, PageService) {
        //----For Popup Communication--------
        $scope.Message = "";
        $scope.Error = "";


        GetApplicationPages();
        function GetApplicationPages() {
                if (ShareData.PageId > 0) {
                        var PageView = PageService.FindApplicationPageView(ShareData.PageId);

                        PageView.then(function (pl) {
                                if (pl.data.responseResult != null) {
                                        $scope.Page = pl.data.responseResult;

                                        // set selected value for pages dropdown
                                        $scope.ddlPage = $scope.Page.parentID;

                                        //set selected value for isActive dropdown
                                        $scope.ddlIsActive = $scope.Page.isActive;
                                }
                                else {
                                        $scope.Error = true;
                                        $scope.Message = 'No Pages Record Found!';
                                        $scope.showModal = true;
                                }
                        },
                                function (errorPl) {
                                        $scope.error = 'Error occurred, Please try again.', errorPl;
                                });
                }
                else {
                        var PageList = PageService.GetPageListViewByApplictionId();

                        PageList.then(function (pl) {
                                if (pl.data.responseResult != null) {
                                        $scope.Page =
                                                {
                                                        applicationID: ShareData.AppId,
                                                        pageList: [],
                                                        parentID: 0,
                                                        title: null,
                                                        viewName: null,
                                                        isActive: true
                                                }

                                        for (var i = 0, l = pl.data.responseResult.length; i < l; i++) {
                                                var obj = { key: pl.data.responseResult[i].pageId, value: pl.data.responseResult[i].title };
                                                $scope.Page.pageList.push(obj);
                                        }

                                        // set selected value for pages dropdown
                                        $scope.ddlPage = $scope.Page.parentID;

                                        //set selected value for isActive dropdown
                                        $scope.ddlIsActive = $scope.Page.isActive;
                                }
                                else {
                                        $scope.Error = true;
                                        $scope.Message = 'Please try again!';
                                        $scope.showModal = true;
                                }
                        },
                                function (errorPl) {
                                        $scope.error = 'Error occured, Please try again.', errorPl;
                                }
                        );
                }
        }

        //save method to update the records  
        $scope.Save = function () {
                debugger;
                if ($scope.PageForm.$valid) {
                        if (ShareData.PageId > 0) {
                                //debugger;
                                var Page = {
                                        id: $scope.Page.id,
                                        guid: $scope.Page.guid,
                                        applicationID: $scope.Page.applicationID,
                                        externalUrl: $scope.Page.externalUrl,
                                        layoutID: $scope.Page.layoutID,
                                        scriptFileId: $scope.Page.scriptFileId,
                                        pageHtml: $scope.Page.pageHtml,
                                        leftPanId: $scope.Page.leftPanId,
                                        rightPanId: $scope.Page.rightPanId,
                                        title: $scope.Page.title,
                                        viewName: $scope.Page.viewName,
                                        parentID: $scope.ddlPage,
                                        metaId: $scope.Page.metaId,
                                        IsActive: $scope.ddlIsActive
                                };
                        }
                        else {
                                var d = new Date();
                                var Page = {
                                        id: 0,
                                        guid: "",
                                        applicationID: $scope.Page.applicationID,
                                        externalUrl: "",
                                        layoutID: 0,
                                        scriptFileId: 0,
                                        pageHtml: "",
                                        leftPanId: 0,
                                        rightPanId: 0,
                                        title: $scope.Page.title,
                                        viewName: $scope.Page.viewName,
                                        parentID: $scope.ddlPage,
                                        metaId: 0,
                                        IsActive: $scope.ddlIsActive
                                };
                        }

                        var pagesUpdate = PageService.UpdateApplicationPages(Page)
                        pagesUpdate.then(function (pl) {
                                ShareData.PageId = pl.data.responseResult.id;
                                $scope.Error = false;
                                $scope.Message = 'Successfully Updated';;
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
                $location.path("Admin/PageSetting");
        };
        window.onbeforeunload = function (e) {
                return false;
        };
});
