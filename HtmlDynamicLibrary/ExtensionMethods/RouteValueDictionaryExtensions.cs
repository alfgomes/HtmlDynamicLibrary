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
		public static RouteValueDictionary Extend(this RouteValueDictionary self, RouteValueDictionary additional)
		{
			additional.ToList().ForEach(x => { self[x.Key] = HtmlHelpers.AppendValues(self[x.Key], x.Value); });

			return self;
		}

		public static RouteValueDictionary Merge(this RouteValueDictionary self, RouteValueDictionary newData)
		{
			return (new RouteValueDictionary(self)).Extend(newData);
		}
	}
}