using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
	public class HtmlAttributesAttribute : DynamicAttribute
	{
		//internal struct SizeProp
		//{
		//	public int Size { get; private set; }
		//	public string Measure { get; private set; }

		//	public SizeProp(int size, string measure = null)
		//	{
		//		this.Size = size;
		//		this.Measure = measure;
		//	}
		//}

		public DataType Type { get; set; }
		public string ID { get; set; }
		public string Name { get; set; }
		public string Class { get; set; }
		public string Style { get; set; }
		public string Width { get; set; }
		public string Height { get; set; }
		public string Placeholder { get; set; }

		public HtmlAttributesAttribute()
			: base()
		{

		}
	}
}