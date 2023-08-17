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
        long InsertQuotation(quotation qtn);
        IEnumerable<tblClient> getAll();
        IEnumerable<client> SearchClientByName(string opt, string name, string cityname);
        IEnumerable<tblClient> getByName(string name);
        bool ClientEmailValidation(string mailID);
        string ClientNameValidation(string name, string emailID);
        string Update(client cnt);
        string DeleteClient(int clientID);
        IEnumerable<operation> getMonthQuotation(string ptype);
        IEnumerable<operation> getProjectDetail(int projectID);
        client GetClient(int clientID);
        IEnumerable<operation> Dashboard_getMonthQuotation();
    }
}
 