using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using HtmlDynamicLibrary.Helpers;

namespace System.ComponentModel.DataAnnotations
{
	// Summary: Specifies the minimum and maximum length of characters that are allowed in a type number data field.
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class NumberLengthAttribute : DynamicValidateAttribute
	{
		public int MinimumLength { get; set; }
		public int MaximumLength { get; private set; }
		public string ErrorMessage { get; set; }

		public NumberLengthAttribute(int maximumLength)
		{
			this.MaximumLength = maximumLength;
		}

		public NumberLengthAttribute(int maximumLength, string errorMessage)
			: base(errorMessage)
		{
			this.MaximumLength = maximumLength;
		}

		public NumberLengthAttribute(int maximumLength, Func<string> errorMessageAccessor)
			: base(errorMessageAccessor)
		{
			this.MaximumLength = maximumLength;
		}

		public override string FormatErrorMessage(string name)
		{
			throw new NotImplementedException();
		}

		public override bool IsValid(object value)
		{
			throw new NotImplementedException();
		}

		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			throw new NotImplementedException();
		}
	}
}