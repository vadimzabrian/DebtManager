(function () {
    'use strict';

    angular
		.module('debtManager')
		.config(function ($httpProvider) {
		    $httpProvider.interceptors.push('contentTypeInterceptorService');
		});
})();