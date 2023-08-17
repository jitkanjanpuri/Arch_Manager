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
        varClientID = clientID;

    }

   $scope.SearchRegistration = function() {
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

    //$scope.PayDesigner = function () {

    //    if (($scope.txtAmount == undefined) || ($scope.txtAmount == "0")) {
    //        $scope.errReceive = " Please enter amount";
    //        return;
    //    }

    //    $http({
    //        url: "/Admin/SavePayDesigner",
    //        dataType: 'json',
    //        method: 'POST',
    //        params: {
    //            sid: varStaffID,
    //            amount: $scope.txtAmount,
    //            remark: $scope.txtRemark
    //        },
    //        contentType: "application/json; charaset=utf-8"
    //    }).then(function (d) {
    //        if (d.data == "") {

    //            alert("Designer payment successfully saved");
    //            location.reload();
    //            return;
    //        }

    //    }).error(function (err) {
    //        alert("Error " + err);
    //    });


    //}

    //$scope.ShowPayWindow = function (sid, name) {

    //    $scope.txtDName = "";
    //    $scope.txtsid = "";
    //    $scope.txtRemark = "";

    //    $scope.paywindow = false;
    //    $scope.txtDName = name;
    //    $scope.txtsid = sid;

    //}

    //$scope.CloseAmountReceiveWindow = function () {
    //    $scope.paywindow = true;
    //    $scope.txtDName = "";
    //    $scope.txtsid = "";
    //    $scope.txtRemark = "";
    //}
    //$scope.CloseAssignWindow = function () {
    //    Reset();
    //}


    //$scope.Update = function () {
    //    var varstaffID = $scope.txtStaffID;
    //    var varname = $scope.txtName;
    //    var vardesignation = $scope.txtDesination;
    //    var varaddress = $scope.txtAddress;
    //    var varcity = $scope.txtCity;
    //    var varphone = $scope.txtPhone;
    //    var varmobile = $scope.txtMobile;
    //    var varemailID = $scope.txtEmailID;
    //    var varuser = $scope.txtUsername;
    //    var varpassword = $scope.txtPassword;

    //    if ((varname == "") || (varname == undefined)) {
    //        alert("Please enter name");
    //        return false;
    //    }
    //    else if ((varcity == "") || (varcity == undefined)) {
    //        alert("Please enter city");
    //        return false;
    //    }
    //    else if ((varmobile == "") || (varmobile == undefined)) {
    //        alert("Please enter mobile no");
    //        return false;
    //    }
    //    else if ((varemailID == "") || (varemailID == undefined)) {
    //        alert("Please enter email ID");
    //        return false;
    //    }
    //    else if ((varuser == "") || (varuser == undefined)) {
    //        alert("Please enter email ID");
    //        return false;
    //    }
    //    else if (varuser.trim().length < 4) {
    //        alert("User must be more than 4 character");
    //        return false;
    //    }
    //    else if ((varpassword == "") || (varpassword == undefined)) {
    //        alert("Please enter email ID");
    //        return false;
    //    }
    //    else if (varpassword.trim().length < 4) {
    //        alert("Password must be more than 4 character");
    //        return false;
    //    }

    //    $http({
    //        url: "/Admin/RegistrationUpdate",
    //        dataType: 'json',
    //        method: 'POST',
    //        params: {
    //            staffID: varstaffID,
    //            name: varname,
    //            designation: vardesignation,
    //            address: varaddress,
    //            city: varcity,
    //            phone: varphone,
    //            mobile: varmobile,
    //            emailID: varemailID,
    //            user: varuser,
    //            password: varpassword
    //        },
    //        contentType: "application/json; charaset=utf-8"
    //    }).then(function (d) {
    //        if (d.data == "") {
    //            alert("Data successfully updated");
    //            Reset();
    //            SearchRegistration();

    //        }
    //        else {
    //            alert(d.data);
    //        }

    //    }).error(function (err) {
    //        alert("Error : " + err);
    //    });
    //}

    $scope.RegistrationDelete = function () {

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

 