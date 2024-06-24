using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MvcSDesign.EF;
using MvcSDesign.Models;

namespace MvcSDesign.Repository
{
   public interface IClient
    {
        void InsertData(clientModel cnt);
        //long InsertQuotation(quotation qtn);

        string SaveQuotation(operation qtn, List<QuotationItemModel> li, ref int quotationID);
        IEnumerable<QuotationItemModel> GetQuotationDetailItem(int quID);

        //IEnumerable<ProjectItemModel> GetProjectDetailItem(int projectID);
        string UpdateQuotation(operation op, List<QuotationItemModel> itemlist);
        IEnumerable<clientModel> GetAll();
        IEnumerable<clientModel> SearchClientByName(string name);

       // string CompeleteDesignerTask(int pmid , int taskID);
       // string CompeleteDesignerTask(int pmid, int taskID);
        IEnumerable<clientModel> SearchClientByNameOrPID(string opt, string name, string pid, string pname);


        IEnumerable<tblClient> getByName(string name);
        
        string ClientNameValidation(clientModel obj);
        string Update(clientModel cnt);
        string DeleteClient(int clientID);
        IEnumerable<operation> getMonthQuotation(string ptype);
      //  operation getProjectDetail(int projectID);

        operation GetQuotationDetail(int quID);

        clientModel GetClient(int clientID);
        IEnumerable<clientModel> GetState(); 
        IEnumerable<operation> Dashboard_getMonthQuotation();
    }
}
 