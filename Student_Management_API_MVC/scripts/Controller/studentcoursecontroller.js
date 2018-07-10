var app = angular.module('studentcourseApp', []);
app.controller('Ctrl', function ($scope, $http, $window) {
    $scope.addStudent = {
        'Id': '',
        'Fname': '',
        'Lname': '',
        'Age': '',
        'Email': ''
    }

    $scope.apiURL = 'api/v1/studentcourse/';
    $scope.apicourseURL = 'api/v1/course/';
    $scope.apistudentURL = 'api/v1/student/';

    $scope.students = [];
    $scope.courses = [];
    $scope.studentcourses = [];
    $scope.searchKeyword = '';
    $scope.addGrade = '';
    $scope.addCourse = '';
    $scope.addStudent = '';

    $scope.init = function () {
        $scope.All();
        $scope.AllStudents();
        $scope.AllCourses();
    }

    $scope.All = function () {
        var url = $scope.apiURL;
        $http({
            method: 'GET',
            url: url,
        }).then(function (response) {
            $scope.studentcourses = response.data;
        }, function (response) {
        });
    };
    $scope.AllStudents = function () {
        var url = $scope.apistudentURL;
        $http({
            method: 'GET',
            url: url,
        }).then(function (response) {
            $scope.students = response.data;
        }, function (response) {
        });
    };

    $scope.AllCourses = function () {
        var url = $scope.apicourseURL;
        $http({
            method: 'GET',
            url: url,
        }).then(function (response) {
            $scope.courses = response.data;
        }, function (response) {
        });
    };

    $scope.Add = function () {

        var t = $scope.studentcourses.find(function (x) { return x.CourseCode == $scope.addCourse.Code && x.StudentId == $scope.addStudent.Id });
        if (t != null) {
            $window.alert("Student Already enroll in a course");
            delete $scope.addCourse;
            delete $scope.addStudent;
            delete $scope.addGrade;
            return;
        }
        var url = $scope.apiURL + 'add/'
        $http({
            method: 'POST',
            url: url,
            data: { "CourseCode":$scope.addCourse.Code,
                "StudentId":$scope.addStudent.Id ,"Grade":$scope.addGrade }
        }).then(function (response) {
            $scope.All();
            delete $scope.addStudent;
        }, function (response) {
        });
    };

    $scope.Search = function () {
        var url = $scope.apiURL + 'search/' + $scope.searchKeyword;
        $http({
            method: 'GET',
            url: url,
        }).then(function (response) {
            $scope.studentcourses = response.data;
        }, function (response) {
        });
    };

    $scope.ObjecttoParams = function (obj) {
        var p = [];
        for (var key in obj) {
            p.push(key + '=' + encodeURIComponent(obj[key]));
        }
        return p.join('&');
    };

    $scope.Delete = function (studentcourse) {

        var url = $scope.apiURL + 'delete/' + studentcourse.CourseCode + '/' + studentcourse.StudentId;
        $http({
            method: 'DELETE',
            url: url
        }).then(function (response) {
            $scope.All();
        }, function (response) {
        });


    };

    $scope.Edit = function (obj) {
        var c = $scope.courses.find(function (x) { return x.Code == obj.CourseCode })
        var s = $scope.students.find(function (x) { return x.Id == obj.StudentId })
        //console.log($scope.studentcourses)
        //var sc = $scope.studentcourses.find(function (x) { return x.StudentId == obj.StudentId && x.CourseCode == obj.CourseCode })


        if (c == null || s == null)
        {
            $window.alert("Course/Student Doesn't Exist")
            obj.CourseCode = '';
            obj.StudentId = '';
        }

        //if (sc != null)
        //{
        //    $window.alert("Student already enroll in a Course")

        //}
        else
        {
            var url = $scope.apiURL + 'Edit/';
            $http({
                method: 'PUT',
                url: url,
                data: obj
            }).then(function (response) {
                $scope.All();
            }, function (response) {
            });
        }    
    };

    $scope.change = function (action,obj) {
        switch (action) {
            case 'grade':
                if ($scope.addGrade.length > 1) {
                    $window.alert('Please enter grade Between A-F');
                    $scope.addGrade = '';
                }
                break;
         }
    }

});