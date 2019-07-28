using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	[Author("André Gomes", "28/07/2019", Description = "Tipos disponíveis para renderização do Componente DynamicEditorListFor.")]
	public enum EditorListType
	{
		DropDownList,
		OptionsList
	}
}