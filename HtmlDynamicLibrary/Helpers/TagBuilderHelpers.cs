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
	public static class TagBuilderHelpers
	{
		#region Extension Methods By TagBuilder...

		#region For HtmlAttributes...

		public static void AddInputAttributeHtmlAttributes(this TagBuilder tagInput, string attributeName, RouteValueDictionary htmlAttributes)
		{
			if (htmlAttributes[attributeName] == null) return;

			tagInput.AddInputAttributeIsNotNullAndExpressionIsTrue(attributeName, htmlAttributes[attributeName], htmlAttributes[attributeName] != null);
		}

		public static void MergeInputAttributeHtmlAttributes(this TagBuilder tagInput, string attributeName, RouteValueDictionary htmlAttributes)
		{
			if (htmlAttributes[attributeName] == null) return;

			RouteValueDictionary tagRouteValueDictionary = tagInput.Attributes.ToRouteValueDictionary();
			htmlAttributes = HtmlHelpers.MergeHtmlAttributes(htmlAttributes, tagRouteValueDictionary);

			tagInput.AddInputAttributeHtmlAttributes(attributeName, htmlAttributes);
		}

		#endregion

		#region For ValueAttributes...

		public static void AddInputAttributeIsNotNull(this TagBuilder tagInput, string attributeName, object value)
		{
			AddInputAttributeIsNotNullAndExpressionIsTrue(tagInput, attributeName, value, value != null);
		}

		public static void AddInputAttributeIfExpressionIsTrue(this TagBuilder tagInput, string attributeName, object value, bool validateExpression)
		{
			if (!validateExpression) return;

			AddInputAttributeStaticValue(tagInput, attributeName, value);
		}

		public static void AddInputAttributeIsNotNullAndExpressionIsTrue(this TagBuilder tagInput, string attributeName, object value, bool validateExpression)
		{
			if (value == null) return;
			if (!validateExpression) return;

			AddInputAttributeStaticValue(tagInput, attributeName, value);
		}

		public static void AddInputAttributeStaticValue(this TagBuilder tagInput, string attributeName, object value)
		{
			if (tagInput.Attributes.ContainsKey(attributeName.ToLowerInvariant()))
				tagInput.Attributes.Remove(attributeName.ToLowerInvariant());

			object valueTreated = null;

			if (value != null)
			{
				bool testBool;
				Boolean.TryParse(value.GetType().IsArray ? ((string[])value)[0] : value.ToString(), out testBool);
				if (testBool)
				{
					switch (attributeName.ToLowerInvariant())
					{
						case "autofocus":
						case "required":
						case "readonly":
						case "disabled":
						case "spellcheck":
							valueTreated = null;
							break;
					}
				}
				else
				{
					if (value != null && value.GetType().IsArray)
					{
						valueTreated = "";
						foreach (var item in (Array)value)
							valueTreated += item.ToString() + " ";
					}
					else
					{
						valueTreated = value;
					}
				}
			}

			tagInput.Attributes.Add(attributeName.ToLowerInvariant(), valueTreated?.ToString().Trim());
		}

		public static void MergeInputAttributeIsNotNull(this TagBuilder tagInput, string attributeName, object value)
		{
			MergeInputAttributeIsNotNullAndExpressionIsTrue(tagInput, attributeName, value, value != null);
		}

		public static void MergeInputAttributeIsNotNullAndExpressionIsTrue(this TagBuilder tagInput, string attributeName, object value, bool validateExpression)
		{
			if (!validateExpression) return;
			if (value == null) return;

			MergeInputAttributeStaticValue(tagInput, attributeName, value);
		}

		public static void MergeInputAttributeStaticValue(this TagBuilder tagInput, string attributeName, object value)
		{
			object valueTreated = null;

			bool testBool;
			Boolean.TryParse(value.GetType().IsArray ? ((string[])value)[0] : value.ToString(), out testBool);
			if (testBool)
			{
				switch (attributeName.ToLowerInvariant())
				{
					case "autofocus":
					case "required":
					case "readonly":
					case "disabled":
					case "spellcheck":
						valueTreated = null;
						break;
				}
			}
			else
			{
				if (value.GetType().IsArray)
				{
					valueTreated = "";
					foreach (var item in (Array)value)
						valueTreated += item.ToString() + " ";
				}
				else
				{
					valueTreated = value;
				}
			}

			RouteValueDictionary htmlAttributes = new RouteValueDictionary();
			foreach (var item in tagInput.Attributes)
				htmlAttributes.Add(item.Key, item.Value);
			RouteValueDictionary tagRouteValueDictionary = new RouteValueDictionary(new Dictionary<string, object>() { { attributeName.ToLowerInvariant(), valueTreated } });
			htmlAttributes = HtmlHelpers.MergeHtmlAttributes(htmlAttributes, tagRouteValueDictionary);

			tagInput.AddInputAttributeHtmlAttributes(attributeName, htmlAttributes);
		}

		public static void RemoveAttribute(this TagBuilder tagInput, string attributeName)
		{
			if (tagInput.Attributes.ContainsKey(attributeName.ToLowerInvariant()))
				tagInput.Attributes.Remove(attributeName.ToLowerInvariant());
		}

		public static void DeleteValueInAttribute(this TagBuilder tagInput, string attributeName, object value)
		{
			if (tagInput.Attributes.ContainsKey(attributeName.ToLowerInvariant()))
			{
				string temp;
				tagInput.Attributes.TryGetValue(attributeName.ToLowerInvariant(), out temp);
				if (temp.Contains(value.ToString()))
					tagInput.Attributes[attributeName.ToLowerInvariant()] = temp.Replace(value.ToString(), "").Trim();
			}
		}

		#endregion

		public static void AddInputTypeAttribute(this TagBuilder tagInput, Type fieldType)
		{
			if (fieldType == null) return;

			if (fieldType.Equals(new Boolean().GetType()))
				tagInput.AddInputAttributeStaticValue("type", "checkbox");
			else if (fieldType.Equals(new int().GetType()))
				tagInput.AddInputAttributeStaticValue("type", "number");
			else
				tagInput.AddInputAttributeStaticValue("type", "text");
		}

		public static MvcHtmlString ToMvcHtmlString(this TagBuilder tagBuilder, TagRenderMode renderMode)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return new MvcHtmlString(tagBuilder.ToString(renderMode));
		}

		#endregion
	}
}