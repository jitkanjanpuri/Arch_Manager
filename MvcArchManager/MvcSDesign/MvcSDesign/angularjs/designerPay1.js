var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http, designerList) {

    designerList.getRecord().then(function (d) {
        $scope.designerlst = d.data;
        $scope.designerName = d.data[0][0];
    });

    $scope.SearchDesignerAmount = function () {
        var id = $scope.designerName;
        if (id == undefined)
        {
            alert("Please select Designer name");
            return;
        }
       

        $http({
            url: "/Admin/getDesignerProjectAmount",
            method:"POST",
            dataType: "json",
            params: {
                designerID :id
            },
            contentType: "appplication/charaset=utf-8"
        }).then(function(d)
        {
            $scope.designerAmountlist = d.data;
            if ($scope.designerAmountlist.length == 0)
            {
                alert("Record not found");
            }
        }).error(function(err)
        {
          alert("Error :" + err); 
        });
   
    }

    $scope.pay = function(pid, designerID, designeName, amount)
    {
        var con = confirm("Do you want to pay amount " + amount +" of Project "+ pid+ " to " + desingerName + " ?");
        if(con == true)
        {
            $http({
                url: "/Admin/DesignerAmountPaid",
                method: "POST",
                dataType: "json",
                params: {
                    projectID : pid,
                    designerID: designerID,
                    amount : amount
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

    }
});

app.factory('designerList', function ($http) {

    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/getOperationDesigner');
    }
    return fac;
});