
mainApp.controller("AccountSettingController", ['$scope', '$location', 'ShareData', 'AccountSettingService', function ($scope, $location, ShareData, AccountSettingService) {

    //-------For Popup Communication--------
    $scope.Message = "";
    $scope.Error = false;
    $scope.Header = "";
    $scope.modalShown = false;
    $scope.AgentId = 1;
    $scope.phoneNumbr = /^[6789]\d{9}$/;
    $scope.options = [
        { value: '', label: '--Select--' },
        { value: false, label: 'InActive' },
        { value: true, label: 'Active' },
    ];
    //-------To get Agent Based onId----------
    getAgents();
    //---------To get CurrentUserId------------
    getUserId();

    function getAgents() {
        var promiseGetAgents = AccountSettingService.getApplicationSettings();
        promiseGetAgents.then(function (pl) {
            if (pl.data.responseResult != null) {
                $scope.Agents = pl.data.responseResult;
            } else {
                $scope.Error = true;
                $scope.Message = 'No Agents Found';
                $scope.showModal = true;
            }
        },
            function (errorPl) {
                $scope.error = 'failure loading Agents', errorPl;
            });
    }
    function getUserId() {
        var promiseGetMerchant = AccountSettingService.getCurrentMerchant()
        promiseGetMerchant.then(function (pl) {
            $scope.merchantId = pl.data;
        });
    }

    $scope.Edit = function (setting) {
        $scope.Header = "Update Agent"
        $scope.labelAlert = "";
        $scope.disable = true;
        $scope.Agent = setting;
        $("#myModal").modal();
    }
    $scope.Create = function () {
        $scope.disable = false;
        $scope.Agent = {
            "deviceId": 0,
            "digitalCertificate": "",
            "email": '',
            "id": 0,
            "isActive": '',
            "isIRCTCActivated": '',
            "macId": '',
            "merchantAccount": '',
            "merchantId": 0,
            "mobNo": '',
            "pancard": '',
            "subUserId": 0,
            "subUserPassword": '',
            "userId": 0
        };
        $scope.Header = "Add Agent"
        $("#myModal").modal();
    }

    $scope.UpdateAgent = function (agent) {
        if ($scope.ApplicationForm.$valid) {
            var AgentData = {

                "active": agent.isActive,
                "merchantId": $scope.merchantId,
                "userId": agent.userId
            };
            var promiseUpdateAgent = AccountSettingService.Update(AgentData);
            promiseUpdateAgent.then(function (pl) {
                if (pl.data.responseResult != null) {

                    $('#myModal').modal('hide');
                    $scope.Error = false;
                    $scope.Message = 'Updated Successfully';
                    $scope.showModal = true;
                } else {
                    $scope.Error = true;
                    $scope.Message = "Not Updated";
                    $scope.showModal = true;
                }
            },
                function (errorPl) {
                    $scope.Error = true;
                    $scope.Message = "Error In Updation" + errorPl;
                    $scope.showModal = true;
                });
        }
    }

    $scope.CreateAgent = function (agent) {
        if ($scope.ApplicationForm.$valid) {

            var LoginViewModel = {
                "deviceId": agent.deviceId,
                "digitalCertificate": "sdsfsTEST",
                "email": agent.email,
                "password": agent.password,
                "isActive": agent.isActive,
                "isIRCTCActivated": agent.isIRCTCActivated,
                "macId": agent.macId,
                "merchantAccount": agent.merchantAccount,
                "merchantId": $scope.merchantId,
                "mobileNo": agent.mobNo,
                "pancard": agent.pancard,
                "subUserId": agent.subUserId,
                "subUserPassword": agent.subUserPassword
            };
            var promiseCreateAgent = AccountSettingService.Create(LoginViewModel);
            promiseCreateAgent.then(function (pl) {
                    if (pl.data.responseCode == "0") {
                        $scope.Message = 'Agent Created Successfully';
                        $scope.Error = false;
                    }
                    else {
                        $scope.Message = pl.data.responseMessage;
                        $scope.Error = true;
                    }
                    $('#myModal').modal('hide');
                    $scope.showModal = true;
            },
                function (errorPl) {
                    $scope.Error = true;
                    $scope.Message = "Error In Creation" + errorPl;
                    $scope.showModal = true;
                });
        }
    };

    $scope.onChange = function (agent) {
        var promiseAgentStatus = AccountSettingService.ChangeStatus(agent.userId, agent.isActive, $scope.merchantId);
        promiseAgentStatus.then(function (pl) {
        });
    };

}]);
mainApp.directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elm, attrs, ctrl) {
            var firstPassword = '#' + attrs.pwCheck;
            $(elm).add(firstPassword).on('keyup', function () {
                scope.$apply(function () {
                    ctrl.$setValidity('pwmatch', $(elm).val() === $(firstPassword).val());
                });
            });
        }
    }
}]);
