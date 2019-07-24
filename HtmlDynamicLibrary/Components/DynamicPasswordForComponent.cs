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
using HtmlDynamicLibrary.CustomTagBuilders;
using HtmlDynamicLibrary.Helpers;

namespace System.Web.Mvc
{
	public static class DynamicPasswordForComponent
	{
		public static MvcHtmlString DynamicPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object viewData = null, bool readOnly = false, bool disabled = false, bool visible = true)
		{
			DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TProperty>(helper, expression, viewData, readOnly, disabled, visible);

			return new TagBuilder_Password<TModel, TProperty>(dynamicComponentBase).GenerateElementMvcString(TagRenderMode.Normal);
		}
	}
}