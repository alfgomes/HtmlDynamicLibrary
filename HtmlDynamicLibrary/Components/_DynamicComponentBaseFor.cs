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
	public class DynamicComponentBaseFor<TModel, TProperty>
	{
		#region Properties...

		public Expression<Func<TModel, TProperty>> TypedExpression { get; private set; }
		public MemberInfo Field { get; private set; }
		public string FieldName { get; private set; }
		public Type FieldType { get; private set; }
		public bool FieldIsNullable { get; private set; }
		public TProperty FieldValue { get; private set; }
		public string FieldFullName { get; private set; }
		public string SanitizedId { get; private set; }
		public ModelMetadata FieldModelMetadata { get; private set; }
		public MetadataAttributes MetadataAttributes { get; private set; }
		public RouteValueDictionary HtmlAttributes { get; set; }

		#endregion

		public DynamicComponentBaseFor(HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, object viewData = null, bool readOnly = false, bool disabled = false, bool visible = true)
		{
			TypedExpression = (Expression<Func<TModel, TProperty>>)(object)expression;

			Field = (expression.Body as MemberExpression).Member;
			FieldName = ExpressionHelper.GetExpressionText(expression);
			FieldType = ((FieldInfo[])((TypeInfo)expression.Body.Type).DeclaredFields)[1].FieldType;
			FieldIsNullable = HtmlHelpers.IsNullable(Field);
			FieldValue = expression.Compile().Invoke(helper.ViewData.Model);
			FieldFullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(FieldName);
			SanitizedId = TagBuilder.CreateSanitizedId(FieldFullName);
			FieldModelMetadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

			//Obter Atributos Adicionados ao Metadata...
			MetadataAttributes = new MetadataAttributes(FieldModelMetadata);

			RouteValueDictionary viewDataObj = HtmlHelper.AnonymousObjectToHtmlAttributes(viewData);
			HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(viewDataObj["htmlAttributes"]);
			HtmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(HtmlAttributes, viewDataObj);
		}
	}
}