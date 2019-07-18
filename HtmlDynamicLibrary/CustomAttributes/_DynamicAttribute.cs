using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.ComponentModel.DataAnnotations
{
	//A propriedade Inherited indica se seu atributo pode ser herdado por classes derivadas das classes às quais seu atributo é aplicado.
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	public class DynamicAttribute : Attribute
	{
		public DynamicAttribute()
			: base()
		{

		}
	}
}