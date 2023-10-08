var app = angular.module("myApp", [])
app.controller('myCtr', function ($scope, $http) {


    $scope.loading = true;
    $scope.err = "";


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
        }).error(function (err) {
            alert("Error : " + err);


        });

    }

    $scope.SetClientID = function (id, name) {
        document.getElementById("txtCID").value = id;
        document.getElementById("txtName").value = name;
    }

    function addProuctCart(pid, productName, price, dis, varqty) {
        $http({
            url: "/Home/addProductCard",
            method: "POST",
            dataType: "json",
            params: {
                pid: pid,
                pname: productName,
                price: price,
                discount: dis,
                qty: varqty,
                orderType: 'Regular'
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            $scope.cartcount = d.data;
            viewCart();
        }).error(function (err) {
            alert("Error " + err);
        });

    }
    $scope.removeCartItem = function (item) {
        $http({
            url: "/Home/removeCartItem",
            method: "POST",
            dataType: "json",
            params: {
                pid: item.productID,
                pname: item.productName
            },
            contentType: "application/json; charaset = utf-8"
        }).then(function (d) {
            $scope.viewlist = d.data;
            $scope.cartcount = $scope.viewlist.length;

            //CalculateProduct();
        }).error(function (err) {

        });
    }

});
