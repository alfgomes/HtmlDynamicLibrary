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
using HtmlDynamicLibrary.Helpers;

namespace System.Web.Mvc
{
	public static class DynamicRequiredSymbolComponent
	{
		public static MvcHtmlString DynamicRequiredSymbol(this HtmlHelper htmlHelper, object viewData = null, bool required = true, bool readOnly = false, string symbol = "*", string message = "Esse campo é obrigatório!", string cssClass = "req editor-field-required")
		{
			if (required && !readOnly)
			{
				var builder = new TagBuilder("sup");
				builder.AddCssClass(cssClass);
				builder.AddInputAttributeStaticValue("style", "color:red");
				builder.AddInputAttributeStaticValue("title", message);
				builder.InnerHtml = symbol;

				return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
			}

			return new MvcHtmlString("");
		}
	}
}