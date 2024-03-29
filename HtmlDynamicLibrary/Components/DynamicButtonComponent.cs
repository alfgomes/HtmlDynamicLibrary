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
using HtmlDynamicLibrary.ExtensionMethods;

namespace System.Web.Mvc
{
	public static class DynamicButtonComponent
	{
		public static MvcHtmlString DynamicButton(this HtmlHelper helper, LinkAction action, string id, string caption, bool printable = true, object viewData = null, string function = null)
		{
			function = HtmlHelpers.ActionToCommand(action, function);

			TagBuilder tag = new TagBuilder("input");
			tag.Attributes.Add("type", "button");
			tag.Attributes.Add("id", id);
			tag.Attributes.Add("value", caption);
			tag.Attributes.Add("onClick", function);

			RouteValueDictionary htmlAttributes;
			RouteValueDictionary viewDataObj = HtmlHelper.AnonymousObjectToHtmlAttributes(viewData);
			if (viewDataObj.ContainsKey("htmlAttributes"))
				htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(viewDataObj["htmlAttributes"]);
			else
				htmlAttributes = viewDataObj;

			tag.MergeAttributeValue("class", "btn", true);
			if (!printable)
				tag.MergeAttributeValue("class", "d-print-none", true);

			foreach (var attr in htmlAttributes)
				tag.Attributes.Add(attr.Key, attr.Value.ToString());

			return tag.ToMvcHtmlString(TagRenderMode.SelfClosing);
		}
	}
}