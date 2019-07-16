using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	[Author("André Gomes", Comments = "Tipos disponíveis para renderização do Componente DynamicDisplayFor.")]
	public enum DynamicDisplayType
	{
		OnlyText,
		Label,
		Span
	}
}