using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.ComponentModel.DataAnnotations
{
	//The AttributeUsageAttribute.Inherited property indicates whether your attribute can be inherited by classes that are derived from the classes to which your attribute is applied.
	//This property takes either a true (the default) or false flag.
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public class DynamicAttribute : Attribute
	{
		public DynamicAttribute()
			: base()
		{

		}
	}
}