var app = angular.module('courseApp', []);
app.controller('Ctrl', function ($scope, $http, $window) {

    $scope.addCourse = {
        'Code': '',
        'Cname': ''
    }
    $scope.studentcourses = [];
    $scope.apiURL = 'api/v1/course/';
    $scope.apistudentcourse = 'api/v1/studentcourse/';

    $scope.courses = [];
    $scope.searchKeyword = '';

    $scope.init = function () {
        $scope.All();
        $scope.AllStudentCourses();
    }

    $scope.All = function () {
        var url = $scope.apiURL;
        $http({
            method: 'GET',
            url: url,
        }).then(function (response) {
            $scope.courses = response.data;
        }, function (response) {
        });
    };

    $scope.AllStudentCourses = function () {
        var url = $scope.apistudentcourse;
        $http({
            method: 'GET',
            url: url,
        }).then(function (response) {
            $scope.studentcourses = response.data;
        }, function (response) {
        });
    };

    $scope.Add = function (course) {

        var t = $scope.courses.find(function (x) { return x.Code == course.Code });
        if (t != null) {
            $window.alert("Course Already Exist");
            delete $scope.addCourse;
            return;
        }
        var url = $scope.apiURL + 'add/'
        $http({
            method: 'POST',
            url: url,
            data: course,
        }).then(function (response) {
            $scope.All();
            delete $scope.addCourse;
        }, function (response) {
        });
    };

    $scope.Search = function () {
        var url = $scope.apiURL + 'search/' + $scope.searchKeyword;
        $http({
            method: 'GET',
            url: url,
        }).then(function (response) {
            $scope.courses = response.data;
        }, function (response) {
        });
    };


    $scope.Delete = function (course) {

        console.log('In Dlete');
        var sc = $scope.studentcourses.find(function (x) { return x.CourseCode == course.Code })
        if (sc != null) {
            $window.alert("cannot Delete Course Because Students are enroll in a course");
            return;
        }

        var url = $scope.apiURL + 'delete/' + course.Code;
        
        $http({
            method: 'DELETE',
            url: url
        }).then(function (response) {
            $scope.All();
        }, function (response) {
        });


    };

    $scope.Edit = function (course) {

        var url = $scope.apiURL + 'Edit/';
        $http({
            method: 'PUT',
            url: url,
            data: course
        }).then(function (response) {
            $scope.All();
        }, function (response) {
        });


    };

    $scope.change = function (action) {
        switch (action) {
            case 'code':
                if ($scope.addCourse.Code >= 500 || $scope.addCourse.Code < 0 ) {
                    $window.alert('Please give Code between 0-500');
                    $scope.addCourse.Code = '';
                }
                break;
            case 'cname':
                if ($scope.addCourse.Cname.length > 10) {
                    $window.alert('Cname length exceeded 10 character');
                    $scope.addCourse.Cname = '';
                    break;
                }


        }
    }

});