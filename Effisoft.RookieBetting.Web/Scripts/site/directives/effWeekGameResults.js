(function () {
    'use strict';

    angular.module('appRookieBetting').directive('effWeekGameResults', ['gameService', function (gameService) {
        return {
            restrict: 'A',
            templateUrl: 'Scripts/site/directiveTemplates/EffWeekGameResults.html',
            link: function (scope, element, attrs) {
                scope.$on('seasonWeekSelected', function (event, args) {
                    gameService.getWeekGameResults(args.week).then(function (data) {
                        scope.weekGameResults = [];
                        scope.weekGameResults = [].concat(data);
                    });
                });
            }
        };
    }]);
})();