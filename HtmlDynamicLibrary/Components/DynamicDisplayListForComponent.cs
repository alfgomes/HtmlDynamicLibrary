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
	public static class DynamicDisplayListForComponent
	{
		public static MvcHtmlString DynamicDisplayListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, DisplayListType displayType, object viewData = null)
		{
			DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TProperty>(helper, expression, viewData, true, true);
			object selectedValue = dynamicComponentBase.FieldValue;
			string selectedText = selectList.Where(w => w.Value == selectedValue?.ToString()).FirstOrDefault().Text;

			switch (displayType)
			{
				case DisplayListType.DropDownList:
					TagBuilder_Select<TModel, TProperty> tagBuilder = new TagBuilder_Select<TModel, TProperty>(dynamicComponentBase);
					tagBuilder.AddOptionLabel(optionLabel);
					tagBuilder.AddOptions(selectList);
					tagBuilder.TagElement.AddInputAttributeStaticValue("disabled", null);
					tagBuilder.TagElement.AddInputAttributeStaticValue("class", "");
					return tagBuilder.GenerateElementMvcString(TagRenderMode.Normal);
				case DisplayListType.SelectedEdit:
					dynamicComponentBase.HtmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(dynamicComponentBase.HtmlAttributes, new RouteValueDictionary() { { "class", "control-input" } });
					return TagBuilderGenerators.GenerateTagEditor(dynamicComponentBase.SanitizedId, selectedText, dynamicComponentBase.HtmlAttributes, dynamicComponentBase.FieldModelMetadata.Description, false, true).ToMvcHtmlString(TagRenderMode.Normal);
				case DisplayListType.OptionsList:
					return null;
				case DisplayListType.SelectedLabel:
					dynamicComponentBase.HtmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(dynamicComponentBase.HtmlAttributes, new RouteValueDictionary() { { "class", "control-label" } });
					return TagBuilderGenerators.GenerateTagLabel(dynamicComponentBase.SanitizedId, selectedText, dynamicComponentBase.SanitizedId, dynamicComponentBase.HtmlAttributes, dynamicComponentBase.FieldModelMetadata.Description).ToMvcHtmlString(TagRenderMode.Normal);
				case DisplayListType.SelectedSpan:
					dynamicComponentBase.HtmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(dynamicComponentBase.HtmlAttributes, new RouteValueDictionary() { { "class", "control-span" } });
					return TagBuilderGenerators.GenerateTagDisplay(dynamicComponentBase.SanitizedId, selectedText, dynamicComponentBase.HtmlAttributes, dynamicComponentBase.FieldModelMetadata.Description).ToMvcHtmlString(TagRenderMode.Normal);
				case DisplayListType.SelectedOnlyText:
				default:
					return TagBuilderGenerators.GenerateOnlyText(selectedText, dynamicComponentBase.HtmlAttributes);
			}
		}
	}
}