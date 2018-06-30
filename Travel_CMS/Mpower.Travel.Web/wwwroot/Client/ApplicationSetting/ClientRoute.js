// var datepicker= angular.module('datepicker', ['ngAnimate', 'ngSanitize', 'ui.bootstrap']);
var App = angular.module("App", ['ngRoute', 'ngMeta','factories','ngAnimate', 'ngSanitize', 'ui.bootstrap']);


App.run(['ngMeta', function (ngMeta) {
	ngMeta.init();
}]);
App.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
	$routeProvider
		.when('/Default', {
			templateUrl: 'Home/Default',
			controller: 'ClientController'
		})
		.when('/Index', {
			templateUrl: 'Home/Index',
			controller: 'ClientController',
			meta: {
				title: 'Home',
				description: 'Home',
				keywords: 'Home',
				author: 'Mpowersoftcom'
			}
		})
		.when('/', {
			templateUrl: 'Home/Oxi_Home',
			controller: 'LoginController',
			meta: {
				title: 'Home',
				description: 'Home',
				keywords: 'Home',
				author: 'Mpowersoftcom'
			}
		})
		.when('/PassengerTravelList', {
			templateUrl: 'Home/PassengerTravelList',
			controller: 'PassengerTravelListController'
		})
		.when('/TrainAtGlance', {
			templateUrl: 'Home/TrainAtGlance',
			controller: 'PassengerTravelListController'

		})
		.when('/DashBoard', {
			templateUrl: 'Home/DashBoard',
			controller: 'LoginController',
		})
		.when('/PassengerList', {
			templateUrl: 'Home/PassengerList',
			controller: 'PassengerListController'

		})
		.when('/PendingTickets', {
			templateUrl: 'Home/PendingTickets',
			controller: 'PendingTicketsController'

		})

		.when('/BookedTicket', {
			templateUrl: 'Home/BookedTicket',
			controller: 'BookedTicketController'

		})

		.when('/CancelTicket', {
			templateUrl: 'Home/CancelTicket',
			controller: 'CancelTicketController'

		})

		.when('/PnrStatus',{
			templateUrl : 'Home/PnrStatus',
			controller : 'PnrController'
		})

		.when('/TDRHistory', {
			templateUrl: 'Home/TDRHistory',
			controller: 'TDRHistoryController'

		})
		.when('/PopularStations',
		{
			templateUrl: 'Home/PopularStations',
			controller: 'PopularStationController'
		})
		.when('/:name*', {
			templateUrl: function (urlattr) {
				return 'Home/Content';
			},
			controller: 'ClientController',
			meta: {
				title: 'About',
				description: 'About',
				keywords: 'About',
				author: 'Mpowersoftcom'
			}
		})


		.otherwise({
			redirectTo: '/',
			controller: 'ClientController'
		});
	$locationProvider.html5Mode(true).hashPrefix('');
}]);
//DashBoard