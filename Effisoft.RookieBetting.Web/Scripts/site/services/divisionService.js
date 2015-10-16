(function () {
    'use strict';

    angular.module("appRookieBetting").service("divisionService", [
    "$http", "$q", "ajaxService", function ($http, $q, ajaxService) {
        function getDivisions() {
            var request = $http({
                method: 'get',
                cache: false,
                url: getDivisionsUrl
            });
            return request.then(ajaxService.handleAjaxSuccess, ajaxService.handleAjaxError);
        }

        return ({
            getDivisions: getDivisions
        });
    }]);
})();