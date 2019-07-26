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
	public class TagBuilder_TextArea<TModel, TProperty> : CustomTagBuilder<TModel, TProperty>
	{
		public TagBuilder_TextArea(DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase)
			: base("textarea", dynamicComponentBase)
		{
			/* Adicionar os atributos de acordo com o que for obtido no Metadata... */
			//TagElement.AddInputTypeAttribute(fieldType);
			TagElement.AddInputAttributeIsNotNull("type", CustomAttributesHelpers.ConvertDataTypeToHtmlType((DataType)this.ComponentBase.MetadataAttributes.GetValue<DataType>("DataType", "DataType"), this.ComponentBase.FieldType));
			int? minimumLength = (int?)this.ComponentBase.MetadataAttributes.GetValue<DataType>("Minimum", "Length") ?? (int?)this.ComponentBase.MetadataAttributes.GetValue<DataType>("StringLength", "MinimumLength");
			TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("minlength", minimumLength, minimumLength.HasValue && minimumLength.Value > 0);
			int? maximumLength = (int?)this.ComponentBase.MetadataAttributes.GetValue<DataType>("Maximum", "Length") ?? (int?)this.ComponentBase.MetadataAttributes.GetValue<DataType>("StringLength", "MaximumLength");
			TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("maxlength", maximumLength, maximumLength.HasValue && maximumLength.Value > 0);
			TagElement.MergeInputAttributeIsNotNull("class", this.ComponentBase.MetadataAttributes.GetValue<object>("OnlyNumber", "ClassDecorator"));
			TagElement.MergeInputAttributeIsNotNull("class", this.ComponentBase.MetadataAttributes.GetValue<object>("NoEspecialChars", "ClassDecorator"));
			TagElement.AddInputAttributeIsNotNull("cols", this.ComponentBase.MetadataAttributes.GetValue<object>("TextArea", "Cols"));
			TagElement.AddInputAttributeIsNotNull("rows", this.ComponentBase.MetadataAttributes.GetValue<object>("TextArea", "Rows"));
			TagElement.AddInputAttributeIsNotNull("wrap", this.ComponentBase.MetadataAttributes.GetValue<object>("TextArea", "Wrap"));
			TagElement.AddInputAttributeIsNotNull("minlength", this.ComponentBase.MetadataAttributes.GetValue<object>("TextArea", "MinLength"));
			TagElement.AddInputAttributeIsNotNull("maxlength", this.ComponentBase.MetadataAttributes.GetValue<object>("TextArea", "MaxLength"));

			/* Adicionar os atributos de acordo com o que for obtido no HtmlAttributes... */
			TagElement.MergeInputAttributeHtmlAttributes("class", this.ComponentBase.HtmlAttributes);
			TagElement.MergeInputAttributeHtmlAttributes("style", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("type", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("minlength", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("maxlength", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("cols", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("rows", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("wrap", this.ComponentBase.HtmlAttributes);

			/* Injetando o Valor no Input... */
			this.Value = this.ComponentBase.FieldValue;
			TagElement.AddInputAttributeIsNotNull("value", this.Value);
			TagElement.InnerHtml = this.Value?.ToString();
		}

		public override MvcHtmlString GenerateElementMvcString(TagRenderMode renderMode)
		{
			return TagElement.ToMvcHtmlString(renderMode);
		}
	}
}