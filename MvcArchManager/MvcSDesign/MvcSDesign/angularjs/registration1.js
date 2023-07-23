﻿var app = angular.module("myApp", [])
app.controller('myController', function ($scope, $http) {

    $scope.assignwindow = true;
    $scope.paywindow = true;

    $scope.nettotal = 0;

    $scope.Search = function () {
        SearchRegistration();
    }

    function SearchRegistration() {
        var varname = $scope.txtSearchName;

        $http({
            url: "/Admin/SearchRegistration",
            dataType: 'json',
            method: 'POST',
            params: {
                name: varname
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {


            if (d.data.length == 0) {
                alert("Record not found");
                return;
            }
            $scope.stafflist = d.data;

        }).error(function (err) {
            alert("Error : " + err);

        });

    }

    $scope.PayDesigner = function () {
         
        if (($scope.txtAmount == undefined) || ($scope.txtAmount == "0")) {
            alert(" Please enter amount");
            return;
        }

        $http({
            url: "/Admin/SavePayDesigner",
            dataType: 'json',
            method: 'POST',
            params: {
                sid: $scope.txtsid,
                amount: $scope.txtAmount,
                remark: $scope.txtRemark
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            if (d.data == "") {
                $scope.paywindow = true;
                $scope.txtDName = "";
                $scope.txtsid = "";
                $scope.txtRemark = "";
                alert("Designer payment successfully saved");
                return;
            }

        }).error(function (err) {
            alert("Error " + err);
        });


    }

    $scope.ShowPayWindow = function (sid, name) {
        
        $scope.txtDName = "";
        $scope.txtsid = "";
        $scope.txtRemark = "";

        $scope.paywindow = false;
        $scope.txtDName = name;
        $scope.txtsid = sid;

    }

    $scope.CloseAmountReceiveWindow = function () {
        $scope.paywindow = true;
        $scope.txtDName = "";
        $scope.txtsid = "";
        $scope.txtRemark = "";
    }
    $scope.CloseAssignWindow = function () {
        Reset();
    }



    function Reset() {
        $scope.txtStaffID = "";
        $scope.txtName = "";
        $scope.txtDesination = "";
        $scope.txtAddress = "";
        $scope.txtCity = "";
        $scope.txtPhone = "";
        $scope.txtMobile = "";
        $scope.txtEmailID = "";

        $scope.assignwindow = true;
    }

    $scope.GetValue = function (staffID, name, designation, address, city, phone, mobile, emailID, username, password) {

        $scope.txtStaffID = staffID;
        $scope.txtName = name;
        $scope.txtDesination = designation;
        $scope.txtAddress = address;
        $scope.txtCity = city;
        $scope.txtPhone = phone;
        $scope.txtMobile = mobile;
        $scope.txtEmailID = emailID;
        $scope.txtUsername = username;
        $scope.txtPassword = password;

        $scope.assignwindow = false;

    }

    $scope.Update = function () {
        var varstaffID = $scope.txtStaffID;
        var varname = $scope.txtName;
        var vardesignation = $scope.txtDesination;
        var varaddress = $scope.txtAddress;
        var varcity = $scope.txtCity;
        var varphone = $scope.txtPhone;
        var varmobile = $scope.txtMobile;
        var varemailID = $scope.txtEmailID;
        var varuser = $scope.txtUsername;
        var varpassword = $scope.txtPassword;

        if ((varname == "") || (varname == undefined)) {
            alert("Please enter name");
            return false;
        }
        else if ((varcity == "") || (varcity == undefined)) {
            alert("Please enter city");
            return false;
        }
        else if ((varmobile == "") || (varmobile == undefined)) {
            alert("Please enter mobile no");
            return false;
        }
        else if ((varemailID == "") || (varemailID == undefined)) {
            alert("Please enter email ID");
            return false;
        }
        else if ((varuser == "") || (varuser == undefined)) {
            alert("Please enter email ID");
            return false;
        }
        else if (varuser.trim().length < 4) {
            alert("User must be more than 4 character");
            return false;
        }
        else if ((varpassword == "") || (varpassword == undefined)) {
            alert("Please enter email ID");
            return false;
        }
        else if (varpassword.trim().length < 4) {
            alert("Password must be more than 4 character");
            return false;
        }

        $http({
            url: "/Admin/RegistrationUpdate",
            dataType: 'json',
            method: 'POST',
            params: {
                staffID: varstaffID,
                name: varname,
                designation: vardesignation,
                address: varaddress,
                city: varcity,
                phone: varphone,
                mobile: varmobile,
                emailID: varemailID,
                user: varuser,
                password: varpassword
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            if (d.data == "") {
                alert("Data successfully updated");
                Reset();
                SearchRegistration();

            }
            else {
                alert(d.data);
            }

        }).error(function (err) {
            alert("Error : " + err);
        });
    }

    $scope.RegistrationDelete = function (staffID, name) {
        var con = confirm("Do you want to delete  " + name + " ?");
        if (con) {
            $http({
                url: "/Admin/RegistrationDelete",
                dataType: 'json',
                method: 'POST',
                params: {
                    sid: staffID
                },
                contentType: "application/json; charaset=utf-8"
            }).then(function (d) {
                if (d.data == "") {
                    alert("Data successfully deleted");
                    SearchRegistration();
                    Reset();
                }
                else {
                    alert(d.data);
                }

            }).error(function (err) {
                alert("Error : " + err);
            });
        }
    }

});
