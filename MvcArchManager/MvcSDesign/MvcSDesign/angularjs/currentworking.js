var app = angular.module("myApp", [])
app.controller("myController", function ($scope, $http, cw) {

    $scope.folder = {};
    $scope.record = "";
    $scope.normal = "0";
    $scope.attention = "0";
    $scope.critical = "0";
    $scope.err = "";
    $scope.projectCategory = ["Presentation Drawing", "Structure Drawing", "Ground Floor Drawing", "First Floor Drawing", "Second Floor Drawing"]
    $scope.category = $scope.projectCategory[0];
    $scope.subcategory = ["Presentation Drawing With Furniture Layout Water Tank UG Rain Water Tank", "Revised Presentation Drawing With Furniture Layout Water Tank UG Rain Water Tank", "Floor Plans Ground To Terrace", "Revised Floor Plans Ground To Terrace"];
    $scope.subcategoryName = $scope.subcategory[0];


    $scope.addNewCategoryList = ["Presentation Drawing", "Structure Drawing", "Ground Floor Drawing", "First Floor Drawing", "Second Floor Drawing"]
    $scope.addNewCategory = $scope.addNewCategoryList[0];

    $scope.addNewSubcategoryList = ["Presentation Drawing With Furniture Layout Water Tank UG Rain Water Tank", "Revised Presentation Drawing With Furniture Layout Water Tank UG Rain Water Tank", "Floor Plans Ground To Terrace", "Revised Floor Plans Ground To Terrace"];
    $scope.addNewSubcategory = $scope.addNewSubcategoryList[0];

    $scope.lblNewErr = "";
    $scope.loading = true;
    $scope.loading1 = true;
    $scope.loadingMail = false;
    $scope.loading2 = true;
    $scope.loadingSend = false;

    var pmID = 0;


    cw.getRecord().then(function (d) {
        $scope.designerList = d.data;
        $scope.dname = $scope.designerList[0];
        $scope.assignDesignerList = $scope.designerList;
        $scope.assignDesigner = $scope.assignDesignerList[0];

        $scope.addNewDesigner = $scope.designerList;
        $scope.addNewDName = $scope.addNewDesigner[0];


    });


    $scope.GetSubcategory = function () {
        if ($scope.category == "Presentation Drawing") {
            $scope.subcategory = ["Presentation Drawing With Furniture Layout Water Tank UG Rain Water Tank", "Revised Presentation Drawing With Furniture Layout Water Tank UG Rain Water Tank", "Floor Plans Ground To Terrace", "Revised Floor Plans Ground To Terrace"];
            $scope.subcategoryName = $scope.subcategory[0];
        }
        else if ($scope.category == "Structure Drawing") {
            $scope.subcategory = ["Center Line Plan", "Revised Center Line Plan", "Column Footing Column and Footing Design", "Revised Column Footing Column and Footing Design", "Plinth Beam Plan And Design", "Revised Plinth Beam Plan And Design", "UG Tank Detail Septic Tank Fire Tank Rain Water Tank", "Revised UG Tank Detail Septic Tank Fire Tank Rain Water Tank"];
            $scope.subcategoryName = $scope.subcategory[0];
        }
        else if ($scope.category == "Ground Floor Drawings") {
            $scope.subcategory = ["Working Drawing", "Revised Working Drawing", "Door and Window Schedule", "Revised Door and Window Schedule", "Lintel Beam", "Revised Lintel Beam", "StairCase Detail Drawing", "Revised StairCase Detail Drawing", "Wall Electrical", "Revised Wall Electrical", "Roof Electrial", "Revised Roof Electrial", "Ground Floor Shuttering", "Revised Ground Floor Shuttering", "Ground Floor Beam and Slab Design", "Revised Ground Floor Beam and Slab Design", "2D Elevation", "Revised 2D Elevation", "2D Elevation Electrical", "Revised 2D Elevation Electrical", "Plumbing Drainage and Rain Water Drawing", "Revised Plumbing Drainage and Rain Water Drawing", "Toilet Plan and Detail Niche and Other Working Drawing", "Revised Toilet Plan and Detail Niche and Other Working Drawing", "Compound Wall", "Revised Compound Wall"];
            $scope.subcategoryName = $scope.subcategory[0];
        }
        else if ($scope.category == "First Floor Drawing") {
            $scope.subcategory = ["Working Drawing", "Revised Working Drawing", "Door and Window Schedule", "Revised Door and Window Schedule", "Lintel Beam", "Revised Lintel Beam", "StairCase Detail Drawing", "Revised Staircase Detail Drawing", "Wall Electrical", "Revised Wall Electrical", "Roof Electrial", "Revised Roof Electrial", "First Floor Shuttering", "Revised First Floor Shuttering", "First Floor Beam and Slab Design", "Revised First Floor Beam and Slab Design", "2D Elevation", "Revised 2D Elevation", "2D Elevation Electrical", "Revised 2D Elevation Electrical", "Toilet Plan and Detail Niche and Other Working Drawing", "Revised Toilet Plan and Detail Niche and Other Working Drawing", "Plumbing Drainage and Rain Water", "Revised Plumbing Drainage and Rain Water"];
            $scope.subcategoryName = $scope.subcategory[0];
        }
        else if ($scope.category == "Second Floor Drawing") {
            $scope.subcategory = ["Working Drawing", "Revised Working Drawing", "Door and Window Schedule", "Revised Door and Window Schedule", "Lintel Beam", "Revised Lintel Beam", "StairCase Detail Drawing", "Revised StairCase Detail Drawing", "Wall Electrical", "Revised Wall Electrical", "Roof Electrial", "Revised Roof Electrial", "First Floor Shuttering", "Revised First Floor Shuttering", "First Floor Beam and Slab Design", "Revised First Floor Beam and Slab Design", "2D Elevation", "Revised 2D Elevation", "2D Elevation Electrical", "Revised 2D Elevation Electrical", "Toilet Plan and Detail Niche and Other Workinge", "Revised Toilet Plan and Detail Niche and Other Workinge", "Plumbing Drainage and Rain Water", "Revised Plumbing Drainage and Rain Water", "Perapet Wall", "Revised Perapet Wall"];
            $scope.subcategoryName = $scope.subcategory[0];
        }
    }




    $scope.GetAddNewSubcategory = function () {
        if ($scope.addNewCategory == "Presentation Drawing") {
            $scope.addNewSubcategoryList = ["Presentation Drawing With Furniture Layout Water Tank UG Rain Water Tank", "Revised Presentation Drawing With Furniture Layout Water Tank UG Rain Water Tank", "Floor Plans Ground To Terrace", "Revised Floor Plans Ground To Terrace"];
            $scope.addNewSubcategory = $scope.subcategory[0];
        }
        else if ($scope.addNewCategory == "Structure Drawing") {
            $scope.addNewSubcategoryList = ["Center Line Plan", "Revised Center Line Plan", "Column Footing Column and Footing Design", "Revised Column Footing Column and Footing Design", "Plinth Beam Plan And Design", "Revised Plinth Beam Plan And Design", "UG Tank Detail Septic Tank Fire Tank Rain Water Tank", "Revised UG Tank Detail Septic Tank Fire Tank Rain Water Tank"];
            $scope.addNewSubcategory = $scope.subcategory[0];
        }
        else if ($scope.addNewCategory == "Ground Floor Drawings") {
            $scope.addNewSubcategoryList = ["Working Drawing", "Revised Working Drawing", "Door and Window Schedule", "Revised Door and Window Schedule", "Lintel Beam", "Revised Lintel Beam", "StairCase Detail Drawing", "Revised StairCase Detail Drawing", "Wall Electrical", "Revised Wall Electrical", "Roof Electrial", "Revised Roof Electrial", "Ground Floor Shuttering", "Revised Ground Floor Shuttering", "Ground Floor Beam and Slab Design", "Revised Ground Floor Beam and Slab Design", "2D Elevation", "Revised 2D Elevation", "2D Elevation Electrical", "Revised 2D Elevation Electrical", "Plumbing Drainage and Rain Water Drawing", "Revised Plumbing Drainage and Rain Water Drawing", "Toilet Plan and Detail Niche and Other Working Drawing", "Revised Toilet Plan and Detail Niche and Other Working Drawing", "Compound Wall", "Revised Compound Wall"];
            $scope.addNewSubcategory = $scope.subcategory[0];
        }
        else if ($scope.addNewCategory == "First Floor Drawing") {
            $scope.addNewSubcategoryList = ["Working Drawing", "Revised Working Drawing", "Door and Window Schedule", "Revised Door and Window Schedule", "Lintel Beam", "Revised Lintel Beam", "StairCase Detail Drawing", "Revised Staircase Detail Drawing", "Wall Electrical", "Revised Wall Electrical", "Roof Electrial", "Revised Roof Electrial", "First Floor Shuttering", "Revised First Floor Shuttering", "First Floor Beam and Slab Design", "Revised First Floor Beam and Slab Design", "2D Elevation", "Revised 2D Elevation", "2D Elevation Electrical", "Revised 2D Elevation Electrical", "Toilet Plan and Detail Niche and Other Working Drawing", "Revised Toilet Plan and Detail Niche and Other Working Drawing", "Plumbing Drainage and Rain Water", "Revised Plumbing Drainage and Rain Water"];
            $scope.addNewSubcategory = $scope.subcategory[0];
        }
        else if ($scope.addNewCategory == "Second Floor Drawing") {
            $scope.addNewSubcategoryList = ["Working Drawing", "Revised Working Drawing", "Door and Window Schedule", "Revised Door and Window Schedule", "Lintel Beam", "Revised Lintel Beam", "StairCase Detail Drawing", "Revised StairCase Detail Drawing", "Wall Electrical", "Revised Wall Electrical", "Roof Electrial", "Revised Roof Electrial", "First Floor Shuttering", "Revised First Floor Shuttering", "First Floor Beam and Slab Design", "Revised First Floor Beam and Slab Design", "2D Elevation", "Revised 2D Elevation", "2D Elevation Electrical", "Revised 2D Elevation Electrical", "Toilet Plan and Detail Niche and Other Workinge", "Revised Toilet Plan and Detail Niche and Other Workinge", "Plumbing Drainage and Rain Water", "Revised Plumbing Drainage and Rain Water", "Perapet Wall", "Revised Perapet Wall"];
            $scope.addNewSubcategory = $scope.subcategory[0];
        }
    }


    function statusCount() {
        $scope.normal = "0";
        $scope.attention = "0";
        $scope.critical = "0";

        var normal = 0;
        var attention = 0;
        var submitted = 0;
        var arr = [];
        for (var i = 0; i < $scope.workinglist.length; i++) {
            arr = $scope.workinglist[i];
            if (arr.rowcolor == "#FFF") {
                normal++;
            }
            else if (arr.rowcolor == "#ffa500") {
                attention++;
            }
            else if (arr.rowcolor == "#E3FF00") {
                submitted++;
            }
        }
        $scope.normal = normal;
        $scope.attention = attention;
        $scope.submitted = submitted;
    }

    $scope.Search = function () {
        SearchProject();
    }

    $scope.ShowFilePopup = function (item) {
        $scope.filelist = item.arr;
        $scope.pmID = item.pmID;
        $scope.projectID = item.projectID;
        $scope.lblError = "";
    }

    $scope.TaskSend = function () {

        $scope.fileNameArr = [];
        $scope.fileNameArr.length = 0;

        $scope.lblError = "";

        var chkWhatsApp = "false";
        var chkGMail = "false";

        angular.forEach($scope.folder, function (key, value) {
            if (key) $scope.fileNameArr.push(value);
        });

        $scope.folder = {};

        if ($scope.fileNameArr.length == 0) {
            $scope.lblError = " Please select file for send to client";
            return;
        }

        chkGMail = document.getElementById("chkGMail").checked;
        chkWhatsApp = document.getElementById("chkWhatsApp").checked;
        $scope.loading2 = false;
        $scope.loadingSend = true;
        $http({
            url: "/Admin/TaskSendToClient",
            dataType: 'json',
            method: 'POST',
            params: {
                pmID: $scope.pmID,
                pid: $scope.projectID,
                filelist: $scope.fileNameArr,
                gmail: chkGMail,
                whatsApp: chkWhatsApp

            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {

            var slist = d.data[0];//Massage 
            var ptype = slist.Text;
            $scope.loading2 = true;
            $scope.loadingSend = false;
            if (ptype == "Y") {
                $scope.lblError = "File successfully send to client";
            }

            $('.bs-example-modal-send').modal('hide');
            SearchProject();
        });
    }



    function SearchProject() {
        $scope.lblMsg = "";
        $scope.workinglist = null;
        $scope.loading = false;
        $http({
            url: '/Admin/SearchCurrentWorking',
            dataType: 'json',
            method: 'POST',
            params: {
                dname: $scope.dname.Text,
                category: $scope.category,
                subcategory: $scope.subcategoryName
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.loading = true;
            $scope.workinglist = d.data;
            if (d.data.length == 0) {
                $scope.lblMsg = " Record not available";
                return false;
            }
            statusCount();

        }).error(function (err) {
            alert(" Error : " + err);
        });
    }



    $scope.AddNewSearchProject = function () {
        $scope.lblNewErr = "";
        $scope.txtClient1 = "";
        if (($scope.addPID == undefined) || ($scope.addPID.length == 0)) {
            $scope.lblNewErr = "Please enter project ID"
            return;
        }

        $scope.loading1 = false;
        $http({
            url: '/Admin/AddSearchProject_ClientName',
            dataType: 'json',
            method: 'POST',
            params: {
                pid: $scope.addPID
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.loading1 = true;
            if (d.data.status == "request") {
                $scope.lblNewErr = "Request is pending in Project Management";
                return false;
            }
            $scope.txtClient1 = d.data.clientName;
            if (d.data.length == 0) {
                $scope.lblNewErr = "Project ID is not available";
                return false;
            }

        }).error(function (err) {
            alert(" Error : " + err);
        });
    }


    $scope.ClearTextValue = function () {
        $scope.addNewTechRemark = "";
    }

    $scope.RollBack = function (item) {
        $scope.rollbackProjectID = item.projectID
        pmID = item.pmID

    }


    $scope.RollBackConfirm = function () {
        $http({
            url: "/Admin/ProjectRollBack",
            dataType: "json",
            method: "GET",
            params: {
                pmID: pmID,
            },
            contentType: "application/json; charaset =utf-8",
        }).then(function (result) {

            if (result.data == "Y") {
                $(".bs-example-modal-rollbackPopup").modal('hide')
                SearchProject();
                return;
            }
        });
    }


    $scope.SubmitNewProject = function () {

        $scope.lblNewErr = "";


        if (($scope.addPID == undefined) || ($scope.addPID.length == 0)) {
            $scope.lblNewErr = "Please enter project ID";
            return;
        }
        else if ($scope.txtClient1 == "") {
            $scope.lblNewErr = "Please enter project ID";
            return;
        }
        else if ($scope.addNewSubcategory == null) {
            $scope.lblNewErr = "Please select subcategory";
            return;
        }
        else if ($scope.addNewDName.Value == "0") {
            $scope.lblNewErr = "Please select designer name";
            return;
        }
        $scope.loading1 = false;
        $scope.loadingMail = true;
        var obj = new Object();

        obj.projectID = $scope.addPID;
        obj.category = $scope.addNewCategory;
        obj.subcategory = $scope.addNewSubcategory;
        obj.designerName = $scope.addNewDName.Value;
        obj.remark = $scope.addNewTechRemark;


        $http({
            url: '/Admin/SaveNewProject',
            dataType: 'json',
            method: 'POST',
            data: obj,
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.loading = true;
            $scope.loadingMail = false;
            $scope.loading1 = true;
            if (d.data == "") {
                alert("Record successfully saved")
                location.reload();
            }
            else
                $scope.lblNewErr = d.data;

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



    //$scope.EditCurWorking = function (item) {

    //    $scope.txtProjectID = item.projectID;
    //    $scope.txtCName = item.clientName;
    //    $scope.txtProjectType = item.projectType;
    //    $scope.txtProjectCategory = item.projectCategory;
    //    $scope.txtRemark = item.remark;
    //    pmID = item.pmID;
    //}


    //$scope.CurrentWorkingRemarkUpdate = function () {

    //    var varremark = $scope.txtRemark;
    //    if ((varremark == undefined) || (varremark.length == 0)) {
    //        alert("Please enter remark")
    //        return;
    //    }

    //    $http({
    //        url: "/Admin/CurrentWorkingRemarkUpdate",
    //        dataType: 'json',
    //        method: 'POST',
    //        params: {
    //            opID: pmID,
    //            remark: varremark
    //        },
    //        contentType: "application/json;charaset=utf-8"
    //    }).then(function (d) {
    //        if (d.data == "success") {
    //            //   alert(" Remark successfully updated");
    //            location.reload();

    //            return;
    //        }
    //        else {
    //            alert(d.data);

    //        }
    //    }).error(function (err) {
    //        alert(" Error " + err);
    //    });

    //}

    $scope.ShowCurrentWorking = function (item) {
        $scope.assignProjectID = item.projectID;
        pmID = item.pmID;

    }
    $scope.ProjectAssigned = function () {


        $scope.lblAssinError = "";
        var vardID = $scope.assignDesigner.Value;
        var remark = $scope.techRemark;
        if (vardID == "0") {
            $scope.lblAssinError = "Please select designer name";
            return;
        }
        var obj = new Object();

        obj.pmID = pmID;
        obj.projectID = $scope.assignProjectID;;
        obj.designerName = vardID;
        obj.techremark = $scope.techRemark;


        $http({
            url: "/Admin/ProjectAssigning",
            method: "POST",
            dataType: "json",
            data: { op: obj },
            contentType: "application/json; charaset =utf-8"
        }).then(function (result) {

            if (result.data == "Success") {
                $scope.techRemark = "";
                $(".bs-example-modal-Assign").modal('hide');
                SearchProject();
                return;
            }


        });
    };


});

app.factory('cw', function ($http) {
    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/getOperationDesigner');
    }

    return fac;
});