(function () {

    "use strict";
    angular.module("app-door")
      .controller("propertiesController", propertiesController);

    function propertiesController($http) {
        var vm = this;

        vm.properties = [];
        vm.events = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        vm.getProperties = getProperties;

        function getProperties() {
            $http.get("/api/properties")
          .then(function (response) {
              angular.copy(response.data, vm.properties);
          }, function (error) {
              vm.errorMessage = "Failed to load data: " + error;
          })
          .finally(function () {
              vm.isBusy = false;
          });
        }
    }
})();