// tripEditorController.js
(function () {
    "use strict";

    angular.module("app-door")
      .controller("doorFormController", doorFormController);

    function doorFormController($routeParams, $http) {
        var vm = this;

        vm.doorId = $routeParams.doorId;
        vm.errorMessage = "";
        vm.isBusy = true;

        var url = "/api/doors/" + vm.doorId;

        $http.get(url)
          .then(function (response) {
          }, function (err) {
              vm.errorMessage = "Failed to load data";
          })
          .finally(function () {
              vm.isBusy = false;
          });
    }
})();