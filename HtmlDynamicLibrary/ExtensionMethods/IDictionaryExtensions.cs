using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Routing;
using HtmlDynamicLibrary.Helpers;

namespace HtmlDynamicLibrary.ExtensionMethods
{
	public static class IDictionaryExtensions
	{
		public static RouteValueDictionary ToRouteValueDictionary<TValue>(this IDictionary<string, TValue> @self)
		{
			RouteValueDictionary ret = new RouteValueDictionary(@self);

			return ret;
		}

		public static RouteValueDictionary ToRouteValueDictionary(this IDictionary<string, object> @self)
		{
			object val;
			RouteValueDictionary ret = new RouteValueDictionary();

			foreach (var key in @self.Keys)
			{
				@self.TryGetValue(key, out val);
				ret.Add(key, (val is string[]) ? HtmlHelpers.ConvertStringArrayToString((string[])val) : val);
			}

			return ret;
		}

		public static RouteValueDictionary ToRouteValueDictionary(this IDictionary<string, string> @self)
		{
			string val;
			RouteValueDictionary ret = new RouteValueDictionary();

			foreach (var key in @self.Keys)
			{
				@self.TryGetValue(key, out val);
				ret.Add(key, val);
			}

			return ret;
		}
	}
}