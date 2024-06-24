var app = angular.module("myApp", [])
app.controller("myController", function ($scope, $http, cw) {

    $scope.folder = {};
    $scope.record = "";
    $scope.normal = "0";
    $scope.attention = "0";
    $scope.critical = "0";
    $scope.err = "";

    $scope.lblNewErr = "";
    $scope.showMessage = true;
    $scope.loading = true;
    $scope.loading1 = true;
    $scope.loadingMail = false;
    $scope.loading2 = true;
    $scope.loadingSend = false;

    var pmID = 0;


    cw.getRecord().then(function (d) {
        //$scope.designerList = d.data;
        //$scope.dname = $scope.designerList[0];
        $scope.assignDesignerList = d.data;
        $scope.assignDesigner = $scope.assignDesignerList[0];

        $scope.addNewDesigner = $scope.assignDesignerList;
        $scope.addNewDName = $scope.addNewDesigner[0];
        getCategory();

      

    });

  
    

    function getCategory() {
        
        $http({
            url: "/Admin/GetCategoryDDL",
            dataType: 'json',
            method: 'POST',
        }).then(function (d) {
            /*$scope.projectCategoryList = d.data;*/
            /*$scope.category = $scope.projectCategoryList[0];*/
            $scope.addNewCategoryList = d.data;
            $scope.addNewCategory = $scope.addNewCategoryList[0];

            GetSubcategoryDDL($scope.addNewCategoryList[0].Value);
        });

    }

    function GetSubcategoryDDL(ch) {
       
        $http({
            url: "/Admin/GetSubcategoryDDL",
            dataType: 'json',
            params: {
                id: ch
            },
            method: 'POST',

        }).then(function (d) {
            //$scope.subcategoryList = d.data;
            //$scope.subcategoryName = $scope.subcategoryList[0];

            $scope.addNewSubcategoryList = d.data;
            $scope.addNewSubcategory = $scope.addNewSubcategoryList[0];

            //Status count

            GetAllCurrentProjects();
            

        });
    }


    function GetAllCurrentProjects() {
        $scope.lblMsg = "";
        $scope.workinglist = null;
        $scope.loading = false;
        


        $http({
            url: "/Admin/GetAllCurrentWorking",
            dataType: 'json',
            method: 'POST',
        }).then(function (d) {
            $scope.loading = true;
            $scope.workinglist = d.data;
            if (d.data.length == 0) {
                $scope.lblMsg = " Record not available";
                return false;
            }
            statusCount();
        });

    }






    $scope.GetAddNewSubcategory = function () {

        var ch = $scope.addNewCategory.Value;

        $http({
            url: "/Admin/GetSubcategoryDDL",
            dataType: 'json',
            params: {
                id: ch
            },
            method: 'POST',

        }).then(function (d) {
            $scope.addNewSubcategoryList = d.data;
            $scope.addNewSubcategory = $scope.addNewSubcategoryList[0].Text;
        });


    }






    $scope.GetSubcategory = function () {
        GetSubcategoryDDL($scope.category.Value);
        
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
      //  $scope.lblError = "";
    }




    $scope.TaskSend = function () {

        $scope.fileNameArr = [];
        $scope.fileNameArr.length = 0;

        //$scope.lblError = "";

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
            location.reload();

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
                category: $scope.category.Text,
                subcategory: $scope.subcategoryName.Text
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
        $scope.showMessage = true;
         

         if (($scope.addPID == undefined) || ($scope.addPID.length == 0)) {
            $scope.lblNewErr = "Please enter project ID"
            $scope.showMessage = false;
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
                $scope.showMessage = false;
                return false;
            }
            $scope.txtClient1 = d.data.clientName;
            if (d.data.length == 0) {
                $scope.lblNewErr = "Project ID is not available";
                $scope.showMessage = false;
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
                GetAllCurrentProjects();
                return;
            }
        });
    }


    $scope.SubmitNewProject = function () {

        $scope.lblNewErr = "";
        $scope.showMessage = true;

        
        if (($scope.addPID == undefined) || ($scope.addPID.length == 0)) {
            $scope.lblNewErr = "Please enter project ID";
            $scope.showMessage = false;
            return;
        }
        else if ($scope.txtClient1 == "") {
            $scope.lblNewErr = "Please enter project ID";
            $scope.showMessage = false;
            return;
        }
        else if ($scope.addNewSubcategory == null) {
            $scope.lblNewErr = "Please select subcategory";
            $scope.showMessage = false;
            return;
        }
        else if ($scope.addNewDName.Value == "0") {
            $scope.lblNewErr = "Please select designer name";
            $scope.showMessage = false;
            return;
        }
        $scope.loading1 = false;
        $scope.loadingMail = true;
        var obj = new Object();

        obj.projectID = $scope.addPID;
        obj.category = $scope.addNewCategory.Text;
        obj.subcategory = $scope.addNewSubcategory.Text;
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

               // GetAllCurrentProjects()
            }
            else {
                $scope.lblNewErr = d.data;
                $scope.showMessage = false;
            }

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


    $scope.SavePRF = function () {
        

        if (($scope.pdID == undefined) || ($scope.pdID.length == 0)) {
            alert( "Please enter project ID");
            return;
        }
        else if (($scope.clientNameDescrtion == undefined) || ($scope.clientNameDescrtion == null) ) {
            alert("Please enter project ID");
            return;
        }
        else if (($scope.projectDescription == undefined) || ($scope.projectDescription == null)) {
            alert("Please enter project description");
            return;
        }
        var obj = new Object();
        obj.projectID = $scope.pdID
        obj.anyother = $scope.projectDescription;


        $http({
            url: "/Admin/SavePRF",
            method: 'POST',
            dataType: 'json',
            data: obj,

            contentType: 'application/json;charaset=utf-8'
        }).then(function (d) {
            if (d.data == "") {
                alert("Project description has been successfully saved ");
                window.location.reload();
            }

        }).error(function (err) {
            alert("Error: " + err);
        });
    }


    $scope.ShowCurrentWorking = function (item) {
        $scope.assignProjectID = item.projectID;
        pmID = item.pmID;

    }

    $scope.ClearProjectDescription = function () {
        $scope.pdID ="" ;
        $scope.clientNameDescrtion = "";
        $scope.projectDescription ="" ;

    }

    $scope.SearchDescription = function () {

        $scope.clientNameDescrtion = "";
        $scope.projectDescription = "";

       
        if (($scope.pdID == undefined) || ($scope.pdID == null)) {
            alert( "Please enter project ID");
            return;
        }
        
        $http({
            url: '/Admin/GetPRFByPrjectID',
            dataType: 'json',
            method: 'POST',
            params: {
                projectID: $scope.pdID
            },
            contentType: "application/json; charaset=utf-8"
        }).then(function (d) {
            if (d.data.clientName == null) {
                alert("This project is not available");
                return;
            }
            $scope.clientNameDescrtion = d.data.clientName
            $scope.projectDescription = d.data.remark;
        }).error(function (err) {
            alert(" Error : " + err);
        });

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
                location.reload();
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