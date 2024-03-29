﻿using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	[Author("André Gomes", "26/07/2019", Description = "Tipos disponíveis para renderização do Componente DynamicDisplayListFor.")]
	public enum DisplayListType
	{
		DropDownList,
		OptionsList,
		SelectedEdit,
		SelectedOnlyText,
		SelectedLabel,
		SelectedSpan
	}
}