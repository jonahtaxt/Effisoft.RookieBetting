(function() {
    'use strict';

    function homeController($scope) {
        $scope.test = 'Testing';
        $scope.seasons = [];
        $scope.selectedSeason = null;
        $scope.seasonWeeks = [];
        $scope.selectedSeasonWeek = null;
    }

    angular.module('appRookieBetting').controller('homeController', homeController);

    homeController.$inject = ['$scope'];
})();