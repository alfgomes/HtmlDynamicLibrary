using System;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class TextAreaAttribute : DynamicAttribute
	{
		public int Rows { get; private set; }
		public int Cols { get; private set; }
		public bool HardWrap { get; set; }
		public int MinLength { get; set; }
		public int MaxLength { get; set; }

		public TextAreaAttribute(int rows, int cols = -1)
			: base()
		{
			DataTypeAttribute dataType = new DataTypeAttribute(DataType.MultilineText);

			this.Rows = rows;
			this.Cols = cols;
		}
	}
}