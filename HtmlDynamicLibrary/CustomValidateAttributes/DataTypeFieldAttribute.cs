using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class DataTypeFieldAttribute : DataTypeAttribute
	{
		public int Rows { get; private set; }
		public int? Cols { get; private set; }
		public bool HardWrap { get; private set; }
		public int? MinLength { get; private set; }
		public int? MaxLength { get; private set; }

		public DataTypeFieldAttribute(DataType datatype, int rows = -1, int cols = -1, int minLength = -1, int maxLength = -1, bool hardWrap = false)
			: base(datatype)
		{

		}
	}
}