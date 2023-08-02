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
    $scope.subcategory = ["Presentation Drawing With Furniture Layout", "Floor Plans Ground To Terrace"];
    $scope.subcategoryName = $scope.subcategory[0];


    $scope.addNewCategoryList = ["Presentation Drawing", "Structure Drawing", "Ground Floor Drawing", "First Floor Drawing", "Second Floor Drawing"];
    $scope.addNewCategory = $scope.addNewCategoryList[0];

    $scope.addNewSubcategoryList = ["Presentation Drawing With Furniture Layout", "Floor Plans Ground To Terrace"];
    $scope.addNewSubcategory = $scope.addNewSubcategoryList[0];

    $scope.lblNewErr = "";
    $scope.loading = true;
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




    $scope.GetAddNewSubcategory = function () {
        if ($scope.addNewCategory == "Presentation Drawing") {
            $scope.addNewSubcategoryList = ["Presentation Drawing With Furniture Layout", "Floor Plans Ground To Terrace"];
            $scope.addNewSubcategory = $scope.subcategory[0];
        }
        else if ($scope.addNewCategory == "Structure Drawing") {
            $scope.addNewSubcategoryList = ["Center Line Plan", "Column, Footing, Column and Footing Design", "Plinth Beam Plan And Design", "UG Tank Detail, Septic Tank, Fire Tank, Rain Water Tank"];
            $scope.addNewSubcategory = $scope.subcategory[0];
        }
        else if ($scope.addNewCategory == "Ground Floor Drawings") {
            $scope.addNewSubcategoryList = ["Working Drawing(Measurement)", "Door And Window Schedule", "Lintel Beam", "Staircase Detail Drawing", "Wall Electrical", "Roof Electrical", "Ground Floor Shuttering", "Ground Floor Beam And Slab Design Drawing", "2D Elevation", "2D Elevation Electrical", "Plumbing, Drainage And Rain Water Drawing", "Toilet Plan And Detail, Niche And Other Working Drawing as Per Elevation", "Compound Wall"];
            $scope.addNewSubcategory = $scope.subcategory[0];
        }
        else if ($scope.addNewCategory == "First Floor Drawing") {
            $scope.addNewSubcategoryList = ["Working Drawing(Measurement)", "Door And Window Schedule", "Lintel Beam", "Staircase Detail Drawing", "Wall Electrical", "Roof Electrical", "First Floor Shuttering", "First Floor Beam And Slab Design Drawing", "2D Elevation", "2D Elevation Electrical", "Toilet Plan And Detail, Niche And Other Working Drawing as Per Elevation", "Plumbing, Drainage And Rain Water Drawing"];
            $scope.addNewSubcategory = $scope.subcategory[0];
        }
        else if ($scope.addNewCategory == "Second Floor Drawing") {
            $scope.addNewSubcategoryList = ["Working Drawing(Measurement)", "Door And Window Schedule", "Lintel Beam", "Staircase Detail Drawing", "Wall Electrical", "Roof Electrical", "First Floor Shuttering", "First Floor Beam And Slab Design Drawing", "2D Elevation", "2D Elevation Electrical", "Toilet Plan And Detail, Niche And Other Working Drawing as Per Elevation", "Plumbing, Drainage And Rain Water Drawing", "Perapet Wall"];
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
        //$scope.category = item.category;
        //$scope.subcategory = item.subcategory;
        $scope.lblError = "";
    }

    $scope.TaskSend = function () {

        $scope.fileNameArr = [];
        $scope.fileNameArr.length = 0;

        $scope.lblError = "";

        var chkFacebook = "false";
       // var chkGMail = "false";

        angular.forEach($scope.folder, function (key, value) {
            if (key) $scope.fileNameArr.push(value);
        });

        $scope.folder = {};

        if ($scope.fileNameArr.length == 0) {
            //if ($scope.projectCategory == "3D Model") {
            //    $scope.lblError = " Please select file for 3D Model accept";
            //}
            //else {
                $scope.lblError = " Please select file for send to client";
            //}
            return;
        }
        //$scope.webcanvasload = false;
        alert("chkfacebook 1" + chkFacebook)
        
        chkFacebook = document.getElementById("chkTwitter").checked;
        //chkGMail = document.getElementById("chkGMail").checked;
        alert("Value test 2" + chkFacebook)
      
        $http({
            url: "/Admin/TaskSendToClient",
            dataType: 'json',
            method: 'POST',
            params: {
                pmID: $scope.pmID,
                pid: $scope.projectID,
                filelist: $scope.fileNameArr,
                gmail: chkFacebook
               
                
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {

            var slist = d.data[0];//Massage 
            var ptype = slist.Text;
            //if (slist.Text == "3D Model") {
            //    $('#downloadPopup').modal('hide');
            //    SearchProject();
            //    return;
            //}
            //else if (slist.Text == "Final View") {

            var chkFacebook = document.getElementById("chkTwitter").checked;
                //var chkTwitter = document.getElementById("chkTwitter").checked;
                var headers;
                //if ((chkFacebook) || (chkTwitter)) {

                    var varfacebook = chkFacebook == true ? '1' : '0';
                   // var vartwitter = chkTwitter == true ? '1' : '0';

                    slist = d.data[1];
                    var filearr = slist.Text.split("#");

                    //for (var i = 0; i < filearr.length; i++) {
                    //    var filepath = "http://173.248.151.174:89/WhatsAppMsg/" + filearr[i];


                    //    //alert("filepath " + filepath);
                    //    headers = {
                    //        twitter: vartwitter,
                    //        facebook: varfacebook,
                    //        message: '-',
                    //        uploadPhoto: filepath
                    //    };

                    //    $.ajax({
                    //        url: 'https://designlabinternational.com/dsuite/upload.php',
                    //        type: 'post',
                    //        dataType: 'json',
                    //        data: headers,
                    //        success: function (d) {
                    //            //  alert("Result " + d.data.responseText);
                    //        },
                    //        error: function (d) {
                    //            alert(d.responseText);
                    //        }
                    //    });



                    //}
                //}
            //}
           /* $scope.webcanvasload = true;*/
            slist = d.data[2];

            //if (slist.Text == "No") {
            $scope.msg = "File successfully send to client";
            //    $scope.messagemodel = false;

            //}
            //else {
            //    $scope.msg = ptype + " submitted successfully";
            //    $scope.messagemodel = false;
            //}


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

        $scope.loading = false;
        $http({
            url: '/Admin/AddSearchProject_ClientName',
            dataType: 'json',
            method: 'POST',
            params: {
                pid: $scope.addPID
            },
            contentType: "application/json;charaset=utf-8"
        }).then(function (d) {
            $scope.txtClient1 = d.data;

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


    //$scope.ShowDesignerTask = function () {
    //    $scope.loading = false;
    //    $scope.record = "";

    //    $http({
    //        url: "/Admin/getCurrentWorkingList",
    //        method: "POST",
    //        dataType: "json",
    //        params: {
    //            dname: $scope.dname.Value
    //        },
    //        contentType: "application/json;charaset=utf-8"
    //    }).then(function (d) {
    //        $scope.currentworkinglist = d.data;
    //        $scope.loading = true;
    //        statusCount();
    //        if ($scope.currentworkinglist.length == 0) {
    //            //  alert("Record not available");
    //            $scope.record = "Record not available";
    //            return;
    //        }

    //    }).error(function (err) {
    //        alert(" Error " + err);
    //    });

    //}

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


    //$scope.CompleteCurrentWorking = function () {
    //    $http({
    //        url: "/Admin/CompleteCurrentWorking",
    //        dataType: 'json',
    //        method: 'POST',
    //        params: {
    //            opID: pmID
    //        },
    //        contentType: "application/json;charaset=utf-8"
    //    }).then(function (d) {
    //        if (d.data.name == "") {
    //            alert("Project successfully updated")

    //            location.reload();
    //        }
    //        else
    //            alert("Error :" + d.data.name);
    //    }).error(function (err) {
    //        alert("Error :" + err);
    //    });

    //}



 


});

app.factory('cw', function ($http) {
    var fac = {};
    fac.getRecord = function () {
        return $http.get('/Admin/getOperationDesigner');
    }

    return fac;
});