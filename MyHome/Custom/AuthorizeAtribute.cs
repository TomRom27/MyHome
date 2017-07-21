using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHome
{

    /// <summary>
    /// This class redefines a behaviour of the default Authorize attribute and in result, in case user is not authorized 403 is returned rather then redirection to login page
    /// Must be in the default namespace of the project!!!
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = true)]
    public class AuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new System.Web.Mvc.HttpStatusCodeResult((int)System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}