using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	[Author("André Gomes", "10/07/2019", Description = "Tipos disponíveis para renderização do Componente DynamicDisplayFor.")]
	public enum DynamicDisplayType
	{
		OnlyText,
		Label,
		Span
	}
}