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
	public class TagBuilder_Checkbox<TModel, TProperty> : CustomTagBuilder<TModel, TProperty>
	{
		public TagBuilder_Checkbox(DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase)
			: base(CustomAttributesHelpers.GetInputElementTypeByDataType((DataType)dynamicComponentBase.MetadataAttributes.GetValue<DataType>("DataType", "DataType")), dynamicComponentBase)
		{
			/* Adicionar os atributos de acordo com o que for obtido no MetaData... */
			TagElement.AddInputAttributeStaticValue("type", "checkbox");

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
