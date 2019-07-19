using System.Collections.Generic;
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
	public static class DynamicActionLinkComponent
	{
		public static MvcHtmlString DynamicActionLink<TModel>(this HtmlHelper<TModel> helper, string fieldId, string name, string type, object viewData = null, bool nullable = false, bool readOnly = false, bool disabled = false, bool visible = true)
		{
			DynamicComponentBase<TModel> dynamicComponent = new DynamicComponentBase<TModel>(helper, fieldId, name, type, viewData, nullable, readOnly, disabled, visible);

			TagBuilder tagInput = new TagBuilder("");

			return tagInput.ToMvcHtmlString(TagRenderMode.SelfClosing);
		}
	}
}