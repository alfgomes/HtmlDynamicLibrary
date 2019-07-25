using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class HtmlAttributesAttributes : DynamicAttribute
	{
		public struct HtmlSizeAttribute
		{
			public int Size { get; set; }
			public string Measure { get; set; }
		}

		public string Class { get; set; }
		public string Style { get; set; }
		public HtmlSizeAttribute Width { get; set; }
		public HtmlSizeAttribute Height { get; set; }

		public HtmlAttributesAttributes()
			: base()
		{

		}
	}
}