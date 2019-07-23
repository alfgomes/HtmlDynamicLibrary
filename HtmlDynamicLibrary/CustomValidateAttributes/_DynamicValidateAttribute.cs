using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.ComponentModel.DataAnnotations
{
	//The AttributeUsageAttribute.Inherited property indicates whether your attribute can be inherited by classes that are derived from the classes to which your attribute is applied.
	//This property takes either a true (the default) or false flag.
	
	//The AttributeUsageAttribute.AllowMultiple property indicates whether multiple instances of your attribute can exist on an element. If set to true, multiple instances are allowed; if set to false (the default), only one instance is allowed.
	//In the following example, MyAttribute has a default AllowMultiple value of false, while YourAttribute has a value of true.
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public abstract class DynamicValidateAttribute : ValidationAttribute, IClientValidatable
	{
		protected const string DefaultErrorMessage = "{0} é inválido.";

		#region Constructors...

		protected DynamicValidateAttribute()
			: base()
		{

		}

		protected DynamicValidateAttribute(string errorMessage)
			: base(errorMessage)
		{

		}

		protected DynamicValidateAttribute(Func<string> errorMessageAccessor)
			: base(errorMessageAccessor)
		{

		}

		#endregion

		#region Methods for Server side validation...

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			return ValidationResult.Success;
		}

		#endregion

		#region Abstract Methods to implement the IClientValidatable interface for client-side validation...

		public abstract IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context);

		#endregion

		public override string FormatErrorMessage(string name)
		{
			return string.Format(ErrorMessageString, name);
		}
	}
}