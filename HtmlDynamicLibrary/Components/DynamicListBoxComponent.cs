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
	public static class DynamicListBoxComponent
	{
		public static MvcHtmlString DynamicListBox(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
		{
			return null;
		}

		public static MvcHtmlString DynamicListBox(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return null;
		}

		public static MvcHtmlString DynamicListBox(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return null;
		}

	}
}
