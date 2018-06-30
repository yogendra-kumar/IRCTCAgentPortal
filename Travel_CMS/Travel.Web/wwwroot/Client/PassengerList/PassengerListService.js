App.service("PassengerListService", function ($http, ShareData) {
//Function to Retrive ID Card Types
this.GetIDCard = function () {
        var request = $http({            
            method: "get",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/Master/GetIDCardType"
        });
        return request;        
    };

   //Function to Retrive Berth Preference List
    this.GetBerth = function () {
        var request = $http({            
            method: "get",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/Master/GetBerthType"
        });
        return request;        
    };

//Function to Retrive Passenger details 
    this.GetAllPassengers = function (roid) {
        var request = $http({            
            method: "get",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/Passenger/GetAllPassenger/"+roid
        });
        return request;
        
    };
//Mpower/Rail/Master/GetIDCardType 
//Function to Retrive Passenger details by id
    this.GetPassengersById = function (id) {
        var request = $http({
            method: "get",
            url: ShareData.OxiRailApiUrl+ "/Mpower/Rail/Passenger/GetAllPassenger/"+id
        });
        return request;
    };

 //Create Passenger List
    this.Save = function (Passenger) {
            var request = $http({
                method: "post",
                url: ShareData.OxiRailApiUrl + "/Mpower/Rail/Passenger/CreatePassenger",
                data: Passenger
            });
            return request;
        }

 //Function to Update Passenger  
      this.Update= function (Passenger) { 
            var request = $http({
                method: 'post',
                url: ShareData.OxiRailApiUrl + "/Mpower/Rail/Passenger/UpdatePassenger",
                data: Passenger
            });
            return request;
        }

//Function to delete the Passenger
this.Delete= function (id) { 
            var request = $http({
                method: 'get',
                url: ShareData.OxiRailApiUrl + "/Mpower/Rail/Passenger/DeletePassenger/"+id
            });
             return request;
                    }
});