var app = angular.module("myApp", ['ui.bootstrap'])
app.controller('myController', function ($scope, $http, reg) {


    $scope.loading = true;
    var varClientID = 0;
     
    reg.getRecord().then(function (d) {
        $scope.reglist = d.data;
        $scope.totalItems = $scope.reglist.length;

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
        document.getElementById("clientID").value = item.clientID;
        document.getElementById("clientName").value = item.clientName;
        document.getElementById("orgName").value = item.orgName;
        document.getElementById("address").value = item.address;
        document.getElementById("city").value = item.city;
        document.getElementById("state").value = item.state;
        document.getElementById("mobile").value = item.mobile;
        document.getElementById("phone").value = item.phone;
        document.getElementById("emailID").value = item.emailID;
    }

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


    //$scope.AllClient = function () {
    //    $http({
    //        url: "/Client/AllClient",
    //        dataType: 'json',
    //        method: 'POST',
    //        contentType: "application/json; charaset=utf-8"
    //    }).then(function (d) {
    //        $scope.allclient = d.data;
    //        if ($scope.allclient.length == 0) {
    //            alert("Record not available");
    //            return;
    //        }
    //    }).error(function (err) {
    //        alert("Error : " + err);
    //    });
    //}

    //Amount Receive from client
    //$scope.ShowAmountReceive = function (cid, cname) {
    //    $scope.errReceive = "";
    //    varClientID = cid;
    //    $scope.txtCName = cname;
    //    $scope.txtAmount = "";
    //    $scope.txtRemark = "";
    //}


    //$scope.AmountReceiveSave = function () {

    //    var varcname = $scope.txtCName;
    //    var varamount = $scope.txtAmount;
    //    var varremark = $scope.txtRemark;
    //    $scope.errReceive = "";
    //    if (varcname == undefined) {
    //        $scope.errReceive = "Please select client for amount receive ";
    //        return;
    //    }
    //    else if (varamount == undefined) {
    //        $scope.errReceive = ""; "Please enter amount";
    //        return;
    //    }
    //    else if ((varamount == "") || (varamount == "0")) {
    //        $scope.errReceive = "Please enter amount";
    //        return;
    //    }


    //    $http({
    //        url: "/Admin/AmountReceive",
    //        dataType: 'json',
    //        method: 'POST',
    //        params: {
    //            clientID: varClientID,
    //            amount: varamount,
    //            remark: varremark

    //        },
    //        contentType: "application/json;charaset=utf-8"
    //    }).then(function (d) {
    //        if (d.data == "Success") {
    //            alert("Client amount successfully received");
    //            location.reload();
    //            return;
    //        }

    //    }).error(function (err) {
    //        alert("Error : " + err);
    //    });

    //}

    //$scope.GetValue = function (cid, clientName, orgName, address, city, state, phone, mobile, emailID) {

    //    $scope.clientID = cid;
    //    $scope.txtClientName = clientName;
    //    $scope.txtOrgName = orgName;
    //    $scope.txtAddress = address;
    //    $scope.txtCity = city;
    //    $scope.statename = state,
    //        $scope.txtPhone = phone;
    //    $scope.txtMobile = mobile;
    //    $scope.txtEmailID = emailID;
    //    $scope.assignwindow = false;

    //}

    //$scope.CloseAssignWindow = function () {
    //    Reset();

    //}




    //$scope.UpdateClient = function () {

    //    var varcid = $scope.clientID;
    //    var varname = $scope.txtClientName;
    //    var varorgName = $scope.txtOrgName;
    //    var varaddress = $scope.txtAddress;
    //    var varcity = $scope.txtCity;

    //    var varphone = $scope.txtPhone;
    //    var varmobile = $scope.txtMobile;
    //    var varemailID = $scope.txtEmailID;

    //    if ((varname == "") || (varname == undefined)) {
    //        alert("Please enter client name");
    //        return false;
    //    }
    //    else if ((varcity == "") || (varcity == undefined)) {
    //        alert("Please enter city ");
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


    //    $http({
    //        url: "/Client/UpdateClient",
    //        dataType: 'json',
    //        method: 'POST',
    //        params: {
    //            cid: varcid,
    //            name: varname,
    //            orgName: varorgName,
    //            address: varaddress,
    //            city: varcity,
    //            state: $scope.statename,
    //            phone: varphone,
    //            mobile: varmobile,
    //            emailID: varemailID
    //        },
    //        contentType: "application/json; charaset=utf-8"
    //    }).then(function (d) {
    //        if (d.data == "") {
    //            alert("Data successfully updated");
    //            Reset();
    //            SearchClient();
    //        }
    //        else {
    //            alert(d.data);
    //        }

    //    }).error(function (err) {
    //        alert("Error : " + err);
    //    });
    //}


    $scope.ShowDelete = function (clientID, cname) {

        $scope.clientname = cname;
        varClientID = clientID;

    }



    $scope.Delete = function () {

        $http({
            url: "/Client/DeleteClient",
            dataType: 'json',
            method: 'POST',
            params: {
                cid: varClientID
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            if (d.data == "") {
                alert("Client successfully deleted");

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
        return $http.get('/Client/AllClient');
    }

    return fac;
});
