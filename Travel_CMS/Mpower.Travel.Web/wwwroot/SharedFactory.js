var factories = angular.module("factories", []),
  factory = factories.factory("ShareData", function () {
    return {
      PageId: 0,
      ApiUrl: "http://103.253.37.56/cms_oxirail/CMS_API",//"http://172.160.3.251/cmsapi",//
      OxiRailApiUrl:"http://172.160.3.213/oxirail_api",//"http://172.160.3.251/railapi",//
      //OxiRailApiUrl: "http://localhost:5001",
      LoginAccount: "",
      RoId: "3245678",
      AppId: 1,
      PanId: 0,
      MeataId: 0,
      Shared: Object
    }
  });

//-----------------
factories.directive('modal', function () {
  return {
    template: '<div class="modal fade">' +
    '<div class="modal-dialog modal-lg">' +
    '<div class="modal-content">' +
    '<div class="modal-header">' +
    '<button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>' +
    '<h4 class="modal-title">Message</h4>' +
    '</div>' +
    '<div class="modal-body" ng-transclude></div>' +
    '</div>' +
    '</div>' +
    '</div>',
    restrict: 'E',
    transclude: true,
    replace: true,
    scope: true,
    link: function postLink(scope, element, attrs) {
      scope.$watch(attrs.visible, function (value) {
        if (value == true)
          $(element).modal('show');
        else
          $(element).modal('hide');
      });

      $(element).on('shown.bs.modal', function () {
        scope.$apply(function () {
          scope.$parent[attrs.visible] = true;
        });
      });

      $(element).on('hidden.bs.modal', function () {
        scope.$apply(function () {
          scope.$parent[attrs.visible] = false;
        });
      });
    }
  };
});

factories.directive('ckEditor', function () {
  return {
    require: '?ngModel',
    link: function (scope, elm, attr, ngModel) {
      var ck = CKEDITOR.replace(elm[0]);
      if (!ngModel) return;
      ck.on('instanceReady', function () {
        ck.setData(ngModel.$viewValue);
      });
      function updateModel() {
        scope.$apply(function () {
          ngModel.$setViewValue(ck.getData());
        });
      }
      ck.on('change', updateModel);
      ck.on('key', updateModel);
      ck.on('dataReady', updateModel);
      ck.on('pasteState', updateModel);
      ck.on('selectionChange', updateModel);

      ngModel.$render = function (value) {
        ck.setData(ngModel.$viewValue);
      };
    }
  };
});
//Alert message for delete records--------
factories.directive('ngConfirmClick', [
  function () {
    return {
      link: function (scope, element, attr) {
        var msg = attr.ngConfirmClick || "Are you sure?";
        var clickAction = attr.confirmedClick;
        element.bind('click', function (event) {
          if (window.confirm(msg)) {
            scope.$eval(clickAction)
          }
        });
      }
    };
  }]);

// Progress/loader directive

factories.directive('loading', ['$http', function ($http) {
  return {
    restrict: 'E',
    replace: true,
    template: '<div class="modal fade">' +
    '<div class="modal-dialog">' +
    '<div class="modal-content">' +
    '<div class="modal-body"><img alt="Please wait" src="/images/processing.gif"></img>Please wait while processing!!!</div>' +
    '</div>' +
    '</div>' +
    '</div>',

    link: function (scope, element, attrs) {
      scope.isLoading = function () {
        return $http.pendingRequests.length > 0;
      };

      scope.$watch(scope.isLoading, function (v) {
        if (v) {
          $(element).modal('show');
        } else {
          $(element).modal('hide');
        }
      });
    }
  };
}]);

factories.directive('onlyDigits', function () {

  return {
    restrict: 'A',
    require: '?ngModel',
    link: function (scope, element, attrs, ngModel) {
      if (!ngModel) return;
      ngModel.$parsers.unshift(function (inputValue) {
        var digits = inputValue.split('').filter(function (s) { return (!isNaN(s) && s != ' '); }).join('');
        ngModel.$viewValue = digits;
        ngModel.$render();
        return digits;
      });
    }
  };
});


factories.directive("regExInput", function () {
  "use strict";
  return {
    restrict: "A",
    require: "?regEx",
    scope: {},
    replace: false,
    link: function (scope, element, attrs, ctrl) {
      element.bind('keypress', function (event) {
        var regex = new RegExp(attrs.regEx);
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
          event.preventDefault();
          return false;
        }
      });
    }
  };
});