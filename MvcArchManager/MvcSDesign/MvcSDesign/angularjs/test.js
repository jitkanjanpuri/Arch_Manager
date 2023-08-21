var app = angular.module("myApp", [])
app.controller('myController', function ($scope, $http, reg) {


    $scope.loading = true;
    var varClientID = 0;

    reg.getRecord().then(function (d) {
        $scope.reglist = d.data;
        
    });
     
     
    $scope.Search = function () {
        SearchClient();
    }

    function SearchClient() {


        var varname = $scope.txtName;
        $scope.errRecord = "";
        $scope.errMessage = "";
        if ((varname == null) || (varname == undefined) || (varname == "")) {
            $scope.errRecord = "Please enter client name";
            return;
        }
        $scope.loading = false;
        $http({
            url: "/Client/SearchClient",
            dataType: 'json',
            method: 'POST',
            params: {
                name: varname
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.reglist = d.data;
            $scope.loading = true;
            if ($scope.reglist.length == 0) {
                $scope.errMessage = "Record not found";
            }
        }).error(function (err) {
            alert("Error : " + err);
        });
    }
     
     
     

});

app.factory('reg', function ($http) {
    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/TestData');
    }

    return fac;
});
