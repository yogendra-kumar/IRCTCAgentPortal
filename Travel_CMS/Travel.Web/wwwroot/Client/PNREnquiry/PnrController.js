App.controller('PnrController', function ($scope, $location, ShareData, PnrService) {
debugger;

// GetPnrStatus();

 $scope.GetPnrStatus = function (){
   debugger;
   if($scope.pnrNo != undefined)
   {
         var response = PnrStatusService.GetPnrStatus($scope.pnrNo);
         console.log(response);
         response.then(function (pl) {
             if (pl.data.responseResult != null) {
                 $scope.PNREnquiryResponse = null,
                     $scope.PNREnquiryResponse = pl.data.responseResult;
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
 }

});