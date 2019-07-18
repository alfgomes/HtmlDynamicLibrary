﻿using System;
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

namespace System.Web.Mvc
{
	public static class DynamicPasswordForComponent
	{
		public static MvcHtmlString DynamicPasswordFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object ViewData = null, bool readOnly = false, bool disabled = false, bool visible = true)
		{
			var typedExpression = (Expression<Func<TModel, TProperty>>)(object)expression;

			MemberInfo field = (expression.Body as MemberExpression).Member;
			var fieldName = ExpressionHelper.GetExpressionText(expression);
			Type fieldType = ((FieldInfo[])((TypeInfo)expression.Body.Type).DeclaredFields)[1].FieldType;
			bool fieldIsNullable = HtmlHelpers.IsNullable(field);
			TProperty fieldValue = expression.Compile().Invoke(helper.ViewData.Model);
			string fieldFullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);
			string sanitizedId = TagBuilder.CreateSanitizedId(fieldFullName);
			ModelMetadata modelMetadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

			RouteValueDictionary viewData = HtmlHelper.AnonymousObjectToHtmlAttributes(ViewData);
			RouteValueDictionary htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(viewData["htmlAttributes"]);
			htmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(htmlAttributes, viewData);
			var defaultHtmlAttributesObject = new { @class = "control-label" };
			htmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(htmlAttributes, defaultHtmlAttributesObject);

			//Obter Atributos Adicionados ao Metadata...
			MetadataAttributes metadataAttributes = new MetadataAttributes(modelMetadata);

			TagBuilder tagInput = new TagBuilder("input");
			//tagInput.GenerateId(fieldFullName);
			tagInput.AddInputAttributeIsNotNull("id", sanitizedId);
			tagInput.AddInputAttributeIsNotNull("name", fieldFullName);
			/*Adicionar os atributos de acordo com o que for obtido no MetaData...*/
			//tagInput.AddInputTypeAttribute(fieldType);
			tagInput.AddInputAttributeIsNotNull("type", "password");
			tagInput.AddInputAttributeIsNotNull("autofocus", metadataAttributes.GetValue<object>("Base", "Autofocus"));
			tagInput.AddInputAttributeIsNotNull("required", metadataAttributes.GetValue<object>("Base", "IsRequired"));
			tagInput.AddInputAttributeIsNotNull("readonly", readOnly || (bool)metadataAttributes.GetValue<object>("Base", "IsReadOnly"));
			int? minimumLength = (int?)metadataAttributes.GetValue<DataType>("Minimum", "Length") ?? (int?)metadataAttributes.GetValue<DataType>("StringLength", "MinimumLength");
			tagInput.AddInputAttributeIsNotNullAndExpressionIsTrue("minlength", minimumLength, minimumLength.HasValue && minimumLength.Value > 0);
			int? maximumLength = (int?)metadataAttributes.GetValue<DataType>("Maximum", "Length") ?? (int?)metadataAttributes.GetValue<DataType>("StringLength", "MaximumLength");
			tagInput.AddInputAttributeIsNotNullAndExpressionIsTrue("maxlength", maximumLength, maximumLength.HasValue && maximumLength.Value > 0);
			tagInput.AddInputAttributeIsNotNull("title", metadataAttributes.GetValue<object>("Base", "Description"));
			tagInput.AddInputAttributeIsNotNull("placeholder", metadataAttributes.GetValue<object>("Base", "Watermark"));
			tagInput.AddInputAttributeIsNotNull("class", metadataAttributes.GetValue<object>("OnlyNumber", "ClassDecorator"));
			tagInput.AddInputAttributeIsNotNull("class", metadataAttributes.GetValue<object>("NoEspecialChars", "ClassDecorator"));
			/*Adicionar os atributos de acordo com o que for obtido no HtmlAttributes...*/
			tagInput.MergeInputAttributeHtmlAttributes("class", htmlAttributes);
			tagInput.MergeInputAttributeHtmlAttributes("style", htmlAttributes);
			tagInput.AddInputAttributeHtmlAttributes("autofocus", htmlAttributes);
			tagInput.AddInputAttributeHtmlAttributes("required", htmlAttributes);
			tagInput.AddInputAttributeHtmlAttributes("readonly", htmlAttributes);
			tagInput.AddInputAttributeHtmlAttributes("disabled", htmlAttributes);
			tagInput.AddInputAttributeHtmlAttributes("minlength", htmlAttributes);
			tagInput.AddInputAttributeHtmlAttributes("maxlength", htmlAttributes);
			tagInput.AddInputAttributeHtmlAttributes("title", htmlAttributes);
			tagInput.AddInputAttributeHtmlAttributes("placeholder", htmlAttributes);
			/*Injetando o Valor no Input...*/
			tagInput.AddInputAttributeIsNotNull("value", fieldValue);
			///*Adicionar os atributos de acordo com o que for obtido no Data-Val...*/
			//tagInput.AddInputAttributeStaticValue("data-val", "true");
			//tagInput.AddInputAttributeStaticValue("data-val-required", "");
			//tagInput.AddInputAttributeStaticValue("data-val-regex-pattern", "");
			//tagInput.AddInputAttributeStaticValue("data-val-regex", "");

			return tagInput.ToMvcHtmlString(TagRenderMode.Normal);
		}
	}
}