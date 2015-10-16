(function () {
    'use strict';

    angular.module('appRookieBetting').directive('effAvailableSeasons', ['gameService', '$rootScope',
        '$filter', function (gameService, $rootScope, $filter) {
            return {
                restrict: 'A',
                templateUrl: 'Scripts/site/directiveTemplates/EffAvailableSeasons.html',
                link: function (scope, element, attrs) {
                    $(document).ready(function () {

                        angular.element(element[0].children.selSeasonWeeks).on('change', function () {
                            $rootScope.$broadcast('seasonWeekSelected', { week: scope.selectedSeasonWeek.week });
                        });

                        gameService.getAvailableSeasons(0).then(function (data) {
                            scope.seasons = [];
                            scope.seasons = [].concat(data);
                            var found = $filter('filter')(scope.seasons, { season: 2015 }, true);
                            if (found.length > 0) scope.selectedSeason = found[0];
                            gameService.getGameWeeks(scope.selectedSeason.season).then(function (data) {
                                scope.seasonWeeks = [].concat(data);
                                scope.selectedSeasonWeek = scope.seasonWeeks[0];
                                $rootScope.$broadcast('seasonWeekSelected', { week: scope.selectedSeasonWeek.week });
                            });
                        });

                    });
                }
            };
        }]);
})();