'use strict';

angular.module('debtManagerApp.payment')
    .factory('paymentFactory', ['$http', function ($http) {

        var urlBase = 'http://localhost:58857/api/Payments';
        var paymentFactory = {};

        paymentFactory.get = function () {
            return $http.get(urlBase);
        };

        paymentFactory.create = function (entity) {
            return $http.post(urlBase, { PayerId: entity.Payer.Id, ReceiverId: entity.Receiver.Id, Amount: entity.Amount, Reason: entity.Reason });
        };

        return paymentFactory;
    }]);