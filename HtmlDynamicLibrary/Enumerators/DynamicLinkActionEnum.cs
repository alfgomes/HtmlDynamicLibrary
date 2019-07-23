using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	[Author("André Gomes", "10/07/2019", Description = "Ações disponíveis para os elementos do tipo DynamicButton e DynamicLink.")]
	public enum DynamicLinkAction
	{
		Custom,
		Refresh,
		GoBack,
		GoForward
	}
}