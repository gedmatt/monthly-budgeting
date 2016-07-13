angular.module('budgetingApp', [])
.controller('HomeController', function ($scope, $http, $location, $window) {
    alert('arse2');
    $scope.homeModel = {};
    $scope.message = 'Nothing to see here!';
    $scope.result = "color-default";
    $scope.isViewLoading = true;
    
    getHomeInfo();

    function getHomeInfo() {
        //debugger;
        $http.get('/Home/GetHomeInfo')
            .success(function (data, status, headers, config) {
                $scope.homeModel = data;
                $scope.isViewLoading = false;
            })
            .error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error while loading data!!';
                $scope.result = "color-red";
                console.log($scope.message);
                $scope.isViewLoading = false;
            });
    };
});
.config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});