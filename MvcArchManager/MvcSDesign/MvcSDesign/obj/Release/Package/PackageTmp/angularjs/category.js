var app = angular.module("myApp", [])
app.controller('myController', function ($scope, $http, reg) {


    var categoryID = 0;
    reg.getRecord().then(function (d) {
        $scope.categorylist = d.data;
    });


    $scope.ShowDelete = function (id, cname) {

        $scope.categoryName = cname;
        categoryID = id;
    }

    $scope.DeleteCategory = function () {

        $http({
            url: "/Admin/DeleteCategory",
            dataType: 'json',
            method: 'POST',
            params: {
                id: categoryID
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            if (d.data == "") {
                alert("Category successfully deleted");

                location.reload();
            }
            else {
                alert("Error " + d.data);
            }

        }).error(function (err) {
            alert("Error : " + err);
        });

    }

});
app.factory('reg', function ($http) {
    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/GetAllCategory')
    }
    return fac;
});
