PanApp.controller('panController', function ($scope, $location, ShareData, PanService) {
        //-------For Popup Communication--------
        $scope.Error = false;
        $scope.Message = "";
        var counter=0;
        getPans();
        //-------To get Pans List Based on PanId----------
        function getPans() {
                debugger;
                var promiseGetPan = PanService.getPanView(ShareData.PanId);
                promiseGetPan.then(function (pl) {
                        $scope.Pans = pl.data.responseResult;
                         console.log($scope.Pans.blockHtml);
                        try {
                                debugger;
                                for (; counter < 2; counter++) {
                                        console.log(CKEDITOR.instances.panEditor.getData());
                                        if (CKEDITOR.instances.panEditor.getData() == "" && $scope.Pans != null) {
                                                getPans();
                                        }
                                        else
                                                break;
                                }
                        }
                        catch (e) {
                                return;
                        }
                },
                        function (errorPl) {
                                $scope.Error = true;
                                $scope.error = 'failure loading page pan', errorPl;
                        });
        }

        //To Inserted and Updated PagePans---------
        $scope.save = function () {
                if ($scope.PagePanForm.$valid && CKEDITOR.instances.panEditor.getData() != "") {
                        $scope.uploadFiles();
                        if (ShareData.PanId > 0) {
                                var Pan = {
                                        id: $scope.Pans.id,
                                        title: $scope.Pans.title,
                                        applicationID: ShareData.AppId,
                                        blockHtml: CKEDITOR.instances.panEditor.getData(),
                                        scriptFileId: $scope.Pans.scriptFileId
                                }
                        }
                        else {
                                var Pan = {
                                        id: ShareData.PanId,
                                        title: $scope.Pans.title,
                                        applicationID: ShareData.AppId,
                                        blockHtml: CKEDITOR.instances.panEditor.getData(),
                                        scriptFileId: $scope.Pans.scriptFileId
                                }
                        }
                        var promiseUpdatePan = PanService.Save(Pan)
                        promiseUpdatePan.then(function (pl) {
                                $scope.Message = 'Successfully Saved';
                                $scope.showModal = true;//To Show The Model PopUp on success
                        },
                                function (errorPl) {
                                        $scope.Error = true;
                                        $scope.Message = 'failure in page pans save', errorPl;
                                        $scope.showModal = true;//To Show The Model PopUp on error
                                });
                }
                else {
                        $scope.Error = true;
                        if (CKEDITOR.instances.panEditor.getData() == "")
                                $scope.Message = 'Please design Pans in editor it can not be blank.';
                        else
                                $scope.Message = 'Please fix form error';
                        $scope.showModal = true; //To Show The Model PopUp on error
                }
        };

        // -----------GET THE PagePans FILE INFORMATION.
        $scope.setFevFile = function (element) {
                $scope.currentFevFile = element.files[0];
        }

        //----------- NOW UPLOAD THE FILES.-------------
        $scope.uploadFiles = function () {

                //FILL FormData WITH FILE DETAILS.
                if ($scope.currentFevFile) {
                        var Fevdata = new FormData();
                        Fevdata.append("file", $scope.currentFevFile);
                        if ($scope.Pans.scriptFileId > 0) {
                                Fevdata.append("id", $scope.Pans.scriptFileId);
                        }
                        else {
                                Fevdata.append("applicationID", $scope.Pans.id);
                        }
                }

                // SEND FEVICON FILE DETAILS TO THE API.
                if ($scope.currentFevFile) {
                        if ($scope.Pans.scriptFileId > 0) {
                                PanService.UpdateFile(Fevdata);
                        }
                        else {
                                (PanService.SaveFile(Fevdata));
                        }
                        $scope.FileFevicon = JSON.parse(ShareData.Shared).responseResult;
                        $scope.Pans.scriptFileId = $scope.FileFevicon.id;
                }
        }
         window.onbeforeunload = function (e) {
        return false;
    };
});

