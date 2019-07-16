using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace HtmlDynamicLibrary.Helpers
{
	public static class MvcHtmlStringHelpers
	{
		#region Extension Methods By MvcHtmlString...

		public static MvcHtmlString ToMvcHtmlStringSanitized(this TagBuilder tagBuilder, TagRenderMode renderMode)
		{
			MvcHtmlString mvcHtmlString = new MvcHtmlString(tagBuilder.ToString(renderMode));

			string ret = mvcHtmlString.ToHtmlString();
			ret = ret.Replace("autofocus=\"True\"", "autofocus");
			ret = ret.Replace("required=\"True\"", "required");
			ret = ret.Replace("disabled=\"True\"", "disabled");
			ret = ret.Replace("readonly=\"True\"", "readonly");

			return new MvcHtmlString(ret);
		}

		public static void AppendElement(this TagBuilder tagBuilder, TagBuilder tagInner)
		{

		}

		#endregion
	}
}