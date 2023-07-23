var app = angular.module("myApp", [])
app.controller('myController', function ($scope, $http) {


    $scope.Search = function () {
        var varname = $scope.txtName;
        if ((varname == "") || (varname == undefined)) {
            alert("Please enter client's");
            return false;
        }
        $http({
            url: "/Client/getQuoationClient",
            dataType: 'json',
            method: 'POST',
            params: {
                name: varname
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.recordlist = d.data;
            if ($scope.recordlist.lenght == 0) {
                alert("! Record not found");
                return;
            }
        }).error(function (err) {
            alert("Error : " + err);


        });

    }


    $scope.ShowClient = function (cid, cname) {
        $scope.txtCID = cid;
        $scope.txtCName = cname;

    }


});
