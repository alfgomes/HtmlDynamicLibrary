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
	public static class DynamicDisplayForComponent
	{
		public static MvcHtmlString DynamicDisplayFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, Rendering displayType = Rendering.Label, object viewData = null, bool blockShowRequiredSymbol = false, string requiredSymbol = null, string requiredMessage = null, string requiredClass = null)
		{
			DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TProperty>(helper, expression, viewData);

			switch (displayType)
			{
				case Rendering.Label:
					dynamicComponentBase.HtmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(dynamicComponentBase.HtmlAttributes, new RouteValueDictionary() { { "class", "control-label" } });
					return TagBuilderGenerators.GenerateTagLabel(dynamicComponentBase.SanitizedId, dynamicComponentBase.FieldValue.ToString(), null, dynamicComponentBase.HtmlAttributes, dynamicComponentBase.FieldModelMetadata.Description, false /*!blockShowRequiredSymbol && dynamicComponentBase.FieldModelMetadata.IsRequired*/, requiredSymbol, requiredMessage, requiredClass).ToMvcHtmlString(TagRenderMode.Normal);
				case Rendering.Span:
					dynamicComponentBase.HtmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(dynamicComponentBase.HtmlAttributes, new RouteValueDictionary() { { "class", "control-span" } });
					return TagBuilderGenerators.GenerateTagDisplay(dynamicComponentBase.SanitizedId, dynamicComponentBase.FieldValue.ToString(), dynamicComponentBase.HtmlAttributes, dynamicComponentBase.FieldModelMetadata.Description, false /*!blockShowRequiredSymbol && dynamicComponentBase.FieldModelMetadata.IsRequired*/, requiredSymbol, requiredMessage, requiredClass).ToMvcHtmlString(TagRenderMode.Normal);
				case Rendering.Edit:
					dynamicComponentBase.HtmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(dynamicComponentBase.HtmlAttributes, new RouteValueDictionary() { { "class", "form-control control-input" } });
					return TagBuilderGenerators.GenerateTagEditor(dynamicComponentBase.SanitizedId, dynamicComponentBase.FieldValue.ToString(), dynamicComponentBase.HtmlAttributes, dynamicComponentBase.FieldModelMetadata.Description, false, true).ToMvcHtmlString(TagRenderMode.Normal);
				case Rendering.Progress:
					dynamicComponentBase.HtmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(dynamicComponentBase.HtmlAttributes, new RouteValueDictionary() { { "class", "form-control control-progress" } });
					double? value = dynamicComponentBase.FieldValue == null ? (double?)null : double.Parse(dynamicComponentBase.FieldValue.ToString());
					double? maxValue = dynamicComponentBase.MetadataAttributes.GetValue<double?>("Progress", "MaxValue");
					return TagBuilderGenerators.GenerateTagProgress(dynamicComponentBase.SanitizedId, dynamicComponentBase.FieldValue.ToString(), dynamicComponentBase.HtmlAttributes, value, maxValue, dynamicComponentBase.FieldModelMetadata.Description, false).ToMvcHtmlString(TagRenderMode.Normal);
				default:
					return TagBuilderGenerators.GenerateOnlyText(dynamicComponentBase.FieldValue.ToString(), dynamicComponentBase.HtmlAttributes, false /*!blockShowRequiredSymbol && dynamicComponentBase.FieldModelMetadata.IsRequired*/, requiredSymbol, requiredMessage, requiredClass);
			}
		}
	}
}