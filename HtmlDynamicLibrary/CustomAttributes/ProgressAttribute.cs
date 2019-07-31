using System;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class ProgressAttribute : DynamicAttribute
	{
		public double MinValue { get; set; } = 0;
		public double MaxValue { get; set; } = 100;
		public double Step { get; set; } = 1;

		public ProgressAttribute(double maxValue)
			: base()
		{
			this.MaxValue = maxValue;
		}
		public ProgressAttribute(double minValue, double maxValue)
			: base()
		{
			this.MinValue = minValue;
			this.MaxValue = maxValue;
		}

		public ProgressAttribute(long maxValue)
			: base()
		{
			this.MaxValue = (double)maxValue;
		}

		public ProgressAttribute(long minValue, long maxValue)
			: base()
		{
			this.MinValue = (double)minValue;
			this.MaxValue = (double)maxValue;
		}
	}
}