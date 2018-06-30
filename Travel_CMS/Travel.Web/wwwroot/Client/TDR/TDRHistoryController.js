App.controller('TDRHistoryController', function ($scope, $location,$filter, ShareData, TDRHistoryService) { 
debugger;
$scope.TicketHistory=null;
//   $scope.TdrTabledata=[
//   {"Ticketno":"1000250001","Pnr":"4562821001","Reason":"gdfdfhfhh","Status":"refunded","TdrDate":"9-11-2016"},
//   {"Ticketno":"1000250002","Pnr":"4562821002","Reason":"gdfdfhfhhaaa","Status":"refunded","TdrDate":"9-11-2016"},
//   {"Ticketno":"1000250003","Pnr":"4562821003","Reason":"gdfdfhfhhccc","Status":"declined","TdrDate":"10-11-2016"},
//   {"Ticketno":"1000250004","Pnr":"4562821004","Reason":"gdfdfhfhhvvv","Status":"false","TdrDate":"9-10-2016"},
//   {"Ticketno":"1000250005","Pnr":"4562821005","Reason":"gdfdfhfhhfff","Status":"refunded","TdrDate":"12-9-2016"},
//   {"Ticketno":"1000250006","Pnr":"4562821006","Reason":"gdfdfhfhhvvv","Status":"refunded","TdrDate":"9-7-2016"},
//   {"Ticketno":"1000250007","Pnr":"4562821007","Reason":"gdfdfhfhhvvv","Status":"refunded","TdrDate":"9-10-2016"}
//   ] 


$scope.Roid=ShareData.RoId;
GetPassengersList();
function GetPassengersList() { 
     var response = TDRHistoryService.GetTdrTickets($scope.Roid);
     debugger;
      response.then(function (pl) {
          if (pl.data.responseResult != null) {
              $scope.TdrTabledata = pl.data.responseResult;
              $scope.searchData=angular.copy($scope.TdrTabledata);
             // console.log($scope.TdrTabledata);

            }
           else {
                    $scope.Error = true;
                    $scope.Message = 'No Pages Record Found!';
                    $scope.showModal = true;
                }
                        },
    function (errorPl) {
                      $scope.error = 'Error occurred, Please try again.', errorPl;
                    });
        }


//         //GetTransactionDetails();
// function GetTransactionDetails(Trxnid) { 
//      var response = TDRHistoryService.GetTransactionInfo(Trxnid);
//       response.then(function (pl) {
//           if (pl.data.responseResult != null) {
//               $scope.TransactionData = pl.data.responseResult;                           
//             }
//            else {
//                     $scope.Error = true;
//                     $scope.Message = 'No Pages Record Found!';
//                     $scope.showModal = true;
//                 }
//                         },
//     function (errorPl) {
//                       $scope.error = 'Error occurred, Please try again.', errorPl;
//                     });
//         }
$scope.Search=function()
{ 
    $scope.TdrTabledata =[];
    if($scope.PnrSearch!="")
    {
debugger;
    angular.forEach( $scope.searchData, function(item){
            if(item.pnr.indexOf( $scope.PnrSearch) !== -1){                
           
            //console.log(item);
           // console.log($scope.PnrSearch);
            $scope.TdrTabledata.push(item);
        }
        });  
    }
    else
    {
        $scope.TdrTabledata=angular.copy($scope.searchData);
    }
}

$scope.OpenPopup = function (setting) {
    $scope.pnrData=null;
$scope.Header = "TDR Ticket Details"
$scope.labelAlert = "";
$scope.disable = true;
$scope.Agent = setting;
console.log($scope.Agent);
console.log(setting);
$scope.pnrData=angular.copy(setting);
$("#TdrTicketModal").modal();

}



App.filter('searchFor', function(){    
    return function(arr, searchString){
        if(!searchString){
            return arr;
        }
        debugger;
        var result = [];
       // searchString = searchString.toLowerCase();
        angular.forEach(arr, function(item){
            if(item.pnr.indexOf(searchString) !== -1){
                
            result.push(item);
        }
        });
        return result;
    };
});
});