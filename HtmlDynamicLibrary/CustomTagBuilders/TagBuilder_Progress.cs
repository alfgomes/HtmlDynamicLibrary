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
	public class TagBuilder_Progress<TModel, TProperty> : CustomTagBuilder<TModel, TProperty>
	{
		public TagBuilder_Progress(DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase)
			: base("progress", dynamicComponentBase)
		{
			/* Adicionar os atributos de acordo com o que for obtido no Metadata... */
			//TagElement.AddInputTypeAttribute(fieldType);
			int? minimumLength = (int?)this.ComponentBase.MetadataAttributes.GetValue<DataType>("Minimum", "Length") ?? (int?)this.ComponentBase.MetadataAttributes.GetValue<DataType>("StringLength", "MinimumLength");
			TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("minlength", minimumLength, minimumLength.HasValue && minimumLength.Value > 0);
			int? maximumLength = (int?)this.ComponentBase.MetadataAttributes.GetValue<DataType>("Maximum", "Length") ?? (int?)this.ComponentBase.MetadataAttributes.GetValue<DataType>("StringLength", "MaximumLength");
			TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("maxlength", maximumLength, maximumLength.HasValue && maximumLength.Value > 0);

			/* Adicionar os atributos de acordo com o que for obtido no HtmlAttributes... */
			TagElement.MergeInputAttributeHtmlAttributes("class", this.ComponentBase.HtmlAttributes);
			TagElement.MergeInputAttributeHtmlAttributes("style", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("minlength", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("maxlength", this.ComponentBase.HtmlAttributes);

			/* Injetando o Valor no Input... */
			TagElement.AddInputAttributeIsNotNull("max", this.ComponentBase.MetadataAttributes.GetValue<double>("ProgressAttribute", "MaxValue"));
			this.Value = this.ComponentBase.FieldValue;
			TagElement.AddInputAttributeStaticValue("value", this.Value);
			TagElement.AddInputAttributeStaticValue("title", $"{this.Value}%");
		}

		public override MvcHtmlString GenerateElementMvcString(TagRenderMode renderMode)
		{
			return TagElement.ToMvcHtmlString(renderMode);
		}
	}
}