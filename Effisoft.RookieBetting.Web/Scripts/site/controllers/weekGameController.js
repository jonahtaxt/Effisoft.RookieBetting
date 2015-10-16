(function() {
    'use strict';

    function weekGameController($scope) {
        $scope.weekGameResults = [];
    }

    angular.module('appRookieBetting').controller('weekGameController', weekGameController);

    weekGameController.$inject = ['$scope'];
})();