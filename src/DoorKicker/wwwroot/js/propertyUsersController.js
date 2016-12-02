(function () {

    "use strict";
    angular.module("app-door")
      .controller("propertyUsersController", propertyUsersController);

    function propertyUsersController($routeParams, $http, $mdDialog) {
        var vm = this;

        vm.propertyId = $routeParams.propertyId;

        vm.users = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        vm.getUsersForProperty = getUsersForProperty;
        vm.addPropertyUser = addPropertyUser;

        vm.url = "/api/properties/" + vm.propertyId + "/users";

        function getUsersForProperty() {
            $http.get(vm.url)
          .then(function (response) {
              angular.copy(response.data, vm.users);
          }, function (error) {
              vm.errorMessage = "Failed to load data: " + error;
          })
          .finally(function () {
              vm.isBusy = false;
          });
        }

        function addPropertyUser()
        {
            $mdDialog.show({
                controller: 'propertyUserDialogController',
                controllerAs: 'pudc',
                templateUrl: 'views/propertyUser.form.html',
                parent: angular.element(document.body),
                clickOutsideToClose:true
            }).then(function (propertyUser){
                $http.post(vm.url, propertyUser).then(function (response) {
                    getUsersForProperty();
                }, function (errorResponse) {
                    vm.errorMessage = "Something bad happened: " + errorResponse;
                });
            });
        }
    }
})();