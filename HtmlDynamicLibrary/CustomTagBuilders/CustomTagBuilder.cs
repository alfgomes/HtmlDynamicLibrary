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
	public abstract class CustomTagBuilder<TModel, TProperty>
	{
		public string TagElementType { get; protected set; }
		public TagBuilder TagElement { get; protected set; }
		public object Value { get; protected set; }
		protected DynamicComponentBaseFor<TModel, TProperty> ComponentBase { get; set; }

		public CustomTagBuilder(string tagElementType, DynamicComponentBaseFor<TModel, TProperty> componentBase)
		{
			this.TagElementType = tagElementType;
			this.ComponentBase = componentBase;

			InicializeTagElement();
		}

		private void InicializeTagElement()
		{
			this.TagElement = new TagBuilder(TagElementType);
			//TagElement.GenerateId(fieldFullName);
			TagElement.AddInputAttributeIsNotNull("id", this.ComponentBase.SanitizedId);
			TagElement.AddInputAttributeIsNotNull("name", this.ComponentBase.FieldFullName);

			//...............................................................................................................................................................\\
			bool _IsRequired = !this.ComponentBase.FieldIsNullable || this.ComponentBase.MetadataAttributes.GetValue<bool>("Common", "IsRequired");
			if (_IsRequired)
				TagElement.AddInputAttributeStaticValue("required", null);
			bool _IsNullable = this.ComponentBase.FieldIsNullable || this.ComponentBase.MetadataAttributes.GetValue<bool>("Common", "IsNullable") || this.ComponentBase.MetadataAttributes.GetValue<bool>("Common", "IsNullableValueType");
			//...............................................................................................................................................................\\


			/* Adicionar os atributos de acordo com o que for obtido no MetaData... */
			//TagElement.AddInputTypeAttribute(fieldType);
			TagElement.AddInputAttributeStaticValue("class", "form-control");
			TagElement.AddInputAttributeIsNotNull("autofocus", this.ComponentBase.MetadataAttributes.GetValue<object>("Common", "Autofocus"));
			TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("required", null, (bool)this.ComponentBase.MetadataAttributes.GetValue<object>("Common", "IsRequired"));
			TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("readonly", null, this.ComponentBase.FieldIsReadOnly || (bool)this.ComponentBase.MetadataAttributes.GetValue<object>("Common", "IsReadOnly"));
			TagElement.AddInputAttributeIsNotNull("title", this.ComponentBase.MetadataAttributes.GetValue<object>("Common", "Description"));
			TagElement.AddInputAttributeIsNotNull("placeholder", this.ComponentBase.MetadataAttributes.GetValue<object>("Common", "Watermark"));

			//...............................................................................................................................................................\\

			TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("required", null, !this.ComponentBase.FieldIsNullable);
			TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("readonly", null, this.ComponentBase.FieldIsReadOnly);
			TagElement.AddInputAttributeIsNotNullAndExpressionIsTrue("disabled", null, this.ComponentBase.FieldIsDisabled);

			//...............................................................................................................................................................\\

			/* Adicionar os atributos de acordo com o que for obtido no HtmlAttributes... */
			TagElement.AddInputAttributeHtmlAttributes("autofocus", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("required", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("readonly", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("disabled", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("title", this.ComponentBase.HtmlAttributes);
			TagElement.AddInputAttributeHtmlAttributes("placeholder", this.ComponentBase.HtmlAttributes);

			//...............................................................................................................................................................\\

			/* Adicionar os atributos de acordo com o que for obtido no Data-Val... */
			//TagElement.AddInputAttributeStaticValue("data-val", "true");
			TagElement.AddInputAttributeIsNotNull("data-val-required", this.ComponentBase.MetadataAttributes.GetValue<object>("Required", "ErrorMessage"));
			//TagElement.AddInputAttributeStaticValue("data-val-regex-pattern", "");
			//TagElement.AddInputAttributeStaticValue("data-val-regex", "");

			this.Value = this.ComponentBase.FieldValue;
		}

		public abstract MvcHtmlString GenerateElementMvcString(TagRenderMode renderMode);
	}
}