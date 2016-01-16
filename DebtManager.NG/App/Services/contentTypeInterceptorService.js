(function () {
    'use strict';

    angular
        .module('debtManager')
        .factory('contentTypeInterceptorService', contentTypeInterceptorService);

    contentTypeInterceptorService.$inject = [];// ['$q', '$location', 'localStorageService', 'constants'];

    function contentTypeInterceptorService(){//($q, $location, localStorageService, constants) {
        var service = {
            request: request,
            responseError: responseError
        };

        return service;

        function request(config) {
            config.headers = config.headers || {};

            config.headers['Content-Type'] = 'application/json; charset=utf-8';
           
            return config;
        }
        function responseError(rejection) {
            //if (rejection.status === 401) {
            //    $location.path('/login');
            //}
            //return $q.reject(rejection);
        }
    }
})();