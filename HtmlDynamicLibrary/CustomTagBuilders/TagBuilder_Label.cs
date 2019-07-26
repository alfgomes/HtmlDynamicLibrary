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

namespace HtmlDynamicLibrary.CustomTagBuilders
{
	public class TagBuilder_Label<TModel, TProperty> : CustomTagBuilder<TModel, TProperty>
	{
		public TagBuilder_Label(DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase, string id, string caption, string forInput, string tooltip = null)
			: base("label", dynamicComponentBase)
		{
			TagBuilder tagSpan = /*this.ComponentBase.FieldIsReadOnly ? GenerateTagSpanRequired(requiredSymbol, requiredClass) :*/ null;

			/* Adicionar os atributos de acordo com os parâmetros informados... */
			TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("id", id ?? $"label.{forInput}", (id ?? $"label.{forInput}") != null);
			TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("for", forInput, forInput != null);
			if (tooltip != null && !string.IsNullOrEmpty(tooltip))
			{
				TagElement.AddInputAttributeStaticValue("data-toggle", "tooltip");
				TagElement.AddInputAttributeStaticValue("data-placement", "top");
				TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("data-original-title", tooltip, tooltip != null);
			}

			/* Adicionar os atributos de acordo com o que for obtido no HtmlAttributes... */
			TagElement.AddInputAttributeHtmlAttributes("class", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("style", this.ComponentBase.HtmlAttributes);

			/* Injetando o Valor no Input... */
			TagElement.InnerHtml = caption;
			if (tagSpan != null)
				TagElement.InnerHtml += tagSpan.ToMvcHtmlString(TagRenderMode.Normal).ToString();
		}

		public override MvcHtmlString GenerateElementMvcString(TagRenderMode renderMode)
		{
			throw new NotImplementedException();
		}
	}
}