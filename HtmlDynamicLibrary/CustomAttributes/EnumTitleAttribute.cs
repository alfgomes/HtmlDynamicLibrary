using System;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
	public class EnumTitleAttribute : DynamicAttribute
	{
		public string Title { get; private set; }
		public string Description { get; set; }

		public EnumTitleAttribute(string title)
			: base()
		{
			this.Title = title;
		}
	}
}