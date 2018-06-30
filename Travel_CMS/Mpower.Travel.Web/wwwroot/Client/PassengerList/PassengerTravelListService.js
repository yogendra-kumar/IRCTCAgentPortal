
App.service("PassengerTravelListService", function ($http, $q, ShareData) {

//Function to fetch all passengers //"/Mpower/Rail/Passenger/GetAllPassenger/11122345
this.GetAllPassengers = function (roid) { 
      var request = $http({             
            method: "get", 
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/Passenger/GetAllPassenger/"+roid 
        }); 
       return request; 
         
    }; 
   //idcrad type
   this.GetIDCard = function () {
        var request = $http({            
            method: "get",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/Master/GetIDCardType"
        });
        return request;        
    };
    // function to fetch login user travel List //Mpower/Rail/Journey/getAllTravellist/11122345
    this.getTarvelList = function (loginid) {
          var request = $http({
                method: 'get',
                url: ShareData.OxiRailApiUrl + "/Mpower/Rail/Journey/getAllTravellist/"+loginid
                });
                 return request;
             
        };

 //Function to get passenger List of travel list // /Mpower/Rail/Journey/getTravellist/1
        this.getTarvelListByName = function (travelid) {      
         var request = $http({
                method: 'get',
                url: ShareData.OxiRailApiUrl + "/Mpower/Rail/Journey/getTravellist/"+travelid
                });
             return request;
        }
    
   //Create PassengerTravelList  ///Mpower/Rail/Journey/CreateTravelList
    this.Save = function (PassengerList) {
       
            var request = $http({
                method: "post",
                url: ShareData.OxiRailApiUrl + "/Mpower/Rail/Journey/CreateTravelList",
                data: PassengerList
            });
            return request;
        }

//Function to delete the PassengerTravelList 
this.Delete= function (travelListid) {  
            var request = $http({ 
                method: 'get', 
                url: ShareData.OxiRailApiUrl + "/Mpower/Rail/Journey/DeleteTravelList/"+travelListid 
            }); 
             return request; 
        } 
 

 
});