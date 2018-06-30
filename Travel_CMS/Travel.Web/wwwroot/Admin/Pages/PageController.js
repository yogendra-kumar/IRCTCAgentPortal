mainApp.controller('PageController', function ($scope, $location, ShareData, PageService) {

        //----For Popup Communication--------
        $scope.Message = "";
        $scope.Error = "";
        var counter = 0;

        GetPageContent();

        function GetPageContent() {
                debugger;
                if (ShareData.PageId > 0) {
                        var Pages = PageService.GetPageById(ShareData.PageId);

                        Pages.then(function (pl) {
                                $scope.Page = pl.data.responseResult;
                                try {
                                        for (; counter < 2; counter++) {
                                                console.log(CKEDITOR.instances.editor1.getData());
                                                if (CKEDITOR.instances.editor1.getData() == "" && $scope.Page != null)
                                                { GetPageContent(); } else break;
                                        }
                                }
                                catch (e) { return; }
                        },
                                function (errorPl) {
                                        $scope.error = 'Error occurred, Please try again.', errorPl;
                                });
                }
        }

        //save method to update the records  
        $scope.Update = function () {
                debugger;
                if ($scope.PageForm.$valid) {
                        var Pages = {
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
                                parentID: $scope.Page.parentID,
                                metaId: $scope.Page.metaId,
                                IsActive: $scope.Page.isActive
                        };

                        var contentUpdate = PageService.UpdateApplicationPages(Pages)
                        contentUpdate.then(function (pl) {
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

