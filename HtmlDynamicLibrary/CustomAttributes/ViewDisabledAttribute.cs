using System;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class ViewDisabledAttribute : DynamicAttribute
	{
		public ViewDisabledAttribute()
			: base()
		{

		}
	}
}