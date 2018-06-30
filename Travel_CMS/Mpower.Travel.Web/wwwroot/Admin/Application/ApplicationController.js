mainApp.controller("UpdateApplicationController", ['$scope', '$location', 'ShareData', 'ApplicationService', function ($scope, $location, ShareData, ApplicationService) {

    //-------For Popup Communication--------
    $scope.Message = "";
    $scope.Error = false;

    //----------Base Url For Images--------
    $scope.Url = ShareData.ApiUrl + '/';

    //-------To get Application Based onId----------
    getApplication();

    function getApplication() {
        var promiseGetApplication = ApplicationService.getApplicationView(ShareData.AppId);
        promiseGetApplication.then(function (pl) {
            if (pl.data.responseResult != null) {
                $scope.Application = pl.data.responseResult;
                $scope.layoutddl = pl.data.responseResult.layoutId;
                $scope.footerddl = pl.data.responseResult.footerId;
                $scope.Application.favIconImageUrl = $scope.Url + $scope.Application.favIconImageUrl;
                $scope.Application.logoImageUrl = $scope.Url + $scope.Application.logoImageUrl;
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

    //To Update Application---------
    $scope.save = function () {
        debugger;
        if ($scope.ApplicationForm.$valid) {
            $scope.uploadFiles();
            var Application = {
                id: $scope.Application.id,
                guid: $scope.Application.guid,
                applicationName: $scope.Application.name,
                domainUrl: $scope.Application.domainUrl,
                domainIP: $scope.Application.domainIP,
                layoutID: $scope.layoutddl,
                feviconFileId: $scope.Application.favIconImageId,
                logoFileId: $scope.Application.logoImageId,
                supportEmail: $scope.Application.supportEmail,
                footerId: $scope.footerddl
            }

            var promiseUpdateApplication = ApplicationService.Update(Application)
            promiseUpdateApplication.then(function (pl) {
                $scope.Error = false;
                $scope.Message = 'Successfully Updated';
                $scope.showModal = true; //To Show The Model PopUp on success
            },
                function (errorPl) {
                    $scope.Error = true;
                    $scope.Message = 'failure in Application Update', errorPl;
                    $scope.showModal = true; //To Show The Model PopUp on error
                });
        }
        else {
            $scope.Error = true;
            $scope.Message = 'Please fix form error';
            $scope.showModal = true; //To Show The Model PopUp on error
        }
    };

    // -----------GET THE LAYOUT FILE INFORMATION.
    $scope.setFevFile = function (element) {
        $scope.currentFevFile = element.files[0];
        var reader = new FileReader();

        reader.onload = function (event) {
            $scope.Application.favIconImageUrl = event.target.result
            $scope.$apply()

        }
        // when the file is read it triggers the onload event above.
        reader.readAsDataURL(element.files[0]);
    }

    // -----------GET THE LOGO FILE INFORMATION.
    $scope.setLogoFile = function (element) {
        $scope.currentLogoFile = element.files[0];
        var reader = new FileReader();

        reader.onload = function (event) {
            $scope.Application.logoImageUrl = event.target.result
            $scope.$apply()

        }
        // when the file is read it triggers the onload event above.
        reader.readAsDataURL(element.files[0]);

    }


    //----------- NOW UPLOAD THE FILES.-------------
    $scope.uploadFiles = function () {

        //FILL FormData WITH FILE DETAILS.
        if ($scope.currentFevFile) {
            var Fevdata = new FormData();
            Fevdata.append("file", $scope.currentFevFile);
            if ($scope.Application.favIconImageId > 0) {
                Fevdata.append("id", $scope.Application.favIconImageId);
            }
            else {
                Fevdata.append("applicationID", $scope.Application.id);
            }
        }
        if ($scope.currentLogoFile) {
            var Logodata = new FormData();
            Logodata.append("file", $scope.currentLogoFile);
            if ($scope.Application.logoImageId > 0) {
                Logodata.append("id", $scope.Application.logoImageId);
            }
            else {
                Logodata.append("applicationID", $scope.Application.id);
            }
        }

        // SEND FEVICON FILE DETAILS TO THE API.
        if ($scope.currentFevFile) {
            if ($scope.Application.favIconImageId > 0) {
                ApplicationService.UpdateFile(Fevdata);
            }
            else {
                (ApplicationService.SaveFile(Fevdata));
            }
            $scope.FileFevicon = JSON.parse(ShareData.Shared).responseResult;
            $scope.Application.favIconImageId = $scope.FileFevicon.id;

        }
        // SEND LOGO FILE DETAILS TO THE API.
        if ($scope.currentLogoFile) {
            if ($scope.Application.logoImageId > 0) {
                ApplicationService.UpdateFile(Logodata);;
            }
            else {
                ApplicationService.SaveFile(Logodata);
            }
            $scope.FileLogo = JSON.parse(ShareData.Shared).responseResult;
            $scope.Application.logoImageId = $scope.FileLogo.id;
        }
    }

    $scope.Back = function () {
                ShareData.PageId = 0;
               window.location.href="/Admin";
        };
    window.onbeforeunload = function (e) {
        return false;
    };
}]);