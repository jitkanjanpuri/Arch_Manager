var app = angular.module("myApp", [])
app.controller("myController", function ($scope, $http, cw) {

    $scope.amountreceivewindow = true;

   
 cw.getRecord().then(function (d) {
           $scope.designerWrokingList = d.data;

    });
    //function getCurrentWorkingList() {

    //    $http({
    //        url: "/Designer/getCurrentWorkingList",
    //        method: 'POST',
    //        dataType: 'json',
    //        contentType: 'application/json;charaset=utf-8'
    //    }).then(function (d) {
    //        $scope.currentworkinglist = d.data;
    //        if ($scope.currentworkinglist.lenth == 0) {
    //            alert("Record not available");
    //            return;
    //        }

    //    }).error(function (err) {
    //        alert("Error: " + err);
    //    });
    //}
 
    
   
});



app.factory('cw', function ($http) {

    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Designer/DesignerWorkingList');
    }
    return fac;
});


