var PanApp = angular.module("PanApp", ['ngRoute','factories']);
PanApp.config(['$routeProvider', '$locationProvider', function ($routeProvider, $locationProvider) {
    $routeProvider
        .when('/PagePans', {
            templateUrl: 'Admin/PagePans',
            controller: 'panListController'
        })
        .when('/PagePansAdd', {
            templateUrl: 'Admin/PagePansAdd',
            controller: 'panController'
        })
        .otherwise({
            templateUrl: 'PagePans',
            redirectTo: 'Admin/PagePans'
        });
    $locationProvider.html5Mode({
        enabled: true,
        requireBase: false
    });
}]);