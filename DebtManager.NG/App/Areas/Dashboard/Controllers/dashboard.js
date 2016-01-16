angular.module('debtManagerApp.dashboard', [])
	.controller('dashboardController', [
		'$scope', '$location', 'dashboardFactory', function ($scope, $location, dashboardFactory) {

		    $scope.displayEmptyMessage = false;

		    dashboardFactory.getAllMinimized().success(function (data) {
		        $scope.aggregatedMinimizedDebts = data;
		        $scope.displayEmptyMessage = data && data.length == 0;
		    });
		}]);