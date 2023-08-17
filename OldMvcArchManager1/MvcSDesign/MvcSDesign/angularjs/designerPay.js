var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http, designerList) {

    designerList.getRecord().then(function (d) {
        $scope.designerlst = d.data;
        $scope.designerName = d.data[0][0];
    });

    $scope.SearchDesignerAmount = function () {
        SearchData();
    }

    function SearchData()
    {
        var id = $scope.designerName;
        if (id == undefined) {
            alert("Please select Designer name");
            return;
        }

        $http({
            url: "/Admin/getDesignerProjectAmount",
            method: "POST",
            dataType: "json",
            params: {
                designerID: id
            },
            contentType: "appplication/charaset=utf-8"
        }).then(function (d) {
            $scope.designerAmountlist = d.data;
            if ($scope.designerAmountlist.length == 0) {
                alert("Record not found");
            }
        }).error(function (err) {
            alert("Error :" + err);
        });
    }

    $scope.payDesigner = function (operationID, pid, amount) {
        
        var con = confirm("Do you want to pay amount " + amount + " against of Project " + pid + " ?");
        if (con == true) {
            $http({
                url: "/Admin/DesignerAmountCancel",
                method: "POST",
                dataType: "json",
                params: {
                    operationID: operationID 
                },
                contentType: "appplication/charaset=utf-8"
            }).then(function (d) {
                if (d.data.name == "Done")
                {
                    alert("Data successfylly saved");
                    SearchData();
                }
                else
                {
                    alert(d.data.name);
                }

            }).error(function (err) {
                alert("Error : " + err);
            });
        }

    }
    $scope.AmountCancel = function (operationID, pid) {

        var con = confirm("Do you want to cancel amount of Project " + pid + " ?");
        if (con == true) {
            $http({
                url: "/Admin/DesignerAmountCancel",
                method: "POST",
                dataType: "json",
                params: {
                    operationID: operationID
                },
                contentType: "appplication/charaset=utf-8"
            }).then(function (d) {
                if (d.data.name == "Done") {
                    alert("Successfully project canceled");
                    SearchData();
                }
                else {
                    alert(d.data.name);
                }

            }).error(function (err) {
                alert("Error : " + err);
            });
        }

    }
});

app.factory('designerList', function ($http) {

    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/getOperationDesigner');
    }
    return fac;
});