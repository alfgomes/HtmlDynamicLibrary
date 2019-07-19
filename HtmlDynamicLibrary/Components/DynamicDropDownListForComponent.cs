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

namespace System.Web.Mvc
{
	public static class DynamicDropDownListForComponent
	{
		public static MvcHtmlString DynamicDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object viewData = null, bool readOnly = false, bool disabled = false, bool visible = true)
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

			////Testes para obter os Metadata Attributes do campo...
			//var list = helper.ViewData.ModelMetadata.Properties.ToList();
			//var required = field.GetCustomAttributes(typeof(RequiredAttribute), false).Cast<RequiredAttribute>().FirstOrDefault();
			//var t = fieldType.GetCustomAttributes(false).OfType<MetadataTypeAttribute>().FirstOrDefault();

			RouteValueDictionary viewDataObj = HtmlHelper.AnonymousObjectToHtmlAttributes(viewData);
			RouteValueDictionary htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(viewDataObj["htmlAttributes"]);
			htmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(htmlAttributes, viewDataObj, new RouteValueDictionary() { { "class", "control-select" } });

			//Obter Atributos Adicionados ao Metadata...
			MetadataAttributes metadataAttributes = new MetadataAttributes(modelMetadata);

			TagBuilder tagSelect = new TagBuilder("select");
			//tagInput.GenerateId(fieldFullName);
			tagSelect.AddInputAttributeIsNotNull("id", sanitizedId);
			tagSelect.AddInputAttributeIsNotNull("name", fieldFullName);
			/*Adicionar os atributos de acordo com o que for obtido no MetaData...*/
			//tagInput.AddInputTypeAttribute(fieldType);
			tagSelect.AddInputAttributeIsNotNull("autofocus", metadataAttributes.GetValue<object>("Base", "Autofocus"));
			tagSelect.AddInputAttributeIsNotNull("required", metadataAttributes.GetValue<object>("Base", "IsRequired"));
			tagSelect.AddInputAttributeIsNotNull("readonly", readOnly || (bool)metadataAttributes.GetValue<object>("Base", "IsReadOnly"));
			tagSelect.AddInputAttributeIsNotNull("title", metadataAttributes.GetValue<object>("Base", "Description"));
			tagSelect.AddInputAttributeIsNotNull("placeholder", metadataAttributes.GetValue<object>("Base", "Watermark"));
			/*Adicionar os atributos de acordo com o que for obtido no HtmlAttributes...*/
			tagSelect.MergeInputAttributeHtmlAttributes("class", htmlAttributes);
			tagSelect.MergeInputAttributeHtmlAttributes("style", htmlAttributes);
			tagSelect.AddInputAttributeHtmlAttributes("autofocus", htmlAttributes);
			tagSelect.AddInputAttributeHtmlAttributes("required", htmlAttributes);
			tagSelect.AddInputAttributeHtmlAttributes("readonly", htmlAttributes);
			tagSelect.AddInputAttributeHtmlAttributes("disabled", htmlAttributes);
			tagSelect.AddInputAttributeHtmlAttributes("title", htmlAttributes);
			tagSelect.AddInputAttributeHtmlAttributes("placeholder", htmlAttributes);
			/*Criando os options...*/
			var options = "";
			TagBuilder option;
			foreach (var item in selectList)
			{
				option = new TagBuilder("option");
				if (item.Value.ToString().Trim() == fieldValue.ToString().Trim())
					option.MergeAttribute("selected", "true");
				option.MergeAttribute("value", item.Value.ToString());
				option.MergeAttribute("data-value", item.Value.ToString());
				option.SetInnerText(item.Text);
				options += option.ToString(TagRenderMode.Normal) + "\n";
			}
			tagSelect.InnerHtml = options;
			/*Injetando o Valor no Input...*/
			tagSelect.AddInputAttributeIsNotNull("value", fieldValue);
			///*Adicionar os atributos de acordo com o que for obtido no Data-Val...*/
			//tagInput.AddInputAttributeStaticValue("data-val", "true");
			//tagInput.AddInputAttributeStaticValue("data-val-required", "");
			//tagInput.AddInputAttributeStaticValue("data-val-regex-pattern", "");
			//tagInput.AddInputAttributeStaticValue("data-val-regex", "");

			return tagSelect.ToMvcHtmlString(TagRenderMode.Normal);
		}

		//public static MvcHtmlString DynamicDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel)
		//{
		//	return null;
		//}

		//public static MvcHtmlString DynamicDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
		//{
		//	return null;
		//}

		//public static MvcHtmlString DynamicDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
		//{
		//	return null;
		//}

		//public static MvcHtmlString DynamicDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		//{
		//	return null;
		//}

		//public static MvcHtmlString DynamicDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList)
		//{
		//	return null;
		//}

		//public static MvcHtmlString DynamicDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		//{
		//	return null;
		//}
	}
}