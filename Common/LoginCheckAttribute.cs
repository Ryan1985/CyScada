using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CyScada.BLL;
using CyScada.Model;

namespace CyScada.Web.Common
{
    public class LoginCheckAttribute : AuthorizeAttribute
    {

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                return false;
            }


            //if (httpContext.User.Identity.IsAuthenticated && base.AuthorizeCore(httpContext))
            //{
            //    return ValidateUser();
            //}
            var user = httpContext.Session["User"] as UserModel;
            if (user != null)
            {
                return ValidateUser();
            }

            httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return false;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            if (filterContext.HttpContext.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                filterContext.Result = new RedirectToRouteResult("AccessErrorPage", null);
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.HttpContext.Response.Redirect("Login/Index");
        }

        private bool ValidateUser()
        {
            //TODO: 权限验证
            return true;
        }


        #region ORG
        //protected override bool AuthorizeCore(HttpContextBase httpContext)
        //{
        //    if (httpContext == null)
        //    {
        //        return false;
        //    }

        //    if (httpContext.User.Identity.IsAuthenticated && base.AuthorizeCore(httpContext))
        //    {
        //        return ValidateUser();
        //    }

        //    httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
        //    return false;





        //    //base.AuthorizeCore(httpContext);




        //    //return httpContext.Session != null && (httpContext.Session["User"] as UserModel) != null && !string.IsNullOrEmpty((httpContext.Session["User"] as UserModel).UserId);
        //}

        //protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        //{
        //    base.HandleUnauthorizedRequest(filterContext);
        //    if (filterContext == null)
        //    {
        //        throw new ArgumentNullException("filterContext");
        //    }
        //    else
        //    {
        //        filterContext.HttpContext.Response.Redirect("Login/Index");
        //    }
        //}
        #endregion
    }
}