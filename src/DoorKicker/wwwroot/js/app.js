(function () {
    "use strict";
    angular.module("app-door", ["ngRoute", "ngMaterial"])
      .config(function ($routeProvider) {
          $routeProvider.when("/", {
              controller: "propertiesController",
              controllerAs: "vm",
              templateUrl: "/views/properties.html"
          });
          $routeProvider.when("/properties/:propertyId/doors", {
              controller: "doorsController",
              controllerAs: "vm",
              templateUrl: "/views/doors.html"
          });
          $routeProvider.when("/properties/:propertyId/doors/:doorId/events", {
              controller: "eventsController",
              controllerAs: "vm",
              templateUrl: "/views/events.html"
          });

          $routeProvider.when("/properties/:propertyId/users", {
              controller: "propertyUsersController",
              controllerAs: "vm",
              templateUrl: "/views/propertyUsers.html"
          });

          $routeProvider.when("/doors/create", {
              controller: "doorFormController",
              controllerAs: "vm",
              templateUrl: "/views/door.form.html"
          });

          $routeProvider.when("/doors/:doorId", {
              controller: "doorFormController",
              controllerAs: "vm",
              templateUrl: "/views/door.form.html"
          });

          $routeProvider.otherwise({ redirectTo: "/" });

      });

})();