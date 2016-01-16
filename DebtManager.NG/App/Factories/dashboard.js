'use strict';

angular.module('debtManagerApp.dashboard')
    .factory('dashboardFactory', ['$http', function ($http) {

        var dashboardFactory = {};

        dashboardFactory.getAllMinimized = function () {
            return $http.get('http://localhost:58857/api/MinimizedDebts');
        };

        return dashboardFactory;
    }]);