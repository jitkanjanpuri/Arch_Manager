using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvcSDesign.EF;
using MvcSDesign.Models;
using System.Data;

namespace MvcSDesign.Repository
{
    interface IAdmin
    {

        // Registration 

        string SaveCompanyProfile(CompanyModel obj);
        CompanyModel GetCompanyProfile();

        void InsertRegistration(staff st);
        string RegistrationUpdate(staff obj);
        string RegistrationDelete(int staffID);
        //IEnumerable<tblStaff> SearchRegistration(string name);


        //Dashboard
        List<operation> DashBoard_getProjectType();
        List<staff> getTopPerformers();
        List<quotation> getQuotation();

        IEnumerable<operation> Dashboard_getMonthQuotation();


        // Quotation
        IEnumerable<operation> getProjectQuotation();
        string UpdateQuotation(int pid, int famount, string projectlocation);
        string QuotationDelete(int projectID);


        //PRF

        string SavePRF(PRFModel obj);
        IEnumerable<tblStaff> getOperationDesigner();
        IEnumerable<operation> getProjectAssign();

        //string SaveGMail(string gmailID, string pwd);
        //List<GMail> getGmail();
        //string RemoveGMailAccount(int id);
        string SaveProjectAssigned(string projectID, string clientID, string projectCategory, string designerAmount);




        // Operation
        IEnumerable<operation> getCurrentWorkingList(string dname);
        IEnumerable<operation> getDesignerProjectAmount(int designerID);
        string DesignerAmountCancel(int operationID);


        // Current working
         //string CompleteCurrentWorking(string projectID, string pcategory);
        string CompleteCurrentWorking(int operationID);

        string CurrentWorkingRemarkUpdate(int opID, string remark);
        string DeleteCurrentWorking(string opID);

        string AmountReceive(int cid, string amount, string remark);
        string SavePayDesigner(int sid, int amount, string remark);

        operation SearchAddProject(int projectID);
        string SaveNewProject(int projectID, string pcategory, string dname, int amount);
        staff getLogin(logincls lgn);
        bool DesignerNameValidation(string name);
        bool DesignerEmailValidation(string mailID);

        IEnumerable<client> RptClientLedger(string cname);
        IEnumerable<operation> RptClientLedgerDetail(int clientID, string fromDt, string toDt, ref DataTable clientDetailRecord);
        IEnumerable<staff> RptDesignerLedger(string dname);
        IEnumerable<operation> RptDesignerLedgerDetail(int sid, string fromDt, string toDt);
        IEnumerable<operation> getDesignerWorkingList(int reg);

        IEnumerable<operation> RptQuotation(string ptype, string dt1, string dt2, string searchOpt, string projectID, string cname);
        IEnumerable<operation> RptClientReceive(string cname, string fromDt, string toDt);

        IEnumerable<operation> ShowBalanceAdjust(string dt1, string dt2, string cname);

        int ClientPreviousBalance(string clinetID, string dt);
        string SaveRegistration(registration obj);

        string SaveBalanceAdjust(int clientID, int balance, int amount, string remark);
        //GMail getGMailAccount();
        //void UpdateGMailPassword(GMail obj);


        //void SaveStatus(string ch);


        

        List<operation> GetClientLedger(int clientID, string fromDt);

        string UpdateLedger(int clientLedgerID, int amount, int receivedAmount, int newAmount, string remark);


    }
}
