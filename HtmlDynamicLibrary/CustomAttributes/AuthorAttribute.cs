using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Delegate | AttributeTargets.Enum
		| AttributeTargets.Interface | AttributeTargets.Module | AttributeTargets.Struct, AllowMultiple = false, Inherited = false)]
	public class AuthorAttribute : DynamicAttribute
	{
		public string Name { get; private set; }
		public DateTime Creation { get; private set; }
		public string Description { get; set; }

		public AuthorAttribute(string name, string creation)
			: base()
		{
			this.Name = name;
			DateTime parsedDate;
			DateTime.TryParse(creation, out parsedDate);
			this.Creation = parsedDate;
		}
	}
}