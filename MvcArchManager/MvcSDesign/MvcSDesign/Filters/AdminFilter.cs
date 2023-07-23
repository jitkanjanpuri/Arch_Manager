using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcSDesign.Filters
{
    public class AdminFilter :  ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(!(Convert.ToBoolean(filterContext.HttpContext.Session["IsAdmin"]) || Convert.ToBoolean(filterContext.HttpContext.Session["IsUser"])))
            {
                filterContext.Result = new ContentResult()
                {
                    Content = "Unathorize to access sepcified resource."
                };
            }
        }

    }
}

 