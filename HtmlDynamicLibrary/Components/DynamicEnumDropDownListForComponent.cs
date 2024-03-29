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
	public static class DynamicEnumDropDownListForComponent
	{
		public static MvcHtmlString DynamicEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TEnum>> expression)
		{
			DynamicComponentBaseFor<TModel, TEnum> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TEnum>(helper, expression);

			return null;
		}

		public static MvcHtmlString DynamicEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
		{
			DynamicComponentBaseFor<TModel, TEnum> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TEnum>(helper, expression, htmlAttributes);

			return null;
		}

		public static MvcHtmlString DynamicEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TEnum>> expression, IDictionary<string, object> htmlAttributes)
		{
			DynamicComponentBaseFor<TModel, TEnum> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TEnum>(helper, expression, htmlAttributes);

			return null;
		}

		public static MvcHtmlString DynamicEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TEnum>> expression, string optionLabel)
		{
			DynamicComponentBaseFor<TModel, TEnum> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TEnum>(helper, expression);

			return null;
		}

		public static MvcHtmlString DynamicEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TEnum>> expression, string optionLabel, object htmlAttributes)
		{
			DynamicComponentBaseFor<TModel, TEnum> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TEnum>(helper, expression, htmlAttributes);

			return null;
		}

		public static MvcHtmlString DynamicEnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TEnum>> expression, string optionLabel, IDictionary<string, object> htmlAttributes)
		{
			DynamicComponentBaseFor<TModel, TEnum> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TEnum>(helper, expression, htmlAttributes);

			return null;
		}

	}
}