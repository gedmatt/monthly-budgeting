angular.module('budgetingApp', [])
.controller('PeriodController', function ($scope, $http, $location, $window) {
    
    $scope.periodModel = {};
    $scope.message = '';
    $scope.result = "color-default";
    $scope.isViewLoading = false;
    $scope.ListPeriod = null;

    getPeriodList();

    //****** Get All Period For This Account ******
    function getPeriodList() {
        //debugger;
    
        $http.get('/Period/GetPeriodList')
            .success(function (data, status, headers, config) {
                $scope.ListPeriod = data;
            })
            .error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error while loading data!!';
                $scope.result = "color-red";
                console.log($scope.message);
            });
    };

    //****** Get Single Period ******
    $scope.getPeriod = function (periodModel) {
        $http.get('/Period/GetbyID/' + periodModel.PeriodId)
        .success(function (data, status, headers, config) {
            //debugger;
            $scope.periodModel = data;
            getPeriodList();
            console.log(data);
        })
        .error(function (data, status, headers, config) {
            $scope.message = 'Unexpected Error while loading data!!';
            $scope.result = "color-red";
            console.log($scope.message);
        });
    };

    //****** Save Period ******
    $scope.savePeriod = function () {
        $scope.isViewLoading = true;

        $http({
            method: 'POST',
            url: '/Period/Insert',
            data: $scope.periodModel
        }).success(function (data, status, headers, config) {
            if (data.success === true) {
                $scope.message = 'Form data Saved!';
                $scope.result = "color-green";
                getPeriodList();
                $scope.periodModel = {};
                console.log(data);
            }
            else {
                $scope.message = 'Form data not Saved!';
                $scope.result = "color-red";
            }
        }).error(function (data, status, headers, config) {
            $scope.message = 'Unexpected Error while saving data!!' + data.errors;
            $scope.result = "color-red";
            console.log($scope.message);
        });
        $scope.isViewLoading = false;
    };

    //****** Edit Period ******
    $scope.updatePeriod = function () {
        //debugger;
        $scope.isViewLoading = true;
        $http({
            method: 'POST',
            url: '/Period/Update',
            data: $scope.periodModel
        }).success(function (data, status, headers, config) {
            if (data.success === true) {
                $scope.periodModel = null;
                $scope.message = 'Form data Updated!';
                $scope.result = "color-green";
                getPeriodList();
                console.log(data);
            }
            else {
                $scope.message = 'Form data not Updated!';
                $scope.result = "color-red";
            }
        }).error(function (data, status, headers, config) {
            $scope.message = 'Unexpected Error while updating data!!' + data.errors;
            $scope.result = "color-red";
            console.log($scope.message);
        });
        $scope.isViewLoading = false;
    };

    //****** Delete Period ******
    $scope.deletePeriod = function (periodModel) {
        //debugger;
        var IsConf = confirm('You are about to delete ' + periodModel.Title + '. Are you sure?');
        if (IsConf) {
            $http.delete('/Period/Delete/' + periodModel.PeriodId)
            .success(function (data, status, headers, config) {
                if (data.success === true) {
                    $scope.message = periodModel.Title + ' deleted from record!!';
                    $scope.result = "color-green";
                    getPeriodList();
                    console.log(data);
                }
                else {
                    $scope.message = 'Error on deleting Record!';
                    $scope.result = "color-red";
                }
            })
            .error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error while deleting data!!';
                $scope.result = "color-red";
                console.log($scope.message);
            });
        }
    };
})
.config(function ($locationProvider) {
    $locationProvider.html5Mode(true);
});