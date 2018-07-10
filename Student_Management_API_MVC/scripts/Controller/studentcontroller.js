var app = angular.module('studentApp', []);
app.controller('Ctrl', function ($scope, $http, $window) {
    $scope.addStudent = {
        'Id': '',
        'Fname': '',
        'Lname': '',
        'Age': '',
        'Email':''
    }

    $scope.studentcourses = [];
    $scope.apistudentcourse = 'api/v1/studentcourse/';
    $scope.apiURL = 'api/v1/student/';
    $scope.students = [];
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
            $scope.students = response.data;
        }, function (response) {
            $window.alert(response);
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


    $scope.Add = function (student) {
        
        var t = $scope.students.find(function (x) { return x.Id == student.Id });
        if (t != null)
        {
            $window.alert("Student Already Exist");
            delete $scope.addStudent;
            return;
        }
            
        var url = $scope.apiURL + 'add/'
        $http({
            method: 'POST',
            url: url,
            data: student,
        }).then(function (response) {
            $scope.All();
            delete $scope.addStudent;
        }, function (response) {
            $window.alert(response);
        });
    };

    $scope.Search = function () {
        var url = $scope.apiURL + 'search/' + $scope.searchKeyword;
        $http({
            method: 'GET',
            url: url,
        }).then(function (response) {
            $scope.students = response.data;
        }, function (response) {
            //$window.alert(response);
        });
    };

    $scope.Delete = function (student) {

        console.log('In Dlete');
        var sc = $scope.studentcourses.find(function (x) { return x.StudentId == student.Id })
        if (sc != null)
        {
            $window.alert("cannot Delete Student Because he/she is enroll in a course");
            return;
        }

        var url = $scope.apiURL+'delete/'+student.Id;
        $http({
            method: 'DELETE',
            url: url
        }).then(function (response) {
            $scope.All();
        }, function (response) {
            $window.alert(response);
        });


    };

    $scope.Edit = function (student) {

        var url = $scope.apiURL+'Edit/';
        $http({
            method: 'PUT',
            url: url,
            data: student
        }).then(function (response) {
            $scope.All();
        }, function (response) {
            $window.alert(response);
        });

        
    };

    $scope.change = function (action)
    {
        switch (action) {
            case 'id':
                if ($scope.addStudent.Id > 400000 || $scope.addStudent.Id < 0)
                {
                    $window.alert('Please give Id between 0-400000');
                    $scope.addStudent.Id = '';
                }
                break;
            case 'fname':
                if ($scope.addStudent.Fname.length > 10) {
                    $window.alert('Fname length exceeded 10 character');
                    $scope.addStudent.Fname = '';
                }
                break;
            case 'lname':
                if ($scope.addStudent.Lname.length > 10) {
                    $window.alert('Lname length exceeded 10 character');
                    $scope.addStudent.Lname = '';
                }
                break;

            case 'age':
                if ($scope.addStudent.Age > 30 || $scope.addStudent.Age < 1) {
                    $window.alert('Age must be between 17-30');
                    $scope.addStudent.Age = '';
                }
                break;
       }
    }
    
});