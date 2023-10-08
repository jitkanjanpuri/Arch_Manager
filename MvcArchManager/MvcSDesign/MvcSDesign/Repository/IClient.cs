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

        string SaveQuotation(operation qtn, string empdata, ref long pid);
        IEnumerable<ProjectItemModel> GetProjectDetailItem(int projectID);
        string UpdateQuotation(operation op,List<ProjectItemModel> itemlist );
        IEnumerable<clientModel> GetAll();
        IEnumerable<clientModel> SearchClientByName(string name);

        IEnumerable<clientModel> SearchClientByNameOrPID(string opt, string name, string pid, string pname);


        IEnumerable<tblClient> getByName(string name);
        
        string ClientNameValidation(clientModel obj);
        string Update(clientModel cnt);
        string DeleteClient(int clientID);
        IEnumerable<operation> getMonthQuotation(string ptype);
        IEnumerable<operation> getProjectDetail(int projectID);
        clientModel GetClient(int clientID);
        IEnumerable<clientModel> GetState(); 
        IEnumerable<operation> Dashboard_getMonthQuotation();
    }
}
 