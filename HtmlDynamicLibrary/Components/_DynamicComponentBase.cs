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
	public class DynamicComponentBase<TModel>
	{
		#region Properties...

		public string FieldName { get; private set; }
		public string FieldFullName { get; private set; }
		public string SanitizedId { get; private set; }
		public RouteValueDictionary HtmlAttributes { get; set; }
		public bool FieldIsNullable { get; private set; }
		public bool FieldIsReadOnly { get; set; }
		public bool FieldIsDisabled { get; set; }
		public bool FieldIsVisible { get; set; }

		#endregion

		public DynamicComponentBase(HtmlHelper<TModel> helper, string fieldId, string name, string type, object viewData = null, bool nullable = false, bool readOnly = false, bool disabled = false, bool visible = true)
		{
			FieldName = name;
			FieldFullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(FieldName);
			SanitizedId = TagBuilder.CreateSanitizedId(FieldFullName);

			RouteValueDictionary viewDataObj = HtmlHelper.AnonymousObjectToHtmlAttributes(viewData);
			HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(viewDataObj["htmlAttributes"]);
			HtmlAttributes = (RouteValueDictionary)helper.MergeHtmlAttributes(HtmlAttributes, viewDataObj);

			FieldIsNullable = nullable;
			FieldIsReadOnly = readOnly;
			FieldIsDisabled = disabled;
			FieldIsVisible = visible;
		}
	}
}