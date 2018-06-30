
App.controller('PassengerListController', function ($scope, $location, ShareData, PassengerListService) {

 //Below Code is for DateTime Picker   
// $scope.clear = function() {
//     $scope.dt = null;
//   };
// $scope.inlineOptions = {
//     customClass: getDayClass,
//     minDate: new Date(),
//     showWeeks: true
//   };

//   $scope.dateOptions = {
//     dateDisabled: disabled,
//     formatYear: 'yy',
//     maxDate: new Date(2020, 5, 22),
//     minDate: new Date(),
//     startingDay: 1
//   };

//   // Disable weekend selection
//   function disabled(data) {
//     var date = data.date,
//       mode = data.mode;
//     return mode === 'day' && (date.getDay() === 0 || date.getDay() === 6);
//   }

//   $scope.toggleMin = function() {
//     $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
//     $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
//   };

//   $scope.toggleMin();

//   $scope.open1 = function() {
//     $scope.popup1.opened = true;
//   };

//   $scope.open2 = function() {
//     $scope.popup2.opened = true;
//   };

//   $scope.setDate = function(year, month, day) {
//     $scope.dt = new Date(year, month, day);
//   };

//   $scope.formats = ['dd-MMMM-yyyy', 'yyyy/MM/dd', 'dd/MM/yyyy','dd.MM.yyyy', 'shortDate'];
//   $scope.format = $scope.formats[0];
//   $scope.altInputFormats = ['M!/d!/yyyy'];

//   $scope.popup1 = {
//     opened: false
//   };

//   $scope.popup2 = {
//     opened: false
//   };

//   var tomorrow = new Date();
//   tomorrow.setDate(tomorrow.getDate() + 1);
//   var afterTomorrow = new Date();
//   afterTomorrow.setDate(tomorrow.getDate() + 1);
//   $scope.events = [
//     {
//       date: tomorrow,
//       status: 'full'
//     },
//     {
//       date: afterTomorrow,
//       status: 'partially'
//     }
//   ];

//   function getDayClass(data) {
//     var date = data.date,
//       mode = data.mode;
//     if (mode === 'day') {
//       var dayToCheck = new Date(date).setHours(0,0,0,0);

//       for (var i = 0; i < $scope.events.length; i++) {
//         var currentDay = new Date($scope.events[i].date).setHours(0,0,0,0);

//         if (dayToCheck === currentDay) {
//           return $scope.events[i].status;
//         }
//       }
//     }

//     return '';
//   }

//function to calculate Age
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

//
$scope.btnEnabled=true;
$scope.passenger=null;
  $scope.BerthSleeper =null; //["Lower","Middle","Upper","SideLower","SideMiddle","SideUpper"];
   $scope.IDCardType =null; //["Driving Licence","PAN Card","Adhar Card","Passport","Ration Card","Govt Issued ID"]; 
//Function to get the List of ID Card Types
GetIDCardList();
function GetIDCardList() { 
   // debugger;
     var response = PassengerListService.GetIDCard();
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
//Function to get the Details of Berth prefernce
GetBerthList();
function GetBerthList() { 
   // debugger;
     var response = PassengerListService.GetBerth();
      response.then(function (pl) {
          if (pl.data.responseResult != null) {
               $scope.BerthSleeper = pl.data.responseResult;             
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

//Function to clear the controls or reeset the controls
 $scope.Clean=function()
    {
        //debugger;
        $scope.btnEnabled=true;
               $scope.passengerName=null;
                       // $scope.dt=null;
                       $scope.journyDate=null;
                        $scope.gender="Male";
                        $scope.berth=null;
                        $scope.foodPf='Veg';
                        $scope.seniorCitizen="false" ;
                        $scope.CardTpye=null;
                        $scope.CardNumber=null;
    }

//Function to update the passenger Details
$scope.editList = function (id) {
debugger;
                for (i in $scope.passengerList) {
                  if ($scope.passengerList[i].id == id) {
                    $scope.passenger =
                    {  id: $scope.passengerList[i].id,
                       loginAccount: $scope.passengerList[i].loginAccount,
                       name: $scope.passengerList[i].name,
                        bDay: $scope.passengerList[i].bDay,
                        bMonth: $scope.passengerList[i].bMonth,
                        bYear: $scope.passengerList[i].bYear,
                        sex: $scope.passengerList[i].sex,
                        birthPf: $scope.passengerList[i].birthPf,
                        foodPf: $scope.passengerList[i].foodPf,
                        senior: $scope.passengerList[i].senior,
                        idCardTypeId:$scope.passengerList[i].idCardTypeId,
                        idCardNumber:$scope.passengerList[i].idCardNumber                  
                    }
                  }
                }
                console.log("passengerlist:"+$scope.passengerList) 
                console.log($scope.passenger);
                var datestring=($scope.passenger.bDay+"/"+$scope.passenger.bMonth+"/"+$scope.passenger.bYear).toString();
                var date=new Date(datestring);
                $scope.passengerName = $scope.passenger.name;
                $scope.journyDate = date;               
                $scope.gender = ($scope.passenger.sex=="m")?"Male":"Female";
                $scope.berth = $scope.BerthSleeper.indexOf($scope.passenger.birthPf);
                $scope.foodPf = ($scope.passenger.foodPf=="Veg")?"Veg":"NonVeg";
                $scope.senior = ($scope.passenger.senior==true)?"yes":"no";
                $scope.btnEnabled=false;
                for( i in $scope.IDCardType)
                {
                if($scope.IDCardType[i].id==$scope.passenger.idCardTypeId)
                {
                 $scope.CardTpye=$scope.IDCardType[i];
                }
                }
               
                $scope.CardNumber=$scope.passenger.idCardNumber
              }

$(function () {
        $('div[name="datetimepicker1"]').daterangepicker({
            singleDatePicker: true,
            showDropdowns: true,
            // maxDate: new Date
        },
            function (start, end, label) {
                var years = moment().diff(start, 'years');
                $('input[name="date"]').val(this.startDate.format('DD/MM/YYYY'))
                $scope.journyDate = this.startDate.format('DD/MM/YYYY');
            });
    });
//find the Age
//Function for getting list of Passengers
//$scope.Roid='11122345';
//debugger;
$scope.Roid=ShareData.LoginAccount;
GetPassengersList();
function GetPassengersList() { 
    debugger;
     var response = PassengerListService.GetAllPassengers($scope.Roid);
      response.then(function (pl) {
          if (pl.data.responseResult != null) {
               $scope.passengerList = pl.data.responseResult;
              $scope.Clean();             
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

//Function for Updating The record
     $scope.Update = function (id) {
         debugger;
         if ($scope.PassengerListForm.$valid) {
               // var date1=new Date($scope.dt);journyDate
              // var date1=new Date($scope.journyDate)
               var date1 = $scope.journyDate.split("/");
                 var passengerUpdate = {
                        passengerId: id,
                        loginAccount: $scope.Roid,
                        name: $scope.passengerName,
                        bDay: date1[0],
                        bMonth: date1[1],
                        bYear: date1[2],
                        sex: ($scope.gender=="Male")?"m":"f",
                        birthPreferance: $scope.berth,
                        foodPreferance: $scope.foodPf,
                        senior: ($scope.seniorCitizen=="true")?true:false ,
                         idCardTypeId:$scope.CardTpye,
                        idCardNumber:$scope.CardNumber
                        // IDCardType:$scope.CardTpye,
                        // IDCrdNumber:$scope.CardNumber
                } 
                console.log("passengerUpdate");     
                console.log(passengerUpdate); 
                console.log($scope.journyDate);     
                   var promiseUpdatePassngerList = PassengerListService.Update(passengerUpdate)
                  promiseUpdatePassngerList.then(function (pl) {
                            $scope.Error = false;
                            $scope.Message = 'Successfully Updated';
                            $scope.showModal = true;
                            GetPassengersList();
                  },
                function (errorPl) {
                    $scope.Error = true;
                    $scope.Message = 'failure in Page Update';;
                    $scope.showModal = true;
                });
        }
        else {
            $scope.Error = true;
            $scope.Message = 'Please fill mandatory fields.';
            $scope.showModal = true;
        }
    };
//journyDate
    $scope.Save = function (id) {
        if ($scope.PassengerListForm.$valid) {
          debugger;
           //var date2=new Date($scope.journyDate)
           var date2 = $scope.journyDate.split("/");
                     var passengerSave = {
                      loginAccount: $scope.Roid,
                       name: $scope.passengerName,                       
                      //  bDay: $scope.dt.getDate(),
                      //  bMonth: $scope.dt.getMonth()+1,
                      //   bYear: $scope.dt.getFullYear(),
                       bDay: date2[0],
                       bMonth:date2[1],
                        bYear: date2[2],
                        sex: ($scope.gender=="Male")?"m":"f",
                        birthPreferance: $scope.berth,
                        foodPreferance: $scope.foodPf,
                        senior: ($scope.seniorCitizen=="true")?true:false,
                        //IDCardType:$scope.CardTpye,
                        //IDCrdNumber:$scope.CardNumber
                        idCardTypeId:$scope.CardTpye,
                        idCardNumber:$scope.CardNumber
                         

                }           
             var promiseUpdatePassngerList = PassengerListService.Save(passengerSave)
            promiseUpdatePassngerList.then(function (pl) {
                $scope.Error = false;
                $scope.Message = 'Successfully Added';
                $scope.showModal = true;
                GetPassengersList();
            },
                function (errorPl) {
                    $scope.Error = true;
                    $scope.Message = 'failure in Page Add passenger';
                    $scope.showModal = true;
                });
        }
        else {
            $scope.Error = true;
            $scope.Message = 'Please fill mandatory fields.';
            $scope.showModal = true;
       }
    };

//Function to Delete a record
$scope.Remove = function (id) {
            var promiseDeletePassnger = PassengerListService.Delete(id)
            promiseDeletePassnger.then(function (pl) {
                       if(pl.data.status == "success") {
                                $scope.Error = false;
                                $scope.Message = 'Deleted Successfully!';
                                $scope.showModal = true;  
                                GetPassengersList();                              
                            }
                            else {
                                $scope.Error = true;
                                $scope.Message = 'Error occured, Please try again!';
                                $scope.showModal = true;
                        }
               },
                        function (errorPl) {
                                $scope.Error = true;
                                $scope.Message = 'Error occured, Please try again!';
                                $scope.showModal = true;
                        }
                );
              }

    window.onbeforeunload = function (e) {
        return false;
    };
});


