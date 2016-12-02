(function () {
    "use strict";

    angular.module("app-door")
      .controller("propertyDialogController", propertyDialogController);

    function propertyDialogController($http, $mdDialog) {
        var vm = this;

        vm.errorMessage = "";
        vm.isBusy = true;

        vm.submit = submit;
        vm.cancel = cancel;
        
        function submit() {
            $mdDialog.hide(vm.property);
        } 

        function cancel() {
            $mdDialog.cancel();
        }
    }
})();