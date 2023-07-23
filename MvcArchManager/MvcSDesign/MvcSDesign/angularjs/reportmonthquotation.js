var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {

    $scope.loading = true;
    $scope.arrProjectType = ["All", "Exterior", "Interior", "3D_Floor", "Structure", "Planning", "Gujarat", "Walkthrough", "Bird_Eye_View", "Interactive_View(Exterior)", "Interactive_View(Interior)"];

    $scope.ptype = $scope.arrProjectType[0];
    $scope.recordmsg = "";

         


        //$scope.Search = function () {
        //    var varname = $scope.txtName;
        //    $scope.err = "";
        //    if ((varname == "") || (varname == undefined)) {
        //        $scope.err = "Please enter client name";
        //        return false;
        //    }
        //    $scope.loading = true;
        //    $http({
        //        url: "/Client/getClient",
        //        dataType: 'json',
        //        method: 'POST',
        //        params: {
        //            name: varname
        //        },
        //        contentType: "application/json; charaset=utf-8"
        //    }).then(function (d) {
        //        $scope.recordlist = d.data;
        //        $scope.loading = true;
        //        if ($scope.recordlist.lenght == 0) {
        //            $scope.err = "! Record not found";

        //        }
        //    }).error(function (err) {
        //        alert("Error : " + err);


        //    });

        //}


        
        $scope.ShowAllQuotation = function () {
            var varptype = $scope.ptype;
            var pendingqty = 0;
            var pendingamount = 0;
            var confirmqty = 0;
            var confirmamount = 0;
            var qlist;
            $scope.recordmsg = "";
            $scope.loading = false;

            $scope.pendingquotationamount = "0";
            $scope.confirmquotationamount = "0";
            $scope.pendingquotation = "0";
            $scope.confirmquotation = "0";

            $http({
                url: "/Client/getMonthQuotation",
                dataType: 'json',
                method: 'POST',
                params: {
                    ptype: varptype
                },
                contentType: "application/json;charaset=utf-8"
            }).then(function (d) {
                $scope.quotationlist = d.data;
                $scope.loading = true;

                if ($scope.quotationlist.length == 0) {
                    $scope.recordmsg = "Record not available";
                    return;
                }

                for (var i = 0; i < $scope.quotationlist.length; i++) {
                    qlist = $scope.quotationlist[i];

                    if (qlist.status == "Pending") {
                        pendingqty++;
                        pendingamount += qlist.amount;
                    }
                    else {
                        confirmqty++;
                        confirmamount += qlist.amount;
                    }
                }

                $scope.pendingquotationamount = pendingamount;
                $scope.confirmquotationamount = confirmamount;
                $scope.pendingquotation = pendingqty;
                $scope.confirmquotation = confirmqty;

            }).error(function (err) {
                alert(" Error  : " + err);
            });

        }


    });
