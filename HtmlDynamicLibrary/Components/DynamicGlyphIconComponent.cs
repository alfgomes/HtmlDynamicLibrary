using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using HtmlDynamicLibrary.Helpers;
using HtmlDynamicLibrary.ExtensionMethods;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	public static class DynamicGlyphIconComponent
	{
		private static string GetEnumTitle(this GlyphIconType @self)
		{
			return @self.GetAttribute<EnumTitleAttribute>() != null ? @self.GetAttribute<EnumTitleAttribute>().Title : null;
		}

		public static MvcHtmlString DynamicGlyphIcon(this HtmlHelper helper, GlyphIconType glyphs, string text = null, IDictionary<string, object> htmlAttributes = null)
		{
			TagBuilder i = new TagBuilder("i");
			i.MergeAttribute("class", glyphs.GetEnumTitle());
			if (!string.IsNullOrEmpty(text))
				i.InnerHtml = text;

			return MvcHtmlString.Create(i.ToString(TagRenderMode.Normal));
		}
	}
}