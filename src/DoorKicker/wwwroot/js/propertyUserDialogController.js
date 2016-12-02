(function () {
    "use strict";

    angular.module("app-door")
      .controller("propertyUserDialogController", propertyUserDialogController);

    function propertyUserDialogController($http, $mdDialog, $q) {
        var vm = this;

        vm.errorMessage = "";
        vm.isBusy = true;

        vm.submit = submit;
        vm.cancel = cancel;

        vm.getUsers = getUsers;

        function getUsers(username) {
             var result = $q.defer();
            $http.get("/api/users?username=" + username)
          .then(function (response) {
              result.resolve(response.data);
          }, function (error) {
                vm.errorMessage = "Failed to load data: " + error;
                result.reject(error);
          });
          return result.promise;
        }
        
        function submit() {
            $mdDialog.hide(vm.selectedItem);
        } 

        function cancel() {
            $mdDialog.cancel();
        }
    }
})();