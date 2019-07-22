using System;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
	public class TextAreaAttribute : DynamicAttribute
	{
		public int Rows { get; private set; }
		public int? Cols { get; private set; }
		public bool HardWrap { get; private set; }
		public int? MinLength { get; private set; }
		public int? MaxLength { get; private set; }

		public TextAreaAttribute(int rows, int cols = -1, int minLength = -1, int maxLength = -1, bool hardWrap = false)
			: base()
		{
			DataTypeAttribute dataType = new DataTypeAttribute(DataType.MultilineText);

			this.Rows = rows;
			this.Cols = cols >= 0 ? cols : (int?)null;
			this.HardWrap = hardWrap;
			this.MinLength = minLength >= 0 ? minLength : (int?)null;
			this.MaxLength = maxLength >= 0 ? maxLength : (int?)null;
		}
	}
}