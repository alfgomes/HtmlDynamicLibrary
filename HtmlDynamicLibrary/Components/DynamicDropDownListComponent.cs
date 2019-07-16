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
	public static class DynamicDropDownListComponent
	{
		public static MvcHtmlString DynamicDropDownList(this HtmlHelper htmlHelper, string name)
		{
			return null;
		}

		public static MvcHtmlString DynamicDropDownList(this HtmlHelper htmlHelper, string name, string optionLabel)
		{
			return null;
		}

		public static MvcHtmlString DynamicDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList)
		{
			var dropdown = new TagBuilder("select");

			var options = "";
			TagBuilder option;

			foreach (var item in selectList)
			{
				option = new TagBuilder("option");
				option.MergeAttribute("value", item.Value.ToString());
				option.MergeAttribute("data-value", item.Value.ToString());
				option.SetInnerText(item.Text);
				options += option.ToString(TagRenderMode.Normal) + "\n";
			}

			dropdown.MergeAttribute("data-val", "true");
			dropdown.MergeAttribute("data-val-required", "Esse campo é obrigatório.");
			dropdown.MergeAttribute("id", name);
			dropdown.MergeAttribute("name", name);

			dropdown.InnerHtml = options;

			return new MvcHtmlString(dropdown.ToString(TagRenderMode.Normal));
		}

		public static MvcHtmlString DynamicDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes)
		{
			return null;
		}

		public static MvcHtmlString DynamicDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, IDictionary<string, object> htmlAttributes)
		{
			return null;
		}

		public static MvcHtmlString DynamicDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel)
		{
			return null;
		}

		public static MvcHtmlString DynamicDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, object htmlAttributes)
		{
			TagBuilder dropdown = new TagBuilder("select");

			var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
			dropdown.MergeAttributes(attributes, true);

			var options = "";
			TagBuilder option;

			foreach (var item in selectList)
			{
				option = new TagBuilder("option");
				option.MergeAttribute("value", item.Value.ToString());
				option.MergeAttribute("data-value", item.Value.ToString());
				option.SetInnerText(item.Text);
				options += option.ToString(TagRenderMode.Normal) + "\n";
			}

			dropdown.MergeAttribute("data-val", "true");
			dropdown.MergeAttribute("data-val-required", "Esse campo é obrigatório.");
			dropdown.MergeAttribute("id", name);
			dropdown.MergeAttribute("name", name);

			dropdown.InnerHtml = options;

			return new MvcHtmlString(dropdown.ToString(TagRenderMode.Normal));
		}

		public static MvcHtmlString DynamicDropDownList(this HtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, string optionLabel, IDictionary<string, object> htmlAttributes)
		{
			return null;
		}

	}
}
