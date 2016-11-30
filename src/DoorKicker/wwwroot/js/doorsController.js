(function () {

    "use strict";
    angular.module("app-door")
      .controller("doorsController", doorsController);

    function doorsController($routeParams, $http) {
        var vm = this;

        vm.propertyId = $routeParams.propertyId;

        vm.doors = [];
        vm.events = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        vm.getDoors = getDoors;
        vm.openDoor = openDoor;
        vm.closeDoor = closeDoor;

        var url = "/api/properties/" + vm.propertyId + "/doors/";

        function getDoors() {
            $http.get(url)
          .then(function (response) {
              angular.copy(response.data, vm.doors);
          }, function (error) {
              vm.errorMessage = "Failed to load data: " + error;
          })
          .finally(function () {
              vm.isBusy = false;
          });
        }

        function openDoor(doorId) {
            $http.post(url + doorId + "/open")
            .then(function (response) {
                vm.getDoors();
            }), function (error) {
                vm.errorMessage = "Failed open door: " + error;
            };
        }

        function closeDoor(doorId) {
            $http.post(url + doorId + "/close").then(function (response) {
                vm.getDoors();;
            });
        }
    }
})();