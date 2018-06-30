mainApp.controller("ApplicationSettingController", ['$scope', '$location', 'ShareData', 'ApplicationSettingService', function ($scope, $location, ShareData, ApplicationSettingService) {

    //-------For Popup Communication--------
    $scope.Message = "";
    $scope.Error = false;

    //-------To get Application Based onId----------
    getApplication();

    function getApplication() {
        var promiseGetApplicationSetting = ApplicationSettingService.getApplicationSettings(ShareData.AppId);
        promiseGetApplicationSetting.then(function (pl) {
            if (pl.data.responseResult != null) {
                $scope.ApplicationSetting = pl.data.responseResult;
                angular.forEach($scope.ApplicationSetting, function (obj) {
                    obj["showEdit"] = true;
                })
            } else {
                $scope.Error = true;
                $scope.Message = 'No Application Found';
                $scope.showModal = true;
            }
        },
            function (errorPl) {
                $scope.error = 'failure loading Application', errorPl;
            });
    }
    //Edit and Save By toggleEdit
    $scope.toggleEdit = function (Setting) {
        if (Setting.showEdit == false) {
            var ApplicationSetting = {
                id: Setting.id,
                applicationID: ShareData.AppId,
                key: Setting.key,
                value: Setting.value
            }
            var promiseUpdateApplication = ApplicationSettingService.Update(ApplicationSetting);
            promiseUpdateApplication.then(function (pl) {
                $scope.Message = 'Successfully Updated';
                $scope.showModal = true; //To Show The Model PopUp on success
            },
                function (errorPl) {
                    $scope.Error = true;
                    $scope.Message = 'failure in Application Update', errorPl;
                    $scope.showModal = true; //To Show The Model PopUp on error
                });
        }
        Setting.showEdit = Setting.showEdit ? false : true;
    }
    window.onbeforeunload = function (e) {
        return false;
    };
}]);