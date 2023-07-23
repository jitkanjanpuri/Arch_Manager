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
        void InsertData(client cnt);
        int InsertQuotation(quotation qtn);
        IEnumerable<tblClient> getAll();
        IEnumerable<tblClient> SearchByName(string chkName, string name, string chkcity, string cityname);
        IEnumerable<tblClient> getByName(string name);

        //IEnumerable<client> getClient_PromMail(string name, string city);
        ////string SendPromoMail(string title, string msg, string[] clientList);
        bool ClientEmailValidation(string mailID);
        string ClientNameValidation(string name, string emailID);
        string Update(client cnt);
        string DeleteClient(int clientID);
        IEnumerable<operation> getMonthQuotation( string ptype);
        //string InsertInteriorQuotation(operation obj, string empdata , ref int pid,  ref DataTable dt);
        IEnumerable<operation> getProjectDetail(int projectID);
        ////IEnumerable<tblInteriorProjectDetail> getInteriorProjectDetail(int projectID);
        ////void SaveStatus(string ch);

        client GetClient(int clientID);

        IEnumerable<operation> Dashboard_getMonthQuotation();
    }
}
 