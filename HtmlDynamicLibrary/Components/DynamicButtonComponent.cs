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
	public static class DynamicButtonComponent
	{
		public static MvcHtmlString DynamicButton(this HtmlHelper helper, DynamicLinkAction action, string id, string caption, object viewData = null, string function = null)
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

			foreach (var attr in htmlAttributes)
				tag.Attributes.Add(attr.Key, attr.Value.ToString());

			return tag.ToMvcHtmlString(TagRenderMode.SelfClosing);
		}
	}
}