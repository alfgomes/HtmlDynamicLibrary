using HtmlDynamicLibrary.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace HtmlDynamicLibrary.ExtensionMethods
{
	public static class RouteValueDictionaryExtensions
	{
		private static string SplitValues(object obj)
		{
			if (obj == null) return null;

			string ret = "";

			if (obj.GetType().IsArray)
				foreach (var item in obj as Array)
					ret += $" {item}";
			else
				ret = obj.ToString();

			return ret.Trim();
		}

		public static RouteValueDictionary Extend(this RouteValueDictionary self, RouteValueDictionary additional, bool replaceExisting = false)
		{
			//additional.ToList().ForEach(x => { @self[x.Key] = HtmlHelpers.AppendValues(@self[x.Key], (replaceExisting ? x.Value : SplitValues(@self[x.Key]) + $" {x.Value}")); });
			additional.ToList().ForEach(x => { self[x.Key] = HtmlHelpers.AppendValues(self[x.Key], x.Value); });

			return self;
		}

		public static RouteValueDictionary Merge(this RouteValueDictionary self, RouteValueDictionary additional, bool replaceExisting = false)
		{
			return (new RouteValueDictionary(self)).Extend(additional, replaceExisting);
		}

		public static bool Match(this RouteValueDictionary @self, string key, object matchValue)
		{
			if (!@self.ContainsKey(key)) return false;

			object potentialCultureName = (string)@self[key];

			return potentialCultureName == matchValue;
		}

		public static IDictionary<string, object> ToDictionary(this RouteValueDictionary @self)
		{
			return (IDictionary<string, object>)@self;
		}
	}
}