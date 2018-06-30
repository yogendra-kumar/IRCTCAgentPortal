mainApp.controller('errorController', function ($scope, $location, ShareData, ErrorService) {
        $scope.currentPage = 1;
        $scope.pageSize = 10;

        getErrorList();

        //-------To get Error List Based on AppId----------
        function getErrorList() {
                var promiseGetError = ErrorService.getErrorList();
                promiseGetError.then(function (pl) {
                        $scope.errorList = pl.data.responseResult;
                        debugger;
                },
                        function (errorPl) {
                                $scope.error = 'failure on loading error list.', errorPl;
                        });
        };

        //------View Error Description on Model PopUp---------------
        $scope.showModel = function (error) {
                $scope.Message = error;
                $scope.title = "Error Description";
                $('#myModel').modal('show');
        }
       
        //----Function delete Error Based on Id-----
        $scope.delete = function (pageId) {
                ShareData.PageId = pageId;
                var promiseDeleteError = ErrorService.deleteError(ShareData.PageId);
                promiseDeleteError.then(function (pl) {
                        $scope.Message = 'Deleted Successfully';
                        $scope.showModal = true;
                        getErrorList();
                },
                        function (errorPl) {
                                $scope.Error = true;
                                $scope.Message = 'Failure in error delete', errorPl;
                                $scope.showModal = true;
                        });
        };

        
});