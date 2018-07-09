var app = angular.module('studentApp', []);
app.controller('Ctrl', function ($scope, $http) {
    
    $scope.students = [];
    $scope.xyz = 12;

    $scope.init = function () {
        $scope.All();
        console.log('in init');
    }
    
    $scope.All = function () {
        var url = '/api/student/all'
        $http({
            method: 'GET',
            url: url,
        }).then(function (response) {
            $scope.students = response.data;
            console.log('all')
            console.log($scope.students);
        }, function (response) {
            console.log(response);
        });
    };

    $scope.Search = function () {
        var url = '/api/student/Index?Searching=' + $scope.searchKeyword;
        $http({
            method: 'GET',
            url: url,
        }).then(function (response) {
            $scope.students = response.data;
            console.log('In Search')
            console.log($scope.students);
        }, function (response) {
            console.log(response);
        });
    };

    $scope.ObjecttoParams = function (obj) {
        var p = [];
        for (var key in obj) {
            p.push(key + '=' + encodeURIComponent(obj[key]));
        }
        return p.join('&');
    };

    $scope.Delete = function (student) {

        console.log(student);
        var url = '/api/student/Delete?id='+student.Id;
        console.log('in Edit');
        console.log(student);
        $http({
            method: 'DELETE',
            url: url,
            data: student
        }).then(function (response) {
            console.log('In Delete Response');
            console.log(response)
            $scope.All();
            console.log($scope.students);
        }, function (response) {
            console.log(response);
        });


    };

    $scope.Edit = function (student) {

        var url = '/api/student/Edit/';
        console.log('in Edit');
        console.log(student);
        $http({
            method: 'PUT',
            url: url,
            data: student
        }).then(function (response) {
            console.log('In Edit Response');
            console.log(response)
            $scope.All();
            console.log($scope.students);
        }, function (response) {
            console.log(response);
        });

        
    };


    



});