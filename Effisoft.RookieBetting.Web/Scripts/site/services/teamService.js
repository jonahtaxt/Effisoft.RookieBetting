(function () {
    'use strict';

    angular.module("appRookieBetting").service("teamService", [
    "$http", "$q", "ajaxService", function ($http, $q, ajaxService) {
        function getDivisionStats(divisionId) {
            var request = $http({
                method: 'get',
                cache: false,
                url: getDivisionStatsUrl + '?divisionId=' + divisionId
            });
            return request.then(ajaxService.handleAjaxSuccess, ajaxService.handleAjaxError);
        }

        return ({
            getDivisionStats: getDivisionStats
        });
    }
    ]);
})();