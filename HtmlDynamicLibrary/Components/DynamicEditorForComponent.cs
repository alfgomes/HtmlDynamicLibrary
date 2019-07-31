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

			switch ((DataType)dynamicComponentBase.MetadataAttributes.GetValue<DataType>("DataType", "DataType"))
			{
				case DataType.MultilineText:
					return DynamicTextArea(dynamicComponentBase);
				case DataType.Custom:
				case DataType.DateTime:
				case DataType.Date:
				case DataType.Time:
				case DataType.Duration:
				case DataType.PhoneNumber:
				case DataType.Currency:
				case DataType.Text:
				case DataType.Html:
				case DataType.EmailAddress:
				case DataType.Password:
				case DataType.Url:
				case DataType.ImageUrl:
				case DataType.CreditCard:
				case DataType.PostalCode:
				case DataType.Upload:
				default:
					if (expression.Body.Type.FullName == "System.Boolean")
						return DynamicCheckbox(dynamicComponentBase);
					else if (dynamicComponentBase.MetadataAttributes.HasAttribute<bool>("ProgressAttribute"))
					{
						helper.AddScript("slider", "$(function() { $('.slider').on('input change', function() { $(this).next($('.slider_label')).html(this.value); }); $('.slider_label').each(function() { var value = $(this).prev().attr('value'); $(this).html(value); }); })");
						return DynamicRange(dynamicComponentBase, true);
					}

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

		private static MvcHtmlString DynamicTextArea<TModel, TProperty>(DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase)
		{
			return new TagBuilder_TextArea<TModel, TProperty>(dynamicComponentBase).GenerateElementMvcString(TagRenderMode.Normal);
		}

		private static MvcHtmlString DynamicRange<TModel, TProperty>(DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase, bool showValue = false)
		{
			TagBuilder_Range<TModel, TProperty> range = new TagBuilder_Range<TModel, TProperty>(dynamicComponentBase);

			if (showValue)
			{
				TagBuilder_Label<TModel, TProperty> label = new TagBuilder_Label<TModel, TProperty>(dynamicComponentBase, null, dynamicComponentBase.FieldValue.ToString(), dynamicComponentBase.SanitizedId);
				label.TagElement.RemoveAttribute("class");
				label.TagElement.AddCssClass("slider_label");
				range.TagElement.InnerHtml = label.TagElement.ToMvcHtmlStringSanitized(TagRenderMode.Normal).ToString();
			}

			return range.GenerateElementMvcString(TagRenderMode.Normal);
		}
	}
}