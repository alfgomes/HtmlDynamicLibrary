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

		public static TagBuilder GenerateTagSpanRequired(string requiredSymbol, string requiredMessage, string requiredClass)
		{
			if (requiredSymbol == null)
				requiredSymbol = "*";
			if (requiredMessage == null)
				requiredMessage = "Esse campo é obrigatório!";
			if (requiredClass == null)
				requiredClass = "req editor-field-required";

			TagBuilder tagSpan = null;
			tagSpan = new TagBuilder("sup");
			tagSpan.AddCssClass(requiredClass);
			tagSpan.AddInputAttributeStaticValue("style", "color:red");
			tagSpan.AddInputAttributeStaticValue("title", requiredMessage);
			tagSpan.InnerHtml = requiredSymbol;

			return tagSpan;
		}

		public static TagBuilder GenerateTagLabel(string id, string caption, string forInput, RouteValueDictionary htmlAttributes, string tooltip = null, bool isRequired = false, string requiredSymbol = null, string requiredMessage = null, string requiredClass = null)
		{
			TagBuilder tagSpan = isRequired ? GenerateTagSpanRequired(requiredSymbol, requiredMessage, requiredClass) : null;

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

		public static TagBuilder GenerateTagDisplay(string id, string caption, RouteValueDictionary htmlAttributes, string tooltip = null, bool isRequired = false, string requiredSymbol = null, string requiredMessage = null, string requiredClass = null)
		{
			TagBuilder tagSpan = isRequired ? GenerateTagSpanRequired(requiredSymbol, requiredMessage, requiredClass) : null;

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

		public static MvcHtmlString GenerateOnlyText(string caption, RouteValueDictionary htmlAttributes, bool isRequired = false, string requiredSymbol = null, string requiredMessage = null, string requiredClass = null)
		{
			TagBuilder tagSpan = isRequired ? GenerateTagSpanRequired(requiredSymbol, requiredMessage, requiredClass) : null;

			string tagText = caption + (tagSpan != null ? tagSpan.ToMvcHtmlString(TagRenderMode.Normal).ToString() : "");

			return new MvcHtmlString(tagText);
		}

		public static TagBuilder GenerateTagEditor(string id, string text, RouteValueDictionary htmlAttributes, string tooltip = null, bool isRequired = false, bool disabled = false)
		{
			TagBuilder tagEdit = new TagBuilder("input");
			tagEdit.AddInputAttributeStaticValue("type", "text");
			tagEdit.AddInputAttributeIsNotNullAndExpressionIsTrue("id", id, id != null);
			tagEdit.AddInputAttributeHtmlAttributes("class", htmlAttributes);
			tagEdit.AddInputAttributeHtmlAttributes("style", htmlAttributes);
			tagEdit.AddInputAttributeIfExpressionIsTrue("required", null, isRequired);
			tagEdit.AddInputAttributeIfExpressionIsTrue("disabled", null, disabled);

			if (tooltip != null && !string.IsNullOrEmpty(tooltip))
			{
				tagEdit.AddInputAttributeStaticValue("data-toggle", "tooltip");
				tagEdit.AddInputAttributeStaticValue("data-placement", "top");
				tagEdit.AddInputAttributeIsNotNullAndExpressionIsTrue("data-original-title", tooltip, tooltip != null);
			}

			tagEdit.AddInputAttributeStaticValue("value", text);

			return tagEdit;
		}

		public static TagBuilder GenerateTagSelect(string id, string text, IEnumerable<SelectListItem> selectList, string optionLabel, RouteValueDictionary htmlAttributes, string tooltip = null, bool isRequired = false, bool disabled = false)
		{
			TagBuilder tagEdit = new TagBuilder("input");

			return tagEdit;
		}

		public static TagBuilder GenerateTagProgress(string id, string caption, RouteValueDictionary htmlAttributes, double? value = null, double? maxValue = null, string tooltip = null, bool isRequired = false, bool disabled = false)
		{
			TagBuilder tagEdit = new TagBuilder("progress");
			tagEdit.AddInputAttributeIsNotNullAndExpressionIsTrue("id", id, id != null);
			tagEdit.AddInputAttributeHtmlAttributes("class", htmlAttributes);
			tagEdit.AddInputAttributeHtmlAttributes("style", htmlAttributes);
			tagEdit.AddInputAttributeIfExpressionIsTrue("required", null, isRequired);
			tagEdit.AddInputAttributeIfExpressionIsTrue("disabled", null, disabled);

			if (tooltip != null && !string.IsNullOrEmpty(tooltip))
			{
				tagEdit.AddInputAttributeStaticValue("data-toggle", "tooltip");
				tagEdit.AddInputAttributeStaticValue("data-placement", "top");
				tagEdit.AddInputAttributeIsNotNullAndExpressionIsTrue("data-original-title", tooltip, tooltip != null);
			}

			/* Injetando o Valor no Input... */
			tagEdit.AddInputAttributeIsNotNullAndExpressionIsTrue("max", maxValue, maxValue != null);
			tagEdit.AddInputAttributeIsNotNullAndExpressionIsTrue("value", value, value != null);

			return tagEdit;
			//return new TagBuilder_Progress<TModel, TProperty>(dynamicComponentBase).GenerateElementMvcString(TagRenderMode.Normal);
		}

		#endregion
	}
}