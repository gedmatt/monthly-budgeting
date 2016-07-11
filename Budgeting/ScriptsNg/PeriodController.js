angular.module('budgetingApp', []).controller('PeriodController', function ($scope, $http, $location, $window) {
    $scope.itemModel = {};
    $scope.message = '';
    $scope.result = "color-default";
    $scope.isViewLoading = false;
    $scope.ListItem = null;

    getPeriodList();

    //****** Get All Period For This Account ******
    function getPeriodList() {
        //debugger;
        $http.get('/Period/GetPeriodList')
            .success(function (data, status, headers, config) {
                $scope.ListItem = data;
            })
            .error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error while loading data!!';
                $scope.result = "color-red";
                console.log($scope.message);
            });
    };

})
.config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});