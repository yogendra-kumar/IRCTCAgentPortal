PanApp.controller('panListController', function ($scope, $location, ShareData, panListService) {
        getPanList();
        //-------To get Pans list Based on AppId----------
        function getPanList() {
                var promiseGetPan = panListService.getPanList();
                promiseGetPan.then(function (pl) {
                        $scope.panList = pl.data.responseResult;
                },
                        function (errorPl) {
                                $scope.error = 'failure on loading page pan list.', errorPl;
                        });
        }
        //----Add function to redirect PagePansAdd---------
        $scope.update = function (panId) {
                ShareData.PanId = panId;
                $location.path("/PagePansAdd");
        };
        $scope.Add = function () {
                ShareData.PanId = 0;
                $location.path("/PagePansAdd");
        }

        //----delete function delete pan based on id---------
        $scope.delete = function (panId) {
                ShareData.PanId = panId;
                var promiseDeletePan = panListService.deletePan(ShareData.PanId);
                promiseDeletePan.then(function (pl) {
                        $scope.Message = 'Deleted Successfully ';
                        $scope.showModal = true;
                        getPanList();
                },
                        function (errorPl) {
                                $scope.Error = true;
                                $scope.Message = 'failure in page pans delete', errorPl;
                                $scope.showModal = true;
                        });
        }
        window.onbeforeunload = function (e) {
                return false;
        };
});
