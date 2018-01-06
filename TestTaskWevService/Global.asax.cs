using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using TestTaskWevService.DI;

namespace TestTaskWevService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DependencyResolver.GetInstance();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
