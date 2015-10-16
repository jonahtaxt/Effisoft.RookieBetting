(function () {
    'use strict';

    angular.module('appRookieBetting').directive('effNflDivisions', ['divisionService', 'teamService',
        function (divisionService, teamService) {
        return {
            restrict: 'A',
            templateUrl: 'Scripts/site/directiveTemplates/EffNflDivisions.html',
            link: function (scope, element, attrs) {
                $(document).ready(function() {
                    divisionService.getDivisions().then(function(data) {
                        scope.divisions = [];
                        scope.divisions = [].concat(data);
                        angular.forEach(scope.divisions, function (value, key) {
                            scope.divisions[key].teams = [];
                            teamService.getDivisionStats(value.divisionId).then(function (result) {
                                scope.divisions[key].teams = [].concat(result);
                            });
                        });
                    });
                });
            }
        };
    }]);
})();