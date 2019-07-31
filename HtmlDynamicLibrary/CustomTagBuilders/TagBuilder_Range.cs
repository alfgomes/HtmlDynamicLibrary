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
	public class TagBuilder_Range<TModel, TProperty> : CustomTagBuilder<TModel, TProperty>
	{
		public TagBuilder_Range(DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase)
			: base("input", dynamicComponentBase)
		{
			/* Adicionar os atributos de acordo com o que for obtido no Metadata... */
			//TagElement.AddInputTypeAttribute(fieldType);
			TagElement.AddInputAttributeIsNotNull("type", "range");
			TagElement.AddInputAttributeIsNotNull("min", this.ComponentBase.MetadataAttributes.GetValue<double>("Progress", "MinValue"));
			TagElement.AddInputAttributeIsNotNull("max", this.ComponentBase.MetadataAttributes.GetValue<double>("Progress", "MaxValue"));
			TagElement.AddInputAttributeIsNotNull("step", this.ComponentBase.MetadataAttributes.GetValue<double>("Progress", "Step"));

			/* Adicionar os atributos de acordo com o que for obtido no HtmlAttributes... */
			TagElement.MergeInputAttributeHtmlAttributes("class", this.ComponentBase.HtmlAttributes);
			TagElement.MergeInputAttributeHtmlAttributes("style", this.ComponentBase.HtmlAttributes);

			/* Injetando o Valor no Input... */
			this.Value = this.ComponentBase.FieldValue;
			TagElement.AddInputAttributeIsNotNull("value", this.Value);
		}

		public override MvcHtmlString GenerateElementMvcString(TagRenderMode renderMode)
		{
			return TagElement.ToMvcHtmlString(renderMode);
		}
	}
}