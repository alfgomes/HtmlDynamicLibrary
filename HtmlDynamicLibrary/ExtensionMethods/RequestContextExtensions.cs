using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace HtmlDynamicLibrary.ExtensionMethods
{
	public static class RequestContextExtensions
	{
		public static AuthorizationContext GetAuthorizationContext(this RequestContext @self, string controllerName, string actionName, RouteValueDictionary routeValues)
		{
			object area;
			string areaName = string.Empty;
			string key = controllerName + " " + actionName;

			if (routeValues != null && routeValues.TryGetValue("Area", out area))
			{
				areaName = area.ToString();
				key = areaName + " " + key;
			}

			ClaimsIdentity identity = (ClaimsIdentity)@self.HttpContext.User.Identity;

			HttpCachePolicyBase cachePolicy = @self.HttpContext.Response.Cache;

			//AuthorizationContext authorizationContext = cache.Get(key, () => AuthorizationContextFactory(@self, controllerName, actionName, areaName));
			//if (authorizationContext == null) return null;
			//authorizationContext.RequestContext = @self;
			//authorizationContext.HttpContext = @self.HttpContext;
			//authorizationContext.Result = null;

			//return authorizationContext;

			return null;
		}
	}
}