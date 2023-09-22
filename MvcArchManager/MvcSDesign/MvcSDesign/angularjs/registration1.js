var app = angular.module("myApp", ['ui.bootstrap'])
app.controller('myController', function ($scope, $http, staff) {

    $scope.loading = true;
    var varStaffID = 0;

    staff.getRecord().then(function (d) {
        $scope.stafflist = d.data;
        $scope.totalItems = $scope.stafflist.length;

        $scope.currentPage = 1;

    });

    $scope.viewby = 10;

    $scope.itemsPerPage = $scope.viewby;
    $scope.maxSize = 5; //Number of pager buttons to show

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




    $scope.editRecord = function (item) {
        document.getElementById("staffID").value = item.staffID;
        document.getElementById("name").value = item.name;
        document.getElementById("designation").value = item.designation;
        document.getElementById("address").value = item.address;
        document.getElementById("city").value = item.city;
        document.getElementById("mobile").value = item.mobile;
        document.getElementById("phone").value = item.phone;
        document.getElementById("rolltype").value = item.rolltype;
        document.getElementById("emailID").value = item.emailID;
        document.getElementById("username").value = item.username;
        document.getElementById("password").value = item.password;

    }

    $scope.ShowDelete = function (clientID, cname) {
        $scope.clientname = cname;
        varStaffID = clientID;

    }

    $scope.SearchRegistration = function () {
        var varname = $scope.txtName;

        $scope.errRecord = "";
        $scope.errMessage = "";
        if ((varname == null) || (varname == undefined) || (varname == "")) {
            $scope.errRecord = "Please enter staff name";
            return;
        }
        $scope.loading = false;
        $http({
            url: "/Admin/SearchRegistration",
            dataType: 'json',
            method: 'POST',
            params: {
                name: varname
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.stafflist = d.data;
            $scope.loading = true;

            if (d.data.length == 0) {
                $scope.errMessage = "Record not found";
                //return;
            }


        }).error(function (err) {
            alert("Error : " + err);

        });

    }



    $scope.RegistrationDelete = function () {
        alert("Staff ID " + varStaffID);

        $http({
            url: "/Admin/RegistrationDelete",
            dataType: 'json',
            method: 'POST',
            params: {
                sid: varStaffID
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            if (d.data == "") {
                alert("Record successfully deleted");
                location.reload();
            }
            else {
                alert(d.data);
            }

        }).error(function (err) {
            alert("Error : " + err);
        });

    }
});


app.factory('staff', function ($http) {
    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/GatAllRegistration')
    };
    return fac;
});

