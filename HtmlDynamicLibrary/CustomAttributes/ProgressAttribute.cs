using System;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class ProgressAttribute : DynamicAttribute
	{
		public double MaxValue { get; set; }
		
		public ProgressAttribute(double maxValue)
			: base()
		{
			this.MaxValue = maxValue;
		}

		public ProgressAttribute(long maxValue)
			: base()
		{
			this.MaxValue = (double)maxValue;
		}
	}
}