var app = angular.module("myApp", [])
app.controller("myController", function ($scope, $http) {

    $scope.loading = true;

    $scope.Search = function () {
        var pid = $scope.varProjectID;
        var name = $scope.name;
        var opt = $scope.opt;
        $scope.record = "";
        if (opt == undefined) {
            $scope.record = "Please select at least one option";
            return;
        }
        else if ((opt == "projectID") && ((pid == null) || (pid == undefined) || (pid == ""))) {
            $scope.record = "Please enter project ID";
            return;
        }
        else if ((opt == "name") && ((name == null) || (name == undefined) || (name == ""))) {
            $scope.record = "Please enter client name";
            return;
        }


        $http({
            url: '/Admin/SearchSiteVisitByNameOrProjectID',
            dataType: 'json',
            method: 'POST',
            params: {
                opt: opt,
                projectID: pid,
                cname: name
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.sitevisitlist = d.data;
            if (d.data.length == 0) {
                $scope.record = " Record not available";
                return false;
            }
        }).error(function (err) {
            alert(" Error : " + err);
        });
    }

    $scope.ShowCurrentWorking = function (item) {
        document.getElementById("txtProjectID").value = item.projectID;

        $http({
            url: '/Admin/GetPRFByPrjectID',
            dataType: 'json',
            method: 'POST',
            params: {
                projectID: item.projectID
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
           
            document.getElementById("txtWorkingStatus").value = d.data.workingStatus;
            document.getElementById("txtSlabHeight").value = d.data.slabheight;


            document.getElementById("txtPlinthHeight").value = d.data.plinthheight;
            document.getElementById("txtPorchHeigh").value = d.data.porchheight;
            document.getElementById("ddlElevationPattern").value = d.data.elevationpattern;
            document.getElementById("txtDoorLintel").value = d.data.doorlintel;

            document.getElementById("txtWindowSill").value = d.data.windowsill;
            document.getElementById("txtWindowLinte").value = d.data.windowlintel;
            document.getElementById("txtAnyOther").value = d.data.anyother;
            document.getElementById("txtPlotFaceSide").value = d.data.plotside;

            alert(d.data.towerroom);
            if (d.data.towerroom == "Yes") {
                document.getElementById("rdTowerRoomYes").checked = true;
            }
            else
            {
                document.getElementById("rdTowerRoomNo").checked = true;
            }

            if (d.data.cornerplotplan == "Yes") {
                document.getElementById("rdCornerPlotYes").checked = true;
            }
            else {
                document.getElementById("rdCornerPlotNo").checked = true;
            }

            if (d.data.boundrywall == "Yes") {
                document.getElementById("rdBoundaryWallDesignYes").checked = true;
            }
            else {
                document.getElementById("rdBoundaryWallDesignNo").checked = true;
            }


        }).error(function (err) {
            alert(" Error : " + err);
        });

    }


});

