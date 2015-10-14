(function () {
    'use strict';

    var msgGenericError = "Ocurrió un error"

    angular.module('appRookieBetting', []);

    angular.module("appRookieBetting").factory("ajaxService", [
    "$q", "$window", function ($q, $window) {

        function handleError(response) {
            if (!angular.isObject(response.data) ||
                !response.data.message) {
                $window.bootbox.alert(msgGenericError);
                return $q.reject(msgGenericError);
            }

            $window.bootbox.alert(msgGenericError);
            return $q.reject(msgGenericError);
        }

        function handleSuccess(response) {
            return response.data;
        }

        return ({
            handleAjaxError: handleError,
            handleAjaxSuccess: handleSuccess
        });
    }
    ]);
})();