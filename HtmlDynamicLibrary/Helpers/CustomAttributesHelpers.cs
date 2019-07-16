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

namespace HtmlDynamicLibrary.Helpers
{
	public static class CustomAttributesHelpers
	{
		public static string ConvertDataTypeToHtmlType(DataType dataType, Type fieldType = null)
		{
			return GetHtmlTypeByFieldType(fieldType) != null ? GetHtmlTypeByFieldType(fieldType) : GetHtmlTypeByDataType(dataType);
		}

		public static string GetHtmlTypeByFieldType(Type fieldType)
		{
			if (fieldType != null && fieldType.Equals(new bool()))
				return "checkbox";

			return null;
		}

		public static string GetHtmlTypeByDataType(DataType dataType)
		{
			switch (dataType)
			{
				case DataType.Custom:
					return "text";
				case DataType.DateTime:
					return "datetime";
				case DataType.Date:
					return "date";
				case DataType.Time:
					return "time";
				case DataType.Duration:
					return "number";
				case DataType.PhoneNumber:
					return "tel";
				case DataType.Currency:
					return "currency";
				case DataType.Text:
					return "input";
				case DataType.Html:
					return "html";
				case DataType.MultilineText:
					return "text";
				case DataType.EmailAddress:
					return "mail";
				case DataType.Password:
					return "pass";
				case DataType.Url:
					return "url";
				case DataType.ImageUrl:
					return "url";
				case DataType.CreditCard:
					return "tel";
				case DataType.PostalCode:
					return "tel";
				case DataType.Upload:
					return "file";
				default:
					return "text";
			}
		}

		public static string GetInputElementTypeByDataType(DataType dataType)
		{
			switch (dataType)
			{
				case DataType.Custom:
					return "input";
				case DataType.DateTime:
					return "input";
				case DataType.Date:
					return "input";
				case DataType.Time:
					return "input";
				case DataType.Duration:
					return "input";
				case DataType.PhoneNumber:
					return "input";
				case DataType.Currency:
					return "input";
				case DataType.Text:
					return "input";
				case DataType.Html:
					return "input";
				case DataType.MultilineText:
					return "textarea";
				case DataType.EmailAddress:
					return "input";
				case DataType.Password:
					return "input";
				case DataType.Url:
					return "input";
				case DataType.ImageUrl:
					return "input";
				case DataType.CreditCard:
					return "input";
				case DataType.PostalCode:
					return "input";
				case DataType.Upload:
					return "input";
				default:
					return "input";
			}
		}
	}
}