using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.ComponentModel.DataAnnotations
{
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