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
	public static class DynamicLinkComponent
	{
		public static MvcHtmlString DynamicLink(this HtmlHelper helper, DynamicLinkAction action, string id, string caption, object viewData = null, string href = null)
		{
			href = HtmlHelpers.ActionToCommand(action, href);

			TagBuilder tag = new TagBuilder("a");
			tag.Attributes.Add("id", id);
			tag.Attributes.Add("href", href);
			tag.SetInnerText(caption);

			RouteValueDictionary htmlAttributes;
			RouteValueDictionary viewDataObj = HtmlHelper.AnonymousObjectToHtmlAttributes(viewData);
			if (viewDataObj.ContainsKey("htmlAttributes"))
				htmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(viewDataObj["htmlAttributes"]);
			else
				htmlAttributes = viewDataObj;

			foreach (var attr in viewDataObj)
				tag.Attributes.Add(attr.Key, attr.Value.ToString());

			return tag.ToMvcHtmlString(TagRenderMode.Normal);
		}
	}
}