using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using HtmlDynamicLibrary.Helpers;

namespace System.Web.Mvc
{
	public static class DynamicAjaxLinkExtensions
	{
		public class MvcAnchor : IDisposable
		{
			public MvcAnchor(HttpResponseBase httpResponse)
			{
				if (httpResponse == null)
					throw new ArgumentNullException("httpResponse");

				this._writer = httpResponse.Output;
			}

			public MvcAnchor(ViewContext viewContext)
			{
				if (viewContext == null)
					throw new ArgumentNullException("viewContext");

				this._writer = viewContext.Writer;
			}

			private bool _disposed;
			private readonly TextWriter _writer;

			#region IDisposable Members

			public void Dispose()
			{
				Dispose(true);
				GC.SuppressFinalize(this);
			}

			public void Dispose(bool isDispose)
			{
				if (!this._disposed)
				{
					this._disposed = true;
					this._writer.Write("</a>");
				}
			}

			public void EndAnchor()
			{
				Dispose(true);
			}

			#endregion
		}

		public static MvcAnchor BeginActionLink(this AjaxHelper helper, string actionName, string controllerName, RouteValueDictionary routeValues, AjaxOptions ajaxOptions, RouteValueDictionary htmlAttributes)
		{
			var targetUrl = UrlHelper.GenerateUrl(null, actionName, controllerName, routeValues, helper.RouteCollection, helper.ViewContext.RequestContext, true);
			var builder = new TagBuilder("a");
			builder.MergeAttributes(htmlAttributes);
			builder.MergeAttribute("href", targetUrl);
			builder.MergeAttributes(ajaxOptions.ToUnobtrusiveHtmlAttributes());
			helper.ViewContext.Writer.Write(builder.ToString(TagRenderMode.StartTag));

			return new MvcAnchor(helper.ViewContext);
		}
		public static MvcAnchor BeginActionLink(this AjaxHelper helper, string actionName, AjaxOptions ajaxOptions)
		{
			return helper.BeginActionLink(actionName, null, ajaxOptions);
		}

		public static MvcAnchor BeginActionLink(this AjaxHelper helper, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
		{
			return helper.BeginActionLink(actionName, controllerName, new RouteValueDictionary(routeValues), ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		public static MvcAnchor BeginActionLink(this AjaxHelper helper, string actionName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
		{
			return helper.BeginActionLink(actionName, null, new RouteValueDictionary(routeValues), ajaxOptions, HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
		}

		public static MvcAnchor BeginActionLink(this AjaxHelper helper, string actionName, string controller, AjaxOptions ajaxOptions)
		{
			return helper.BeginActionLink(actionName, controller, null, ajaxOptions, null);
		}

		public static MvcAnchor BeginActionLink(this AjaxHelper helper, string actionName, object routeValues, AjaxOptions ajaxOptions)
		{
			return helper.BeginActionLink(actionName, null, new RouteValueDictionary(routeValues), ajaxOptions, null);
		}

		public static MvcAnchor BeginActionLink(this AjaxHelper helper, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions)
		{
			return helper.BeginActionLink(actionName, controllerName, new RouteValueDictionary(routeValues), ajaxOptions, null);
		}
	}
}