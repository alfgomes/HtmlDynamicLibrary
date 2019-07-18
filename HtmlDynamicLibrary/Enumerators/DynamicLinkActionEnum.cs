using System;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc
{
	[Author("André Gomes", Comments = "Ações disponíveis para os elementos do tipo DynamicButton e DynamicLink.")]
	public enum DynamicLinkAction
	{
		Custom,
		Refresh,
		GoBack,
		GoForward
	}
}