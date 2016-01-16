var debtManager = angular.module('debtManager', ['ngRoute', 'debtManagerApp.payment', 'debtManagerApp.user', 'debtManagerApp.dashboard']);

debtManager.config(function ($routeProvider) {
    $routeProvider.when('/', { templateUrl: 'Areas/Dashboard/Views/overview.html', controller: 'dashboardController' });
    $routeProvider.when('/Payments', { templateUrl: 'Areas/Payment/Views/overview.html', controller: 'paymentsController' });
    $routeProvider.when('/Payments/Create', { templateUrl: 'Areas/Payment/Views/create.html', controller: 'paymentController' });
});

debtManager.config(function ($httpProvider) {
    $httpProvider.defaults.headers.common = {};
    $httpProvider.defaults.headers.post = {};
    $httpProvider.defaults.headers.put = {};
    $httpProvider.defaults.headers.patch = {};
});