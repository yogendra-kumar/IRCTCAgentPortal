mainApp.controller('PageSettingController', function ($scope, $location, ShareData, PageService) {

        //----For Popup Communication--------
        $scope.Message = "";
        $scope.Error = "";
        GetPageList();

        function GetPageList() {
                // debugger;
                var pageViewList = PageService.GetApplicationPageList();

                pageViewList.then(function (pl) {
                        debugger;
                        if (pl.data.responseResult != null) {
                                $scope.PageView = pl.data.responseResult;
                        }
                        else {
                                $scope.Error = true;
                                $scope.Message = 'No Record Found!';
                                $scope.showModal = true;
                        }
                },
                        function (errorPl) {
                                $scope.error = 'Error occurred, Please try again.', errorPl;
                        });

        }

        $scope.Remove = function (id) {
                var page = PageService.RemoveApplicationPage(id);
                page.then(function (pl) {
                        debugger;
                       if (pl.data.status == "success") {
                                $scope.Error = false;
                                $scope.Message = 'Deleted Successfully!';
                                $scope.showModal = true;
                                GetPageList();
                       }
                        else {
                                $scope.Error = true;
                                $scope.Message = 'Error occured, Please try again!';
                                $scope.showModal = true;
                        }
               },
                        function (errorPl) {
                                //$scope.error = 'Error occured, Please try again!', errorPl;

                                $scope.Error = true;
                                $scope.Message = 'Error occured, Please try again!';
                                $scope.showModal = true;
                        }
                );
        }

        $scope.click = function (id) {
                //debugger;                
                ShareData.PageId = id;
                $location.path("/PageEdit");
        };

        //To Index Up
        $scope.pageUp = function (index, pageId) {
                if (index <= 0 || index >= $scope.PageView.length)
                        return;

                var temp = getPage(index);
                var PromiseUpdate = PageService.UpdatePageIndex(temp.id, getPage(index - 1).index)
                PromiseUpdate.then(function (pl) {
                        var subPromise = PageService.UpdatePageIndex(getPage(index - 1).id, temp.index);
                        subPromise.then(function (pl) {
                                GetPageList();
                        });
                });
        };
        
        //To Index Down
        $scope.pageDown = function (index, pageId) {
                if (index < 0 || index >= ($scope.PageView.length - 1))
                        return;
                var temp = getPage(index);
                var PromiseUpdate = PageService.UpdatePageIndex(temp.id, getPage(index + 1).index)
                PromiseUpdate.then(function (pl) {
                        var subPromise = PageService.UpdatePageIndex(getPage(index + 1).id, temp.index);
                        subPromise.then(function (pl) {
                                GetPageList();
                        });
                });

        };
        function getPage(index) {
                var i = 0;
                for (; i < $scope.PageView.length; i++) {
                        if ($scope.PageView[i].index == index) {
                                return $scope.PageView[i];
                        }
                }
        }
        $scope.Create = function (id) {                
                ShareData.PageId = 0;
                $location.path("/PageEdit");
        }
        window.onbeforeunload = function (e) {
                return false;
        };
});