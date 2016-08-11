using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace CyScada.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            OpcClient.OpcClient.StartClient();
            int count = 20;

            Application["total"] = count;
            Application["online"] = 0;

        }
        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }
        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码
        }
        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码
            Session.Timeout = 1;
            Application.Lock();
            Application["total"] = System.Convert.ToInt32(Application["total"]) + 1;
            Application["online"] = System.Convert.ToInt32(Application["online"]) + 1;
            Application.UnLock();
        }
        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
            // 或 SQLServer，则不会引发该事件。
            Application.Lock();
            Application["online"] = System.Convert.ToInt32(Application["online"]) - 1;
            Application["total"] = System.Convert.ToInt32(Application["total"]) - 1;
            Application.UnLock();
        }

    
     
        //public override void Dispose()
        //{
        //    OpcClient.OpcClient.StopClient();
        //    base.Dispose();
        //}

    }
}