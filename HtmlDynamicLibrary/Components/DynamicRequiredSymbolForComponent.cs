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
	public static class RequiredSymbolForComponent
	{
		public static MvcHtmlString DynamicRequiredSymbolForComponent<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object ViewData = null, bool readOnly = false, string symbol = "*", string cssClass = "req editor-field-required")
		{
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

			if (modelMetadata.IsRequired && !readOnly)
			{
				var builder = new TagBuilder("span");
				builder.AddCssClass(cssClass);
				builder.InnerHtml = symbol;

				return new MvcHtmlString(builder.ToString(TagRenderMode.Normal));
			}

			return new MvcHtmlString("");
		}
	}
}