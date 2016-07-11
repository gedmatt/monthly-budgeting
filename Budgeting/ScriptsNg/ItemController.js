angular.module('budgetingApp', []).controller('ItemController', function ($scope, $http, $location, $window) {
    $scope.itemModel = {};
    $scope.message = '';
    $scope.result = "color-default";
    $scope.isViewLoading = false;
    $scope.ListItem = null;

    getItemList();

    //****** Get All Items For This Period ******
    function getItemList() {
        //debugger;
        $http.get('/Item/GetItemList')
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