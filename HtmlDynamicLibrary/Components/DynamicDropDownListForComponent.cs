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
using HtmlDynamicLibrary.CustomTagBuilders;
using HtmlDynamicLibrary.Helpers;

namespace System.Web.Mvc
{
	public static class DynamicDropDownListForComponent
	{
		public static MvcHtmlString DynamicDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, object viewData = null, bool readOnly = false, bool disabled = false, bool visible = true)
		{
			DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TProperty>(helper, expression, viewData, readOnly, disabled, visible);

			TagBuilder_Select<TModel, TProperty> tagBuilder = new TagBuilder_Select<TModel, TProperty>(dynamicComponentBase);
			tagBuilder.AddOptionLabel(optionLabel);
			tagBuilder.AddOptions(selectList);

			return tagBuilder.GenerateElementMvcString(TagRenderMode.Normal);
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