(function () {

    "use strict";
    angular.module("app-door")
      .controller("eventsController", eventsController);

    function eventsController($routeParams, $http) {
        var vm = this;

        vm.propertyId = $routeParams.propertyId;
        vm.doorId = $routeParams.doorId;

        vm.doors = [];
        vm.events = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        vm.getEvents = getEvents;

        var url = "/api/properties/" + vm.propertyId + "/doors/" + vm.doorId + "/events"; 

        function getEvents() {
            $http.get(url)
          .then(function (response) {
              angular.copy(response.data, vm.events);
          }, function (error) {
              vm.errorMessage = "Failed to load data: " + error;
          })
          .finally(function () {
              vm.isBusy = false;
          });
        }
    }
})();