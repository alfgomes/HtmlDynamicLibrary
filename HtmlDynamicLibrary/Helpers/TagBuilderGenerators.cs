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
	public static class TagBuilderGenerators
	{
		#region TagBuilder Generators...

		public static TagBuilder GenerateTagSpanRequired(string requiredSymbol = "*", string requiredClass = "req editor-field-required")
		{
			TagBuilder tagSpan = null;
			tagSpan = new TagBuilder("span");
			tagSpan.AddCssClass(requiredClass);
			tagSpan.InnerHtml = requiredSymbol;

			return tagSpan;
		}

		public static TagBuilder GenerateTagLabel(string id, string caption, string forInput, RouteValueDictionary htmlAttributes, string tooltip = null, bool isRequired = false, string requiredSymbol = "*", string requiredClass = "req editor-field-required")
		{
			TagBuilder tagSpan = isRequired ? GenerateTagSpanRequired(requiredSymbol, requiredClass) : null;

			TagBuilder tagLabel = new TagBuilder("label");
			tagLabel.AddInputAttributeIsNotNullAndExpressionIsTrue("id", id ?? $"label.{forInput}", (id ?? $"label.{forInput}") != null);
			tagLabel.AddInputAttributeIsNotNullAndExpressionIsTrue("for", forInput, forInput != null);
			tagLabel.AddInputAttributeHtmlAttributes("class", htmlAttributes);
			tagLabel.AddInputAttributeHtmlAttributes("style", htmlAttributes);

			if (tooltip != null && !string.IsNullOrEmpty(tooltip))
			{
				tagLabel.AddInputAttributeStaticValue("data-toggle", "tooltip");
				tagLabel.AddInputAttributeStaticValue("data-placement", "top");
				tagLabel.AddInputAttributeIsNotNullAndExpressionIsTrue("data-original-title", tooltip, tooltip != null);
			}

			tagLabel.InnerHtml = caption + (tagSpan != null ? tagSpan.ToMvcHtmlString(TagRenderMode.Normal).ToString() : "");

			return tagLabel;
		}

		public static TagBuilder GenerateTagDisplay(string id, string caption, RouteValueDictionary htmlAttributes, string tooltip = null, bool isRequired = false, string requiredSymbol = "*", string requiredClass = "req editor-field-required")
		{
			TagBuilder tagSpan = isRequired ? GenerateTagSpanRequired(requiredSymbol, requiredClass) : null;

			TagBuilder tagLabel = new TagBuilder("span");
			tagLabel.AddInputAttributeIsNotNullAndExpressionIsTrue("id", id, id != null);
			tagLabel.AddInputAttributeHtmlAttributes("class", htmlAttributes);
			tagLabel.AddInputAttributeHtmlAttributes("style", htmlAttributes);

			if (tooltip != null && !string.IsNullOrEmpty(tooltip))
			{
				tagLabel.AddInputAttributeStaticValue("data-toggle", "tooltip");
				tagLabel.AddInputAttributeStaticValue("data-placement", "top");
				tagLabel.AddInputAttributeIsNotNullAndExpressionIsTrue("data-original-title", tooltip, tooltip != null);
			}

			tagLabel.InnerHtml = caption + (tagSpan != null ? tagSpan.ToMvcHtmlString(TagRenderMode.Normal).ToString() : "");

			return tagLabel;
		}

		public static MvcHtmlString GenerateOnlyText(string caption, RouteValueDictionary htmlAttributes, bool isRequired = false, string requiredSymbol = "*", string requiredClass = "req editor-field-required")
		{
			TagBuilder tagSpan = isRequired ? GenerateTagSpanRequired(requiredSymbol, requiredClass) : null;

			string tagText = caption + (tagSpan != null ? tagSpan.ToMvcHtmlString(TagRenderMode.Normal).ToString() : "");

			return new MvcHtmlString(tagText);
		}

		#endregion
	}
}