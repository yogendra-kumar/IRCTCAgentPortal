App.controller("ClientController", ['$scope', '$location', 'ShareData', 'ClientSettingService', '$sce', '$routeParams', 'ngMeta', function ($scope, $location, ShareData, ClientSettingService, $sce, $routeParams, ngMeta) {

    //------To get Application setting--------
    getApplication();


    //--------To get Dynamic Menu-------------
    getApplicationMenu();

    //-----For Pans Setting-------------
    $scope.right = false;
    $scope.left = false;
    $scope.enable = ShareData.PageId;
    $scope.journyDate = '';
    $scope.divFrom = true;
    $scope.divTo = true;
    //----------On menu click----------
    $scope.redirect = function (Manu, View) {
        if (View != "#") {
            ShareData.PageId = Manu;
            $location.path("/Content");
        }
    }

    //-------To get Page Setting of selected page
    if (ShareData.PageId != 0) {
        getPage(ShareData.PageId);
    }
    //--------For setting Meta tags(SEO).---------
    $scope.setMeta = function (keyword, desc, title) {
        ngMeta.setTag('author', 'Mpowersoftcom');
        ngMeta.setTag('keywords', keyword);
        ngMeta.setTag('description', desc);
        ngMeta.setTitle(title);

    };

    function getApplication() {
        var promiseGetApplicationSetting = ClientSettingService.getApplicationView();
        promiseGetApplicationSetting.then(function (pl) {
            $scope.AppSetting = pl.data.responseResult;
            $scope.fevicon = ShareData.ApiUrl + "/" + $scope.AppSetting.feviconImageUrl;
            $scope.logoImageUrl = ShareData.ApiUrl + "/" + $scope.AppSetting.logoImageUrl;
            $scope.header = $scope.AppSetting.applicationName;
            $scope.deliberatelyTrustDangerousSnippet = function () {
                return $sce.trustAsHtml($scope.AppSetting.footerContent);
            };
        },
            function (errorPl) {
                $scope.error = 'failure loading Application', errorPl;
            });
    }

    function getPage(i) {
        if (i > 0) {
            var promiseGetPageSetting = ClientSettingService.getPageSetting(i);
            promiseGetPageSetting.then(function (pl) {
                $scope.PageSetting = pl.data.responseResult;
                if ($scope.PageSetting != null) {
                    if ($scope.PageSetting.scriptFileUrlType == ".js") {
                        $scope.ScriptUrl = "<script type='text/javascript' src='" + $scope.PageSetting.scriptFileUrl + "'></script>";
                    }
                    else if ($scope.PageSetting.scriptFileUrlType == ".css") {
                        $scope.ScriptUrl = "<link rel='stylesheet' type='text/css' href='" + $scope.PageSetting.scriptFileUrl + "' />";
                    }

                    $scope.deliberatelyTrustDangerousHtml = function () {
                        return $sce.trustAsHtml($scope.PageSetting.pageHtml);
                    };
                    $scope.deliberatelyTrustDangerousLeftHtml = function () {
                        return $sce.trustAsHtml($scope.PageSetting.leftPanHtmlContent);
                    };
                    $scope.deliberatelyTrustDangerousRightHtml = function () {
                        return $sce.trustAsHtml($scope.PageSetting.rightPanHtmlContent);
                    };
                    if ($scope.PageSetting.rightPanHtmlContent != "")
                        $scope.right = true;
                    if ($scope.PageSetting.leftPanHtmlContent != "")
                        $scope.left = true;
                    $scope.contentClass();
                    $scope.setMeta($scope.PageSetting.keyword, $scope.PageSetting.description, $scope.PageSetting.title);
                }
            },
                function (errorPl) {
                    $scope.error = 'failure loading Application', errorPl;
                });
        }
    };

    $scope.contentClass = function () {
        if (($scope.right == false && $scope.left == true) || ($scope.right == true && $scope.left == false)) {
            $scope.content = "10";
        }
        else if ($scope.right == true && $scope.right == true) {
            $scope.content = "8";
        }
        else
            $scope.content = "12";
    }

    //----Menu List------
    $scope.SiteMenu = [];
    function getApplicationMenu() {
        var promiseGetApplicationMenu = ClientSettingService.getAppMenu();
        promiseGetApplicationMenu.then(function (pl) {
            $scope.SiteMenu = pl.data.responseResult;

        }, function (error) {
            $scope.message = 'Internal Error Try Again';
        });
    };
    //train between station
    $scope.TrainBtwnStations = function (trainFrom, trainTo, Journydate) {
        var date = Journydate.split("/");
        $scope.dateOfJourney = (date[2] + "" + date[0] + "" + date[1]);
        var request = {
            "trainFrom": $scope.trainFrom == undefined ? trainFrom : $scope.trainFrom,
            "trainTo": $scope.trainTo == undefined ? trainTo : $scope.trainTo,
            "enquiryForDate": $scope.dateOfJourney //"20170126"
        };
        var promiseGetTrainBetweenStation = ClientSettingService.getTrainBtwnStations(request);
        promiseGetTrainBetweenStation.then(function (pl) {
            $scope.trains = pl.data.responseResult.trainBtwnStnsList;
            $scope.trainConfig = pl.data.responseResult;
        }, function (error) {
            $scope.message = 'Internal Error Try Again';
        });
    }

    //To get trains for journy date
    var getTrainDetailN = function (obj) {
        var request = { "masterId": "b2bms6", "enquiryType": "3", "moreThanOneDay": "true", "reservationChoice": "99" };
        var objRequest =
            {
                "request": request,
                "trainNo": obj.trainNo,
                "journyDate": $scope.dateOfJourney,
                "trainFrom": obj.from,
                "trainTo": obj.to,
                "trainClass": obj.class,
                "quota": obj.quota,
                "userSession": "3245678"
            };
        var promiseGetTrainDetailN = ClientSettingService.getTrainDetailwithN(objRequest);
        promiseGetTrainDetailN.then(function (pl) {
            if (pl.data.responseCode == "0") {
                $scope.trainDeatil = pl.data.responseResult;
                $scope.avlbl = true;
            }
            else {
                $scope.message = pl.data.responseMessage;
                $scope.avlbl = false;
            }
        }, function (error) {
            $scope.message = 'Internal Error Try Again';
            $scope.avlbl = false;
        });
    };
    $scope.planTravel = function () {
        ShareData.PageId = 0;
        $location.path("/Home/Dashboard");
    }
    $scope.isItArray = function (object) {
        if (angular.isArray(object)) {
            return true;
        }
        else
            return false;
    }
    $scope.findSeat = function (train, date, cls) {
        if ($scope.Quota == undefined)
            alert("select quota");
        else {
            var obj = {
                "trainNo": train.trainNumber,
                "jDate": $scope.dateOfJourney,
                "from": train.fromStnCode,
                "to": train.toStnCode,
                "class": cls,
                "quota": $scope.Quota
            };
            $scope.avlClass = cls;
            ShareData.Shared = train;
            $scope.trainData = ShareData.Shared;
            getTrainDetailN(obj);
        }
    }
    $scope.quotaChange = function (config) {
        $scope.Quota = config;
    };
    $scope.findStation = function (key) {
        if (key != '') {
            $scope.divFrom = true;
            var confirmStation = ClientSettingService.getStation(key);
            confirmStation.then(function (pl) {
                if (pl.data.responseCode == "0") {
                    $scope.MyStationFrom = pl.data.responseResult;
                }
            });
        }
    };
    $scope.findStationTo = function (key) {
        if (key != '') {
            $scope.divTo = true;
            var confirmStation = ClientSettingService.getStation(key);
            confirmStation.then(function (pl) {
                if (pl.data.responseCode == "0") {
                    $scope.MyStationTo = pl.data.responseResult;
                }
            });
        }
    };
    $scope.swapStation = function (trainFrom, trainTo) {
        if (trainFrom != '' && trainTo != '') {
            var temp = $('#from-station').val();
            var temp1 = $('#to-station').val();
            $('#from-station').val(temp1);
            $('#to-station').val(temp);
            $scope.trainFrom = $('#from-station').val();
            $scope.trainTo = $('#to-station').val();
        }
        else {
            alert("select trainFrom and trainTo");
        }
    };
    $scope.SelectItFrom = function (stnCode) {
        stnCode = stnCode.replace('(', '').replace(')', '');
        $('#from-station').val(stnCode);
        $scope.trainFrom = $('#from-station').val();
        $scope.divFrom = false;
    };
    $scope.SelectItTo = function (stnCode) {
        stnCode = stnCode.replace('(', '').replace(')', '');
        $('#to-station').val(stnCode);
        $scope.trainTo = $('#to-station').val();
        $scope.divTo = false;
    };

}]);