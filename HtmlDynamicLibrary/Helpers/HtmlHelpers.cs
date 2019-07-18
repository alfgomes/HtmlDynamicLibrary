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
using HtmlDynamicLibrary.ExtensionMethods;

namespace HtmlDynamicLibrary.Helpers
{
	public static class HtmlHelpers
	{
		public static bool IsNullable<T>(T obj)
		{
			if (obj == null) return true; // obvious
			Type type = typeof(T);
			if (!type.IsValueType) return true; // ref-type
			if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
			return false; // value-type
		}

		public static object AppendValues(object primary, object secondary)
		{
			string separator = (primary != null ? " " : "");

			object result = ((primary is string[]) ? HtmlHelpers.ConvertStringArrayToString((string[])primary) : primary) + separator + ((secondary is string[]) ? HtmlHelpers.ConvertStringArrayToString((string[])secondary) : secondary);

			var convertedInArray = ((string)result).Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

			return convertedInArray.Distinct().ToArray();
		}

		public static string ConvertStringArrayToString(string[] array)
		{
			StringBuilder strinbuilder = new StringBuilder();

			foreach (string value in array)
			{
				strinbuilder.Append(value);
				strinbuilder.Append(' ');
			}

			return strinbuilder.ToString().Trim();
		}

		public static RouteValueDictionary MergeHtmlAttributes(RouteValueDictionary htmlAttributesObject, RouteValueDictionary defaultHtmlAttributesObject)
		{
			htmlAttributesObject = htmlAttributesObject.Merge(defaultHtmlAttributesObject);

			return htmlAttributesObject;
		}

		public static IDictionary<string, object> MergeHtmlAttributes(this HtmlHelper helper, object htmlAttributesObject, object defaultHtmlAttributesObject)
		{
			var concatKeys = new string[] { "class" };

			var htmlAttributesDict = htmlAttributesObject as IDictionary<string, object>;
			var defaultHtmlAttributesDict = defaultHtmlAttributesObject as IDictionary<string, object>;

			RouteValueDictionary htmlAttributes = (htmlAttributesDict != null) ? new RouteValueDictionary(htmlAttributesDict) : HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObject);
			RouteValueDictionary defaultHtmlAttributes = (defaultHtmlAttributesDict != null) ? new RouteValueDictionary(defaultHtmlAttributesDict) : HtmlHelper.AnonymousObjectToHtmlAttributes(defaultHtmlAttributesObject);
			defaultHtmlAttributes = new RouteValueDictionary(defaultHtmlAttributes["htmlAttributes"]);

			foreach (var item in htmlAttributes)
			{
				if (concatKeys.Contains(item.Key))
					defaultHtmlAttributes[item.Key] = (defaultHtmlAttributes[item.Key] != null) ? string.Format("{0} {1}", defaultHtmlAttributes[item.Key], item.Value) : item.Value;
				else
					defaultHtmlAttributes[item.Key] = item.Value;

				string[] sentences = defaultHtmlAttributes[item.Key].ToString().Split(new char[] { ' ' });
				defaultHtmlAttributes[item.Key] = sentences.Distinct().ToArray();
			}

			return defaultHtmlAttributes;
		}

		public static string ActionToCommand(DynamicLinkAction action, string customCommand = null)
		{
			switch (action)
			{
				case DynamicLinkAction.Custom:
					return customCommand;
				case DynamicLinkAction.Refresh:
					return "javascript:location.reload();";
				case DynamicLinkAction.GoBack:
					return "javascript:window.history.back();";
				case DynamicLinkAction.GoForward:
					return "javascript:window.history.forward();";
				default:
					return null;
			}
		}

		//public static MvcHtmlString ReadOnlyEditorFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
		//{
		//	IList<MvcHtmlString> tagList = new List<MvcHtmlString>();

		//	TValue fieldValue = expression.Compile().Invoke(helper.ViewData.Model);

		//	string result = EditorExtensions.EditorFor(helper, expression).ToHtmlString();
		//	result = result.Replace("<input ", "<input readonly=\"readonly\"");

		//	tagList.Add(new MvcHtmlString(result));

		//	StringBuilder sb = new StringBuilder();
		//	sb.Append(result);

		//	return new MvcHtmlString(result);
		//}
	}
}