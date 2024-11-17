using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Unity;
using WebsiteBanQuanAo.Services;

namespace WebsiteBanQuanAo
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var container = new UnityContainer();
            container.RegisterType<IVnPayServers, VnPayServer>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies["auth"];
            HttpCookie roleCookie = Request.Cookies["role"];

            if (authCookie != null && roleCookie != null)
            {
                string username = authCookie.Value;
                string role = roleCookie.Value;

                var identity = new GenericIdentity(username);
                var principal = new GenericPrincipal(identity, new[] { role });

                HttpContext.Current.User = principal;
            }
        }
    }
}
