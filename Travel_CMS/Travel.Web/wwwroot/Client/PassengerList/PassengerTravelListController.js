App.controller('PassengerTravelListController', function ($scope, $location, ShareData, PassengerTravelListService) {
    // $scope.travelListId = 1;
    
    $scope.loginAccount = ShareData.LoginAccount;
    $scope.TravelList = null;
    $scope.passengerList = null;
    $scope.passengerTableList = null;
    $scope.travelListmodal = null;
    $scope.IDCardType = null; 
    $scope.tabledata=null;              
    
//Function to find the age of the passenger
    $scope.Age=function calculate_age(birth_month,birth_day,birth_year)
{
    today_date = new Date();
    today_year = today_date.getFullYear();
    today_month = today_date.getMonth();
    today_day = today_date.getDate();
    age = today_year - birth_year;

    if ( today_month < (birth_month - 1))
    {
        age--;
    }
    if (((birth_month - 1) == today_month) && (today_day < birth_day))
    {
        age--;
    }
    return age;
}

    //function to fetch all passendres in passengerList
    var getAllPassengersList = function () {
        var response = PassengerTravelListService.GetAllPassengers($scope.loginAccount);
        response.then(function (pl) {
            if (pl.data.responseResult != null) {
                $scope.tabledata = null;
                $scope.tabledata = pl.data.responseResult;
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
            console.log($scope.tabledata);
    }

    //Fuction to fetch the loginuser travellist by  travelid and bind with dropdown

    var GetAllTravelList = function (loginid) {
        var response = PassengerTravelListService.getTarvelList(loginid);
        response.then(function (pl) {
            if (pl.data.responseResult != null) {
                $scope.tabledata = null;
                $scope.TravelList = pl.data.responseResult;
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

    getAllPassengersList();
    GetAllTravelList($scope.loginAccount);
    GetIDCardList();
  // bindCrad();

    //Function to get Passenger of a Travel List
    var getPassengers = function (travelListid) {
        var response = PassengerTravelListService.getTarvelListByName(travelListid);
        response.then(function (pl) {
            if (pl.data.responseResult != null) {                
                $scope.tabledata = pl.data.responseResult.passengers;
                
              var data;  
                for(var i=0;1<$scope.tabledata.length;i++)
                     {
                         for(var j=0;j<$scope.IDCardType.length;j++)
                        {
                            data=$scope.tabledata[i];
                            var card=$scope.IDCardType[j]
                            if(data.idCardTypeId==card.id)
                            {
                                data.CardName=card.name
                            }
                        }
                    } 
                    $scope.tabledata=data;
                    

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


function GetIDCardList() { 
     var response = PassengerTravelListService.GetIDCard();
      response.then(function (pl) {
          if (pl.data.responseResult != null) {
              
               $scope.IDCardType = pl.data.responseResult;  
                          
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
    $scope.Save = function (data) {
        var passengerIds = [];
        var passengerSave = {};
        if ($scope.TravelListForm.$valid) {        
            angular.forEach(data, function (x, key) {               
                if (x.hasOwnProperty('checked')) {
                    if (x.checked == true) {
                        passengerIds.push(x.id);
                    }
                }
            });

            passengerSave = {
                loginAccount: ($scope.loginAccount).toString(),
                description: $scope.UserTravelListDescription,
                listName: $scope.UserTravelListName,
                passengerIds: passengerIds
            }
            if (passengerIds.length > 0) {
                var promiseUpdatePassngerList = PassengerTravelListService.Save(passengerSave)
                promiseUpdatePassngerList.then(function (pl) {
                    $scope.Error = false;
                    $scope.Message = 'Successfully Added';
                    $scope.showModal = true;
                    getAllPassengersList();
                    GetAllTravelList($scope.loginAccount);
                    $scope.UserTravelListDescription = null;
                    $scope.UserTravelListName = null;

                },
                    function (errorPl) {
                        $scope.Error = true;
                        $scope.Message = 'failure in loading the page';
                        $scope.showModal = true;

                    });
            }
            else {
                $scope.Error = true;
                $scope.Message = 'Please fill mandatory fields.';
                $scope.showModal = true;
            }
        }
        else {
            $scope.Error = true;
            $scope.Message = 'Select atleast one record.';
            $scope.showModal = true;
        }

    };

    function bindCrad()
    {
        debugger;
        if($scope.tabledata!=null && $scope.IDCardType!=null)
        {
        foreach(data in $scope.tabledata)
        {
            foreach(card in $scope.IDCardType)
            {
                if(data.iidCardTypeId==card.id)
                {
                $scope.tabledata.CardName=card.name
                }
            }
        }
        }
    }

$scope.IsTravelList=false;
    $scope.TableUpdate = function () {       
        if ($scope.travelListmodal != "") {            
            var travel = JSON.parse($scope.travelListmodal);
            $scope.tabledata = null;
            $scope.IsTravelList=true;
            $scope.UserTravelListName = travel.listName;
            $scope.UserTravelListDescription = travel.description;
            getPassengers(travel.id);            
        }
        else {            
            $scope.UserTravelListName = null;
            $scope.UserTravelListDescription = null;
            $scope.IsTravelList=false;
            getAllPassengersList();
            GetAllTravelList($scope.loginAccount);
        }
    }

    $scope.Remove = function (travel) {        
        var json = JSON.parse(travel);
        var promiseDeleteTravelList = PassengerTravelListService.Delete(json.id)
        promiseDeleteTravelList.then(function (pl) {
            if (pl.data.status == "success") {
                $scope.Error = false;
                $scope.Message = 'Deleted Successfully!';
                $scope.showModal = true;
                getAllPassengersList();
                GetAllTravelList($scope.loginAccount);
                $scope.UserTravelListDescription = null;
                $scope.UserTravelListName = null;
            }
            else {
                $scope.Error = true;
                $scope.Message = 'Error occurred, Please try again!';
                $scope.showModal = true;
            }
        },
            function (errorPl) {
                $scope.Error = true;
                $scope.Message = 'Error occurred, Please try again!';
                $scope.showModal = true;
            }
        );
    }

    window.onbeforeunload = function (e) {
        return false;
    };

});
