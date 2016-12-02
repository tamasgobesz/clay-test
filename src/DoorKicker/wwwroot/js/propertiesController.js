(function () {

    "use strict";
    angular.module("app-door")
      .controller("propertiesController", propertiesController);

    function propertiesController($http, $mdDialog) {
        var vm = this;

        vm.properties = [];
        vm.events = [];
        vm.errorMessage = "";
        vm.isBusy = true;

        vm.getProperties = getProperties;
        vm.addProperty = addProperty;

        var url = "/api/properties";

        function getProperties() {
            $http.get(url)
          .then(function (response) {
              angular.copy(response.data, vm.properties);
          }, function (error) {
              vm.errorMessage = "Failed to load data: " + error;
          })
          .finally(function () {
              vm.isBusy = false;
          });
        }

        function addProperty() {
            $mdDialog.show({
                controller: 'propertyDialogController',
                controllerAs: 'pdc',
                templateUrl: 'views/property.form.html',
                parent: angular.element(document.body),
                clickOutsideToClose: true
            }).then(function (property){
                $http.post(url, property).then(function (response) {
                    getProperties();
                }, function (errorResponse) {
                    vm.errorMessage = "Something bad happened: " + errorResponse;
                });
            });
        }
    }
})();