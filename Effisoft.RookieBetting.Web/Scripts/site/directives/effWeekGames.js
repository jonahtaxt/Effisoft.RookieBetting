(function () {
    'use strict';

    angular.module('appRookieBetting').directive('effWeekGames', ['gameService', function (gameService) {
        return {
            restrict: 'E',
            template: '<label class="control-label">Semana:</label>' +
                '<select class="form-control" ng-options="seasonWeek.week for seasonWeek in seasonWeeks" ng-model="selectedSeasonWeek"></select>',
            link: function (scope, element, attrs) {
                scope.$on('seasonWeekSelected', function () {
                    
                });
            }
        };
    }]);
})();