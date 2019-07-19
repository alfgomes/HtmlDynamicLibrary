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
	public static class DynamicDisplayNameForComponent
	{
		public static MvcHtmlString DynamicDisplayNameFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, DynamicDisplayType displayType, object viewData = null, bool blockShowRequiredSymbol = false, string requiredSymbol = "*", string requiredClass = "req editor-field-required")
		{
			DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TProperty>(helper, expression, viewData);

			switch (displayType)
			{
				case DynamicDisplayType.Label:
					dynamicComponentBase.HtmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(dynamicComponentBase.HtmlAttributes, new RouteValueDictionary() { { "class", "control-label" } });
					return TagBuilderGenerators.GenerateTagLabel($"display_{ dynamicComponentBase.SanitizedId }", dynamicComponentBase.FieldModelMetadata.DisplayName, dynamicComponentBase.SanitizedId, dynamicComponentBase.HtmlAttributes, dynamicComponentBase.FieldModelMetadata.Description, !blockShowRequiredSymbol && dynamicComponentBase.FieldModelMetadata.IsRequired, requiredSymbol, requiredClass).ToMvcHtmlString(TagRenderMode.Normal);
				case DynamicDisplayType.Span:
					dynamicComponentBase.HtmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(dynamicComponentBase.HtmlAttributes, new RouteValueDictionary() { { "class", "control-span" } });
					return TagBuilderGenerators.GenerateTagDisplay($"display_{ dynamicComponentBase.SanitizedId }", dynamicComponentBase.FieldModelMetadata.DisplayName, dynamicComponentBase.HtmlAttributes, dynamicComponentBase.FieldModelMetadata.Description, !blockShowRequiredSymbol && dynamicComponentBase.FieldModelMetadata.IsRequired, requiredSymbol, requiredClass).ToMvcHtmlString(TagRenderMode.Normal);
				default:
					return TagBuilderGenerators.GenerateOnlyText(dynamicComponentBase.FieldModelMetadata.DisplayName, dynamicComponentBase.HtmlAttributes, !blockShowRequiredSymbol && dynamicComponentBase.FieldModelMetadata.IsRequired, requiredSymbol, requiredClass);
			}
		}
	}
}