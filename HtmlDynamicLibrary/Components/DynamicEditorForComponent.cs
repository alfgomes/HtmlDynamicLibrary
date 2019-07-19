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
	public static class DynamicEditorForComponent
	{
		public static MvcHtmlString DynamicEditorFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object viewData = null, bool readOnly = false, bool disabled = false, bool visible = true)
		{
			DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TProperty>(helper, expression, viewData, readOnly, disabled, visible);

			switch (expression.Body.Type.FullName)
			{
				case "System.Boolean":
					return DynamicCheckbox(dynamicComponentBase);
				default:
					return DynamicInput(dynamicComponentBase);
			}
		}

		private static MvcHtmlString DynamicInput<TModel, TProperty>(DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase)
		{
			return new TagBuilder_Input<TModel, TProperty>(dynamicComponentBase).GenerateElementMvcString(TagRenderMode.Normal);
		}

		private static MvcHtmlString DynamicCheckbox<TModel, TProperty>(DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase)
		{
			return new TagBuilder_Checkbox<TModel, TProperty>(dynamicComponentBase).GenerateElementMvcString(TagRenderMode.Normal);
		}
	}
}