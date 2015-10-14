(function () {
    'use strict';

    angular.module("appRookieBetting").service("gameService", [
    "$http", "$q", "ajaxService", function ($http, $q, ajaxService) {
        function getGameWeeks(season) {
            var request = $http({
                method: 'get',
                cache: false,
                url: getGameWeeksUrl,
                data: { Season: season }
            });
            return request.then(ajaxService.handleAjaxSuccess, ajaxService.handleAjaxError);
        }

        function getAvailableSeasons() {
            var request = $http({
                method: 'get',
                cache: false,
                url: getAvailableSeasonsUrl
            });
            return request.then(ajaxService.handleAjaxSuccess, ajaxService.handleAjaxError);
        }

        return ({
            getGameWeeks: getGameWeeks,
            getAvailableSeasons: getAvailableSeasons
        });
    }
    ]);
})();