using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	[Author("André Gomes", Comments = "Ações disponíveis para os elementos do tipo Button e Link.")]
	public enum DynamicLinkAction
	{
		Custom,
		Refresh,
		GoBack,
		GoForward
	}
}