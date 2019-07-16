using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum)]
	public class AuthorAttribute : Attribute
	{
		public string Name { get; private set; }
		public double Version { get; set; }
		public string Comments { get; set; }

		public AuthorAttribute(string name)
		{
			this.Name = name;
			Version = 1.0;
			Comments = "";
		}
	}
}