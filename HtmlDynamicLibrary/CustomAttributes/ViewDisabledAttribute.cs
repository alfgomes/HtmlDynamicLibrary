using System;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
	public class ViewDisabledAttribute : DynamicAttribute
	{
		public ViewDisabledAttribute()
			: base()
		{

		}
	}
}