using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebShop.Security
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!(new UserManager()).Authorize(this.Roles)) filterContext.Result = new RedirectResult("/User/Login");
        }
    }
}