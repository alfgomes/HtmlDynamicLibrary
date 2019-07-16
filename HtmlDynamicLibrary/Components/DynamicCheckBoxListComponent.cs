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
	public static class DynamicCheckBoxListComponent
	{
		public static string CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> listInfo, IDictionary<string, object> htmlAttributes)
		{
			if (string.IsNullOrEmpty(name))
				throw new ArgumentException("The argument must have a value", "name");

			if (listInfo == null)
				throw new ArgumentNullException("listInfo");

			var sb = new StringBuilder();

			foreach (SelectListItem info in listInfo)
			{
				var builder = new TagBuilder("input");
				if (info.Selected)
					builder.MergeAttribute("checked", "checked");

				builder.MergeAttributes(htmlAttributes);
				builder.MergeAttribute("type", "checkbox");
				builder.MergeAttribute("value", info.Value);
				builder.MergeAttribute("name", name);
				builder.InnerHtml = info.Text;
				sb.Append(builder.ToString(TagRenderMode.Normal));
				sb.Append("<br />");
			}

			return sb.ToString();
		}
	}
}