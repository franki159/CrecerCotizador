using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.Classes.Setting
{  
    
    public class CheckFilter : ActionFilterAttribute, IActionFilter  
        {            
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                HttpContext context = HttpContext.Current;
                if (context.Session == null || System.Web.HttpContext.Current.Session["Username"] == null)
                {
                    
                    filterContext.Result = new RedirectResult("~/Security/Process");                    
                   return;
                }               

                base.OnActionExecuting(filterContext);
            }
        
    }
}