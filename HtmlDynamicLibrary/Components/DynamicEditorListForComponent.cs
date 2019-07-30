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
using HtmlDynamicLibrary.CustomTagBuilders;
using HtmlDynamicLibrary.Helpers;

namespace System.Web.Mvc
{
	public static class DynamicEditorListForComponent
	{
		public static MvcHtmlString DynamicEditorListFor<TModel, TProperty>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel, EditorListType editorType = EditorListType.DropDownList, object viewData = null)
		{
			DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase = new DynamicComponentBaseFor<TModel, TProperty>(helper, expression, viewData, true, true);
			object selectedValue = dynamicComponentBase.FieldValue;
			string selectedText = selectList.Where(w => w.Value == selectedValue?.ToString()).FirstOrDefault()?.Text;

			switch (editorType)
			{
				case EditorListType.DropDownList:
					TagBuilder_Select<TModel, TProperty> tagSelBuilder = new TagBuilder_Select<TModel, TProperty>(dynamicComponentBase);
					tagSelBuilder.AddOptionLabel(optionLabel);
					tagSelBuilder.AddOptions(selectList);
					return tagSelBuilder.GenerateElementMvcString(TagRenderMode.Normal);
				case EditorListType.OptionsList:
					//<input type="radio" name="group2" value="male" checked> Male
					TagBuilder_RadioGroup<TModel, TProperty> tagRadBuilder = new TagBuilder_RadioGroup<TModel, TProperty>(dynamicComponentBase);
					tagRadBuilder.AddOptions(selectList);
					return tagRadBuilder.GenerateElementMvcString(TagRenderMode.Normal);
				default:
					return null;
			}
		}
	}
}