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
	public static class DynamicDisplayNameForComponent
	{
		public static MvcHtmlString DynamicDisplayNameFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, DynamicDisplayType displayType, object viewData = null, bool blockShowRequiredSymbol = false, string requiredSymbol = "*", string requiredClass = "req editor-field-required")
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

			RouteValueDictionary viewDataObj = HtmlHelper.AnonymousObjectToHtmlAttributes(viewData);
			RouteValueDictionary htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(viewDataObj["htmlAttributes"]);

			//Adicionar os atributos de acordo com o que for obtido no MetaData...

			switch (displayType)
			{
				case DynamicDisplayType.Label:
					htmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(htmlAttributes, viewDataObj, new RouteValueDictionary() { { "class", "control-label" } });
					return TagBuilderGenerators.GenerateTagLabel($"display_{ sanitizedId}", modelMetadata.DisplayName, sanitizedId, htmlAttributes, modelMetadata.Description, !blockShowRequiredSymbol && modelMetadata.IsRequired, requiredSymbol, requiredClass).ToMvcHtmlString(TagRenderMode.Normal);
				case DynamicDisplayType.Span:
					htmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(htmlAttributes, viewDataObj, new RouteValueDictionary() { { "class", "control-span" } });
					return TagBuilderGenerators.GenerateTagDisplay($"display_{ sanitizedId}", modelMetadata.DisplayName, htmlAttributes, modelMetadata.Description, !blockShowRequiredSymbol && modelMetadata.IsRequired, requiredSymbol, requiredClass).ToMvcHtmlString(TagRenderMode.Normal);
				default:
					htmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(htmlAttributes, viewDataObj);
					return TagBuilderGenerators.GenerateOnlyText(modelMetadata.DisplayName, htmlAttributes, !blockShowRequiredSymbol && modelMetadata.IsRequired, requiredSymbol, requiredClass);
			}
		}
	}
}