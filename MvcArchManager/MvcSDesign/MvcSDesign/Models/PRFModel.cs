using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcSDesign.Models
{
    public class PRFModel
    {
       
        public int prfID { get; set; }

        public long projectID { get; set; }
        public string workingStatus { get; set; }
        public string slabdetail { get; set; }
        public string slabheight { get; set; }
        public string plinthheight { get; set; }
        public string porchheight { get; set; }
        public string elevationpattern { get; set; }
        public string totalfloor { get; set; }
        public string towerroom { get; set; }
        public string cornerplotplan { get; set; }
        public string plotside { get; set; }
        public string boundrywall { get; set; }
        public string anyother { get; set; }
        public string doorlintel { get; set; }
        public string windowsill { get; set; }
        public string windowlintel { get; set; }
        

          


    }
}