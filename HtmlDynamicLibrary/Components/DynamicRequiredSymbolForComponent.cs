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
		public static MvcHtmlString DynamicRequiredSymbolForComponent<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object viewData = null, bool readOnly = false, string symbol = "*", string message = "Esse campo é obrigatório!", string cssClass = "req editor-field-required")
		{
			DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TProperty>(helper, expression, viewData, readOnly);

			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

			return helper.DynamicRequiredSymbol(viewData, modelMetadata.IsRequired, readOnly);
		}
	}
}