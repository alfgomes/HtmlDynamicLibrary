using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using HtmlDynamicLibrary.Helpers;

namespace HtmlDynamicLibrary.ExtensionMethods
{
	public static class ObjectExtensions
	{
		public static TAttribute GetAttribute<TAttribute>(this object @self) where TAttribute : Attribute
		{
			var objType = @self.GetType();
			var name = @self.ToString();
			return objType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
		}

		public static bool IsNull(this object @self)
		{
			return @self == null;
		}
	}
}