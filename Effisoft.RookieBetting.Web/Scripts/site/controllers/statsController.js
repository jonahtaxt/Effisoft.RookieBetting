(function () {
    'use strict';

    function statsController($scope) {
        $scope.divisions = [];
    }

    angular.module('appRookieBetting').controller('statsController', statsController);

    statsController.$inject = ['$scope'];
})();