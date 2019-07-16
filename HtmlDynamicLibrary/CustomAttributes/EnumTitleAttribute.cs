using System;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	public class EnumTitleAttribute : Attribute
	{
		public string Title { get; private set; }
		public string Description { get; set; }

		public EnumTitleAttribute(string title)
		{
			this.Title = title;
		}
	}
}