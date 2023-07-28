var app = angular.module("myApp", [])
app.controller("myController", function ($scope, $http) {

    $scope.record = "";
    $scope.normal = "0";
    $scope.attention = "0";
    $scope.critical = "0";
    $scope.err = "";
    $scope.projectCategory = ["Presentation Drawing", "Structure Drawing", "Ground Floor Drawing", "First Floor Drawing", "Second Floor Drawing"]
    $scope.category = $scope.projectCategory[0];
    $scope.subcategory = ["Presentation Drawing With Furniture Layout", "Floor Plans Ground To Terrace"];
    $scope.subcategoryName = $scope.subcategory[0];
    $scope.loading = true;
    var operationID = 0;
    var varCategory = "";

    //cw.getRecord().then(function (d) {
    //    //$scope.currentworkinglist = d.data;
    //    //$scope.loading = true;

    //    //  getDesigner();



    //});

    function getDesigner() {
        $http({
            url: "/Admin/getOperationDesigner",
            method: "POST",
            dataType: 'json',
            contentType: 'application/json;charaset=utf-8'
        }).then(function (d) {
            $scope.designerlst = d.data;
            $scope.dname = $scope.designerlst[0];
            $scope.designerList1 = d.data;
            $scope.dName1 = $scope.designerList1[0];

            statusCount();


        }).error(function (err) {
            alert("Error: " + err);
        });
    }

    $scope.GetSubcategory = function () {

        

        if ($scope.category == "Presentation Drawing") {
            $scope.subcategory = ["Presentation Drawing With Furniture Layout", "Floor Plans Ground To Terrace"];
            $scope.subcategoryName = $scope.subcategory[0];
        }
        else if ($scope.category == "Structure Drawing") {
            $scope.subcategory = ["Center Line Plan", "Column, Footing, Column and Footing Design", "Plinth Beam Plan And Design", "UG Tank Detail, Septic Tank, Fire Tank, Rain Water Tank"];
            $scope.subcategoryName = $scope.subcategory[0];
        }
        else if ($scope.category == "Ground Floor Drawings") {
            $scope.subcategory = ["Working Drawing(Measurement)", "Door And Window Schedule", "Lintel Beam", "Staircase Detail Drawing", "Wall Electrical", "Roof Electrical", "Ground Floor Shuttering", "Ground Floor Beam And Slab Design Drawing", "2D Elevation", "2D Elevation Electrical", "Plumbing, Drainage And Rain Water Drawing", "Toilet Plan And Detail, Niche And Other Working Drawing as Per Elevation", "Compound Wall"];
            $scope.subcategoryName = $scope.subcategory[0];
        }
        else if ($scope.category == "First Floor Drawing") {
            $scope.subcategory = ["Working Drawing(Measurement)", "Door And Window Schedule", "Lintel Beam", "Staircase Detail Drawing", "Wall Electrical", "Roof Electrical", "First Floor Shuttering", "First Floor Beam And Slab Design Drawing", "2D Elevation", "2D Elevation Electrical", "Toilet Plan And Detail, Niche And Other Working Drawing as Per Elevation", "Plumbing, Drainage And Rain Water Drawing"];
            $scope.subcategoryName = $scope.subcategory[0];
        }
        else if ($scope.category == "Second Floor Drawing") {
            $scope.subcategory = ["Working Drawing(Measurement)", "Door And Window Schedule", "Lintel Beam", "Staircase Detail Drawing", "Wall Electrical", "Roof Electrical", "First Floor Shuttering", "First Floor Beam And Slab Design Drawing", "2D Elevation", "2D Elevation Electrical", "Toilet Plan And Detail, Niche And Other Working Drawing as Per Elevation", "Plumbing, Drainage And Rain Water Drawing", "Perapet Wall"];
            $scope.subcategoryName = $scope.subcategory[0];
        }
    }

    function statusCount() {
        $scope.normal = "0";
        $scope.attention = "0";
        $scope.critical = "0";

        var normal = 0;
        var attention = 0;
        var critical = 0;

        var arr = [];
        for (var i = 0; i < $scope.currentworkinglist.length; i++) {
            arr = $scope.currentworkinglist[i];
            if (arr.rowcolor == "#FFF") {
                normal++;
            }
            else if (arr.rowcolor == "#ffa500") {
                attention++;
            }
            else if (arr.rowcolor == "#ff0000") {
                critical++;
            }
        }
        $scope.normal = normal;
        $scope.attention = attention;
        $scope.critical = critical;



    }

    //$scope.addNewProject = function () {
    //    $scope.addNewWindow = false;
    //}
    //$scope.CloseAddWindow = function () {
    //    Reset();
    //}

    //function Reset() {
    //    $scope.txtProjectID = "";
    //    $scope.addNewWindow = true;
    //}

    $scope.Search = function () {
        $scope.err = "";
        $scope.workinglist = null;
        $scope.loading = false;
        $http({
            url: '/Admin/SearchCurrentWorking',
            dataType: 'json',
            method: 'POST',
            params: {
                dname :"0",
                category: $scope.category,
                subcategory: $scope.subcategoryName
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.loading = true;

            $scope.workinglist = d.data;
           
           
            if (d.data.length == 0) {
                $scope.err =" Record not available";
                return false;
            }


        }).error(function (err) {
            alert(" Error : " + err);
        });
    }



    $scope.SearchProject = function () {
        var pid = $scope.txtPID;
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
            $scope.txtCName1 = d.data.clientName;
            $scope.txtPType = d.data.projectType;


        }).error(function (err) {
            alert(" Error : " + err);
        });
    }
    $scope.SubmitNewProject = function () {
        var pid = $scope.txtPID
        var cname = $scope.txtCName1;
        var vardname = $scope.dName1.Value;
        var damount = $scope.txtAmount;

        if (pid == undefined) {
            alert("Please enter project ID");
            return false;
        }
        else if (cname == undefined) {
            alert("Please enter project ID");
            return false;
        }
        else if (vardname == undefined) {
            alert("Please select designer name");
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
            if (d.data.clientName == "") {
                alert("Data successfully saved")
                location.reload();
            }
            else
                alert("Error :" + d.data.clientName);

        }).error(function (err) {
            alert("Error :" + err);
        });
    }
    function getCurrentWorkingList() {

        $http({
            url: "/Admin/getCurrentWorkingList",
            method: 'POST',
            dataType: 'json',
            contentType: 'application/json;charaset=utf-8'
        }).then(function (d) {
            $scope.currentworkinglist = d.data;
            if ($scope.currentworkinglist.length == 0) {
                alert("Record not available");
                return;
            }

        }).error(function (err) {
            alert("Error: " + err);
        });
    }


    $scope.ShowDesignerTask = function () {
        $scope.loading = false;
        $scope.record = "";

        $http({
            url: "/Admin/getCurrentWorkingList",
            method: "POST",
            dataType: "json",
            params: {
                dname: $scope.dname.Value
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.currentworkinglist = d.data;
            $scope.loading = true;
            statusCount();
            if ($scope.currentworkinglist.length == 0) {
                //  alert("Record not available");
                $scope.record = "Record not available";
                return;
            }

        }).error(function (err) {
            alert(" Error " + err);
        });

    }

    $scope.EditCurWorking = function (item) {

        $scope.txtProjectID = item.projectID;
        $scope.txtCName = item.clientName;
        $scope.txtProjectType = item.projectType;
        $scope.txtProjectCategory = item.projectCategory;
        $scope.txtRemark = item.remark;
        operationID = item.operationID;
    }


    $scope.CurrentWorkingRemarkUpdate = function () {

        var varremark = $scope.txtRemark;
        if ((varremark == undefined) || (varremark.length == 0)) {
            alert("Please enter remark")
            return;
        }
        //pid: $scope.txtProjectID,
        //    category: $scope.txtProjectCategory,

        $http({
            url: "/Admin/CurrentWorkingRemarkUpdate",
            dataType: 'json',
            method: 'POST',
            params: {
                opID: operationID,
                remark: varremark
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            if (d.data == "success") {
                //   alert(" Remark successfully updated");
                location.reload();

                return;
            }
            else {
                alert(d.data);

            }
        }).error(function (err) {
            alert(" Error " + err);
        });

    }

    $scope.ShowCurrentWorking = function (item) {
        $scope.confirmprojectID = item.projectID;
        varCategory = item.projectCategory;
        operationID = item.operationID;

    }


    $scope.CompleteCurrentWorking = function () {
        //projectID: $scope.confirmprojectID,
        //pcategory: varCategory
        $http({
            url: "/Admin/CompleteCurrentWorking",
            dataType: 'json',
            method: 'POST',
            params: {
                opID: operationID
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            if (d.data.name == "") {
                alert("Project successfully updated")

                location.reload();
            }
            else
                alert("Error :" + d.data.name);
        }).error(function (err) {
            alert("Error :" + err);
        });

    }




    $scope.ShowDelete = function (item) {
        //$scope.projectID = pid;
        //varCategory = category;
        operationID = item.operationID;
    }


    $scope.DeleteCurrentWorking = function () {

        $http({
            url: "/Admin/DeleteCurrentWorking",
            dataType: 'json',
            method: 'POST',
            params: {
                opID: operationID
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            if (d.data.name == "") {
                alert("Project successfully deleted")
                location.reload();

            }
            else
                alert("Error :" + d.data.name);
        }).error(function (err) {
            alert("Error :" + err);
        });
    }



});



//app.factory('cw', function ($http) {

//    var fac = {};
//    fac.getRecord = function () {
//        return $http.get('/Admin/getCurrentWorkingList');
//    }
//    return fac;
//});


