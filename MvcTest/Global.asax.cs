using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MvcTest
{
	public class MvcApplication : HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			GlobalFilters.Filters.Add(new DynamicActionFilter(), 0);
		}
	}

	public class DynamicActionFilter : ActionFilterAttribute
	{
		public override void OnResultExecuting(ResultExecutingContext filterContext)
		{
			filterContext.Controller.ViewBag.AppName = "Dynamic Components Samples";
		}
	}
}