﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSDesign.EF;
using MvcSDesign.Models;
using System.Data;
using System.Web.Mvc;

namespace MvcSDesign.Repository
{
    interface IAdmin
    {

        // Registration 

        string SaveCompanyProfile(CompanyModel obj);
        CompanyModel GetCompanyProfile();

        string InsertRegistration(staff st);
        string RegistrationUpdate(staff obj);
        string RegistrationDelete(int staffID);
        IEnumerable<staff> SearchRegistration(string name);
        IEnumerable<operation> GetCurrentWorking(string dname, string category, string subcategory);

        string AddSearchProject_ClientName(int projectID);

        //Dashboard
        List<operation> DashBoard_getProjectType();
        List<staff> getTopPerformers();
        List<quotation> getQuotation();

        IEnumerable<operation> Dashboard_getMonthQuotation();
        IEnumerable<QuotationModel> SearchByProjectIDOrName(string opt, string projectID, string name);

        // Quotation
        IEnumerable<operation> getProjectQuotation();
        string UpdateQuotation(int pid, int famount, string projectlocation);
        string QuotationDelete(int projectID);

        operation GetProjectInfo(int projectID);

        //PRF
        PRFModel GetPRFByPrjectID(int projectID);
        string SavePRF(PRFModel obj);

        string DownloadPRF(string projectID, string filelocation);
        IEnumerable<SelectListItem> getOperationDesigner();
        IEnumerable<operation> getProjectAssign();
         
        string SaveProjectAssigned(string projectID, string clientID, string projectCategory, string designerAmount);

        string ProjectRollBack(int pmID);
         
        IEnumerable<operation> getDesignerProjectAmount(int designerID);
        string DesignerAmountCancel(int operationID);
        string CompleteCurrentWorking(int operationID);

        string CurrentWorkingRemarkUpdate(int opID, string remark);
        string DeleteCurrentWorking(string opID);

        string GetFilePath(int pmID, string filename);

        string SendTaskMailToClien(int pmID,int pid, string[] arrFiles, out string uploadedFileName, string gmail);
        string DeleteProjectManagement(int pmID, string uploadedFileName);
        string AmountReceive(int cid, string amount, string remark);
        string SavePayDesigner(int sid, int amount, string remark);
        string ProjectAssigning(operation op);
        operation SearchAddProject(int projectID);
        string SaveNewProject(operation obj);
        staff getLogin(logincls lgn);
        bool DesignerNameValidation(string name);
        bool DesignerEmailValidation(string mailID);


        //Site Visit
        string SaveSiteVisit(int projectID, string fname,  string remark);
        IEnumerable<operation> SearchSiteVisitByNameOrProjectID(string opt, string projectID, string name);
        string DownloadSiteVist(int projectID, string filename);

        string DownloadUploadFile(int projectID, int uploafFileID,  string filename);


        //Report

        IEnumerable<client> RptClientLedger(string cname);
        IEnumerable<operation> RptClientLedgerDetail(int clientID, string fromDt, string toDt, ref DataTable clientDetailRecord);
        IEnumerable<staff> RptDesignerLedger(string dname);
        IEnumerable<operation> RptDesignerLedgerDetail(int sid, string fromDt, string toDt);
        IEnumerable<operation> getDesignerWorkingList(int reg);

        IEnumerable<operation> RptQuotation(string dt1, string dt2, string searchOpt, string projectID, string cname);
        IEnumerable<operation> RptClientReceive(string cname, string fromDt, string toDt);

        IEnumerable<operation> RptSiteVisit(int projectID);

        IEnumerable<operation> RptProjectHistory(int projectID);

        IEnumerable<operation> RptTechnical(string dnama, string fromDt, string toDt);

        IEnumerable<operation> ShowBalanceAdjust(string dt1, string dt2, string cname);

        int ClientPreviousBalance(string clinetID, string dt);
        string SaveRegistration(registration obj);

        string SaveBalanceAdjust(int clientID, int balance, int amount, string remark);
        List<operation> GetClientLedger(int clientID, string fromDt);
        string UpdateLedger(int clientLedgerID, int amount, int receivedAmount, int newAmount, string remark);


    }
}