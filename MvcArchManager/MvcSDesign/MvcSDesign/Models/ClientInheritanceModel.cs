using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSDesign.Models
{
    public class ClientInheritanceModel : clientModel
    {
        public int ID { get; set; }
        public int amount { get; set; }
    }
}