var app = angular.module("myApp", ['ui.bootstrap'])
app.controller('myController', function ($scope, $http, reg) {


    var subcategory = 0;
    reg.getRecord().then(function (d) {
        $scope.subcategorylist = d.data;
        $scope.totalItems = $scope.subcategorylist.length;
        $scope.currentPage = 1;

    });
    $scope.viewby = 10;

    $scope.itemsPerPage = $scope.viewby;
    $scope.maxSize = 5;


    $scope.setPage = function (pageNo) {
        $scope.currentPage = pageNo;
    };



    $scope.pageChanged = function () {
        console.log('Page changed to: ' + $scope.currentPage);
    };

    $scope.setItemsPerPage = function (num) {
        $scope.itemsPerPage = num;
        $scope.currentPage = 1; //reset to first page
    }

    $scope.ShowDelete = function (id, sname) {

        $scope.subcategoryName = sname;
        subcategory = id;

    }



    $scope.DeleteSubcategory = function () {

        $http({
            url: "/Admin/DeleteSubcategory",
            dataType: 'json',
            method: 'POST',
            params: {
                id: subcategory
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            if (d.data == "") {
                alert("Subcategory successfully deleted");

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
        return $http.get('/Admin/GetAllSubcategory')
    }
    return fac;
});
