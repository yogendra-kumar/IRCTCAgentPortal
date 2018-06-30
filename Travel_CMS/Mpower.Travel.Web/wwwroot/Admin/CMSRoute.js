var mainApp = angular.module("mainApp", ['ngRoute','factories','angularUtils.directives.dirPagination']);

mainApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
	$routeProvider
		.when('/ConfigureApp', {
			templateUrl: 'Admin/ConfigureApp',
			controller: 'ApplicationController'
		})
		.when('/PageEdit', {
			templateUrl: 'Admin/PageEdit',
			controller: 'PageInfoController'
		})
		.when('/PageSetting', {
			templateUrl: 'Admin/PageSetting',
			controller: 'PageSettingController'
		})
		.when('/PageContent', {
			templateUrl: 'Admin/PageContent',
			controller: 'PageController'
		})
		.when('/PageLayout', {
			templateUrl: 'Admin/PageLayout',
			controller: 'PageLayoutController'
		})
		.when('/PageMetaData', {
			templateUrl: 'Admin/PageMetaData',
			controller: 'PageMetaDataController'
		})
		.when('/PagePans', {
			templateUrl: 'Admin/PagePans',
			controller: 'panListController'
		})
		.when('/PagePansAdd', {
			templateUrl: 'Admin/PagePansAdd',
			controller: 'PanController'
		})
		.when('/ErrorList', {
			templateUrl: 'Admin/ErrorList',
			controller: 'ErrorController'
		})
		.otherwise({
			templateUrl: 'PageSetting',
			redirectTo: 'Admin/PageSetting'
		});
	$locationProvider.html5Mode({
		enabled: true,
		requireBase: false
	});
}]);