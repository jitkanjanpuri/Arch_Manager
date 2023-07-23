var app = angular.module("myApp", []);
app.controller("myController", function ($scope, $http, projectFactory) {

    $scope.assignwindow = true;
    $scope.addNewWindow = true;
    $scope.projectCategory = "Elevation";
    $scope.cmbProjectCategory = "Elevation";

    projectFactory.getRecord().then(function (d) {
        $scope.designerlst = d.data;
        $scope.designerList1 = $scope.designerlst;
        $scope.designerName = $scope.designerlst[0];
         $scope.dName = $scope.designerList1[0];
    
        getProjectAssignList();
    });


    function getProjectAssignList() {
        $http({
            url: "/Admin/getProjectAssignList",
            method: 'POST',
            dataType: 'json',
            contentType: 'application/json;charaset=utf-8'
        }).then(function (d) {
            $scope.projectlist = d.data;
            if ($scope.projectlist.length == 0) {
                alert("Record not available");
                return;
            }

        });
    }

    $scope.ShowAssignWindow = function (clientID, projectID, clientName, designerID, amount) {

        if (designerID == null) {
            alert(" Please select designer name");
            return false;
        }
        $scope.assignwindow = false;
        $scope.projectID = projectID;
        $scope.clientName = clientName;
        $scope.amount = amount;
        $scope.designerAmount = 0;
        $scope.designerID = designerID;

    }

    $scope.addNewProject = function () {
        $scope.addNewWindow = false;
    }

    $scope.CloseAddWindow = function () {
        $scope.txtProjectID = "";
        $scope.addNewWindow = true;
    }

    $scope.CloseAssignWindow = function () {
        Reset();

    }

    function Reset() {
        $scope.assignwindow = true;
        $scope.projectID = 0;
        $scope.clientName = "";
        $scope.amount = 0;
        $scope.designerAmount = 0;
        $scope.designerID = 0;
    }

    $scope.SearchProject = function () {
        var pid = $scope.txtProjectID;
        if (pid == undefined) {
            alert("Please enter project ID   ");
            return false;
        }
        $http({
            url: '/Admin/SearchAddProject',
            dataType: 'json',
            method: 'POST',
            params: {
                projectID: pid
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {

            if (d.data.clientName == "") {
                alert(" Record not available");
                return false;
            }
            $scope.txtClientName = d.data.clientName;
            $scope.txtProjectType = d.data.projectType;


        }).error(function (err) {
            alert(" Error : " + err);
        });
    }


    $scope.SubmitNewProject = function () {
        var pid = $scope.txtProjectID;
        var cname = $scope.txtClientName;
        var vardname = $scope.dName.Text;
        var damount = $scope.txtAmount;

    
        if (pid == undefined) {
            alert("Please enter project ID   ");
            return false;
        }
        else if (cname == undefined) {
            alert("Please enter project ID   ");
            return false;
        }
        else if (vardname == undefined) {
            alert("Please select designer name   ");
            return false;
        }
        else if (damount == undefined) {
            alert("Please enter amount");
            return false;
        }

        $http({
            url: '/Admin/SaveNewProject',
            dataType: 'json',
            method: 'POST',
            params: {
                projectID: pid,
                pcategory: $scope.cmbProjectCategory,
                dname: vardname,
                amount: damount

            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            if (d.data.name == "") {
                alert("Data successfully saved")
                Reset();
                getProjectAssignList();
            }
            else
                alert("Error :" + d.data.name);

        }).error(function (err) {
            alert("Error :" + err);
        });
    }
    $scope.Submit = function () {
        var varDAmount = $scope.designerAmount;
        if (varDAmount == undefined) {
            alert("Please enter finalize amount ");
            return false;
        }

        $http({
            url: '/Admin/SaveProjectAssigned',
            dataType: 'json',
            method: 'POST',
            params: {
                projectID: $scope.projectID,
                staffID: $scope.designerID,
                projectCategory: $scope.projectCategory,
                designerAmount: varDAmount
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            if (d.data.name == "") {
                alert("Data successfully saved")
                Reset();
                getProjectAssignList();
            }
            else
                alert("Error :" + d.data.name);

        }).error(function (err) {
            alert("Error :" + err);
        });
    }


});


app.factory('projectFactory', function ($http) {

    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/getOperationDesigner');
    }
    return fac;
});