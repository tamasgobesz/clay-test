(function () {

    "use strict";
    angular.module("app-door")
      .controller("propertyUsersController", propertyUsersController);

    function propertyUsersController($routeParams, $http) {
        var vm = this;

        vm.propertyId = $routeParams.propertyId;

        vm.users = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        vm.getUsers = getUsers;

        function getUsers() {
            $http.get("/api/properties/" + vm.propertyId + "/users")
          .then(function (response) {
              angular.copy(response.data, vm.users);
          }, function (error) {
              vm.errorMessage = "Failed to load data: " + error;
          })
          .finally(function () {
              vm.isBusy = false;
          });
        }
    }
})();