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

namespace HtmlDynamicLibrary.CustomTagBuilders
{
	public class TagBuilder_Select<TModel, TProperty> : CustomTagBuilder<TModel, TProperty>
	{
		IList<SelectListItem> selectListItems = new List<SelectListItem>();
		SelectListItem optionLabel;

		public TagBuilder_Select(DynamicComponentBaseFor<TModel, TProperty> dynamicComponentBase)
			: base("select", dynamicComponentBase)
		{
			/* Adicionar os atributos de acordo com o que for obtido no HtmlAttributes... */
			TagElement.MergeInputAttributeHtmlAttributes("class", this.ComponentBase.HtmlAttributes);
			TagElement.MergeInputAttributeHtmlAttributes("style", this.ComponentBase.HtmlAttributes);
			
			/* Injetando o Valor no Input... */
			this.Value = this.ComponentBase.FieldValue;
			TagElement.AddInputAttributeIsNotNull("value", this.Value);
		}

		public void AddOptionLabel(string text)
		{
			this.optionLabel = new SelectListItem() { Text = text };
		}

		public void AddOptions(IEnumerable<SelectListItem> selectListItems)
		{
			this.selectListItems = selectListItems.ToList();
		}

		public void AddOption(SelectListItem selectListItem)
		{
			AddOption(selectListItem.Value, selectListItem.Text, selectListItem.Selected, selectListItem.Disabled, selectListItem.Group);
		}

		public void AddOption(object value, object text, bool selected = false, bool disabled = false, SelectListGroup group = null)
		{
			this.selectListItems.Add(new SelectListItem() { Value = value.ToString(), Text = text.ToString(), Selected = selected, Disabled = disabled, Group = group });
		}

		public override MvcHtmlString GenerateElementMvcString(TagRenderMode renderMode)
		{
			/*Criando os options...*/
			string options = "";

			TagBuilder option;
			if (this.optionLabel != null)
			{
				option = new TagBuilder("option");
				option.SetInnerText(this.optionLabel.Text);
				options += option.ToString(TagRenderMode.Normal) + "\n";
			}

			foreach (SelectListItem item in this.selectListItems)
			{
				option = new TagBuilder("option");
				if (this.Value != null && (item.Value.ToString().Trim() == this.Value.ToString().Trim()))
					option.MergeAttribute("selected", "true");
				option.MergeAttribute("value", item.Value.ToString());
				option.MergeAttribute("data-value", item.Value.ToString());
				if (item.Disabled)
					option.MergeAttribute("disabled", "true");
				option.SetInnerText(item.Text);
				options += option.ToString(TagRenderMode.Normal) + "\n";
			}

			TagElement.InnerHtml = options;

			return TagElement.ToMvcHtmlString(TagRenderMode.Normal);
		}
	}
}