angular.module('debtManagerApp.payment', [])
	.controller('paymentsController', [
		'$scope', '$location', 'paymentFactory', function ($scope, $location, paymentFactory) {

		    paymentFactory.get().success(function (data) {
		        $scope.payments = data;
		    });

		    $scope.addPaymentClicked = function () {
		        $location.path('Payments/Create');
		    }
		}]);