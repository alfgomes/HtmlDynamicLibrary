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
	public static class JavascriptHelpers
	{
		//public static MvcHtmlString InjectScript(this HtmlHelper helper, string id, string code)
		//{
		//	int screenCharactersHeight = helper.ViewContext.HttpContext.Request.Browser.ScreenCharactersHeight;
		//	int screenCharactersWidth = helper.ViewContext.HttpContext.Request.Browser.ScreenCharactersWidth;
		//	string browserType = helper.ViewContext.HttpContext.Request.Browser.Type;
		//	var headers = helper.ViewContext.HttpContext.Request.Headers;

		//	var scriptsSection = helper.ViewContext.RequestContext.HttpContext.GetSection("scripts");

		//	dynamic document = helper.ViewContext.View;

		//	TagBuilder script = new TagBuilder("SCRIPT");
		//	script.Attributes.Add("id", $"script_{id}");
		//	script.Attributes.Add("type", "text/javascript");
		//	script.InnerHtml = $"{code}";

		//	return script.ToMvcHtmlString(TagRenderMode.Normal);
		//}

		//public static MvcHtmlString InjectScriptFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, string code)
		//{
		//	var fieldName = ExpressionHelper.GetExpressionText(expression);

		//	TagBuilder script = new TagBuilder("SCRIPT");
		//	script.Attributes.Add("id", $"script_{fieldName}");
		//	script.Attributes.Add("type", "text/javascript");
		//	script.InnerHtml = $"{code}";

		//	return script.ToMvcHtmlString(TagRenderMode.Normal);
		//}

		private static IDictionary<string, string> razorScripts = new Dictionary<string, string>();

		public static MvcHtmlString AddScript(this HtmlHelper helper, string name, string code)
		{
			if (!razorScripts.ContainsKey(name))
				razorScripts.Add(name, code);

			return null;
		}

		public static MvcHtmlString RenderRazorScripts(this HtmlHelper helper)
		{
			//Injetar os Scripts em uma lista...
			//Executar um método que irá obter essa lista e injetar no html
			//((HttpResponseWrapper)((HttpContextWrapper)helper.ViewContext.HttpContext).Response).Output.Write("<script id=\"scripts_razor\" type=\"text/javascript\"></script>");
			string ret = "";
			foreach (var key in razorScripts.Keys)
			{
				TagBuilder tagScript = new TagBuilder("script");
				tagScript.Attributes.Add("name", $"js-{key}");
				tagScript.Attributes.Add("type", "text/javascript");
				tagScript.InnerHtml = razorScripts[key];
				ret += tagScript.ToMvcHtmlString(TagRenderMode.Normal).ToString();
			}

			return new MvcHtmlString(ret);
		}
	}
}