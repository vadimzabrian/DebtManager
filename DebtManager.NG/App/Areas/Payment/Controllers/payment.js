angular.module('debtManagerApp.payment')
	.controller('paymentController', [
		'$scope', '$location', 'paymentFactory', 'userFactory', function ($scope, $location, paymentFactory, userFactory) {
		    $scope.errorMessage = '';
		    $scope.submited = false;

		    $scope.model = { Payer: {}, Receiver: {} };

		    $scope.submit = function () {
		        if ($scope.submited == true) {
		            return;
		        }

		        if ($scope.model.Payer.Id == $scope.model.Receiver.Id) {
		            $scope.errorMessage = 'I dont care about what you do to yourself.';
		            return;
		        }
		        else {
		            $scope.errorMessage = '';
		        }

		        paymentFactory.create($scope.model)
                    .success(function (data) {
                        $scope.submited = true;
                        $location.path('Payments');
                    })
		        .error(function () {
		            $scope.errorMessage = 'An error occured. Payment has NOT been created.';
		        });
		    }

		    userFactory.get().success(function (data) {
		        $scope.users = data;
		        $scope.model.Payer = $scope.users[0];
		        $scope.model.Receiver = $scope.users[1];
		    });
		}]);