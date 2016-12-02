(function () {
    "use strict";

    angular.module("app-door")
      .controller("doorDialogController", doorDialogController);

    function doorDialogController($http, $mdDialog) {
        var vm = this;

        vm.errorMessage = "";
        vm.isBusy = true;

        vm.submit = submit;
        vm.cancel = cancel;
        
        function submit() {
            $mdDialog.hide(vm.door);
        } 

        function cancel() {
            $mdDialog.cancel();
        }
    }
})();