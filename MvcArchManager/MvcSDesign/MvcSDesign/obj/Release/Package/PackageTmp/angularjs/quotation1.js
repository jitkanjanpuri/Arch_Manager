var app = angular.module("myApp", [])
app.controller('myCtr', function ($scope, $http) {

    $scope.saveErr = "";
    $scope.loading = true;
    $scope.err = "";
    $scope.total = 0;
    $scope.loading2 = true;



    $scope.Search = function () {
        var varname = $scope.txtName;
        $scope.err = "";
        if ((varname == "") || (varname == undefined)) {
            $scope.err = "Please enter client name";
            return false;
        }
        $scope.loading = false;
        $http({
            url: "/Client/getClient",
            dataType: 'json',
            method: 'POST',
            params: {
                name: varname
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.recordlist = d.data;
            $scope.loading = true;
            if ($scope.recordlist.lenght == 0) {
                $scope.err = "! Record not found";

            }
        });


    }

    $scope.SaveQuotation = function () {


        $scope.saveErr = "";

        if (($scope.txtProjectName == "") || ($scope.txtProjectName == undefined)) {
            $scope.saveErr = "Please enter project name";
            document.getElementById("txtProjectName").focus();
            return false;
        }
        else if (($scope.txtService == "") || ($scope.txtService == undefined)) {
            $scope.saveErr = "Please enter service";
            document.getElementById("txtService").focus();
            return false;
        }
        else if (($scope.qtItemList == undefined) || ($scope.qtItemList.lenght == 0)) {
            $scope.saveErr = "Pleae enter item in quotation list";
            return false;

        }

        var cid = document.getElementById("txtCID").value;
        var remark = $scope.remark;     ///  document.getElementById("remark").value;
        $scope.loading2 = false;

        $http({
            url: "/Client/SaveQuotation",
            dataType: 'json',
            method: 'POST',
            params: {
                cid: cid,
                projectname: $scope.txtProjectName,
                service: $scope.txtService,
                remark: remark
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            alert(d.data);
            location.reload();
        });




    }




    $scope.SetClientID = function (id, name) {
        document.getElementById("txtCID").value = id;
        document.getElementById("txtName").value = name;
    }

    $scope.AddItem = function () {
        var obj = new Object();

        if ($scope.projectDetails == undefined || $scope.projectDetails == "") {
            document.getElementById("").focus();
            alert("Please enter project details");
            return;
        }
        else if ($scope.services == undefined || $scope.services == "") {
            document.getElementById("").focus();
            alert("Please enter services ");
            return;
        }

        else if ($scope.qty == undefined || $scope.qty == 0) {
            document.getElementById("qty").focus();
            alert("Please enter qty ");
            return;
        }
        else if ($scope.rate == undefined || $scope.rate == 0) {
            document.getElementById("rate").focus();
            alert("Please enter rate ");
            return;
        }

        var selectUnit = document.getElementById("unit");
        var optValue = selectUnit.options[selectUnit.selectedIndex].value;

        obj.projectDetails = $scope.projectDetails;
        obj.services = $scope.services;
        obj.hsn = $scope.hsn;
        obj.qty = $scope.qty;
        obj.unit = optValue;
        obj.rate = $scope.rate;

        $http({
            url: "/Client/Quotation_AddItem",
            dataType: "json",
            method: "POST",
            data: obj,
            contentType: "application/json;charaset =utf-8",
        }).then(function (d) {
            $scope.qtItemList = d.data;
            $scope.projectDetails = "";
            $scope.services = "";
            $scope.hsn = "";
            $scope.qty = "";
            $scope.rate = "";
            GetTotal();
        });


    }



    function GetTotal() {

        var total = 0;
        var arr1;
        if ($scope.qtItemList != undefined) {
            for (var i = 0; i < $scope.qtItemList.length; i++) {
                arr1 = $scope.qtItemList[i];

                total = total + arr1["amount"];
            }

            $scope.total = total;
        }
    }

    $scope.RemoveItem = function (item) {

        $http({
            url: "/Client/Quotation_RemoveItem",
            dataType: "json",
            method: "POST",
            params:
            {
                prjDetail: item.projectDetails,
                service: item.services
            },
            contentType: "application/json;charaset =utf-8",
        }).then(function (d) {
            $scope.qtItemList = d.data;
            $scope.projectDetails = "";
            $scope.services = "";
            $scope.hsn = "";
            $scope.qty = "";
            $scope.rate = "";
            GetTotal();
        });

    }


});
