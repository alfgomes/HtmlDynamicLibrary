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
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class NIFAttribute : DynamicValidateAttribute
	{
		public string CountryCodeProperty { get; private set; }

        public NIFAttribute(string countryCodeProperty)
			: base(DefaultErrorMessage)
        {
			if (string.IsNullOrEmpty(countryCodeProperty))
				throw new ArgumentNullException("countryCodeProperty");

			CountryCodeProperty = countryCodeProperty;
        }

		//For Server side ...
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
				const int MaximumLength = 9;

				var properties = TypeDescriptor.GetProperties(value);
				foreach (PropertyDescriptor property in properties)
				{
					var stringValue = property.GetValue(value) as string;

					if (stringValue != null && (stringValue.Length > MaximumLength))
						return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
				}

				if ("PT".Equals(CountryCodeProperty) && !NIFValidate(value.ToString()))
					return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

				var otherProperty = validationContext.ObjectInstance.GetType().GetProperty(CountryCodeProperty);
                if (otherProperty != null)
                {
                   var codigoPaisPropertyValue = otherProperty.GetValue(validationContext.ObjectInstance, null);

                    if ("PT".Equals(codigoPaisPropertyValue) && !NIFValidate(value.ToString()))
                        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
                }
            }

			return base.IsValid(value, validationContext);
        }

		//Implement IClientValidatable for client side Validation...
		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
		{
			string errorMessage = this.FormatErrorMessage(metadata.DisplayName);

			// The value we set here are needed by the jQuery adapter
			var validaNifPT = new ModelClientValidationRule[] { new ModelClientValidationRule
				{
					ErrorMessage = errorMessage,
					ValidationType = "nifpt", // This is the name the jQuery adapter will use
				}
			};

			validaNifPT[0].ValidationParameters.Add("countryCodeProperty", CountryCodeProperty);

			return validaNifPT;
		}

		private bool NIFValidate(string nif)
        {
            bool ret = false;

            if (nif.Any(c => char.IsDigit(c) == false)) return false;

            nif = new string(nif.Where(c => char.IsDigit(c)).ToArray());

            if (nif.Length != 9) return false;

            long checkDigit = 0;
            string[] nifString = new string[9];
            string nifChar = null;

			for (short i = 0; i <= 8; i++)
				nifString[i] = Convert.ToString(nif[i]);

			if (nif.Length.Equals(9))
            {
                nifChar = nifString[0];
                if (nifString[0].Equals("1") || nifString[0].Equals("2") || nifString[0].Equals("5") || nifString[0].Equals("6") || nifString[0].Equals("7") || nifString[0].Equals("8") || nifString[0].Equals("9"))
                {
                    checkDigit = Convert.ToInt32(nifChar) * 9;

                    for (short i = 2; i <= 8; i++)
                        checkDigit = checkDigit + (Convert.ToInt32(nifString[i - 1]) * (10 - i));

                    checkDigit = 11 - (checkDigit % 11);

                    if ((checkDigit >= 10))
                        checkDigit = 0;

                    if ((checkDigit == Convert.ToInt32(nifString[8])))
                        ret = true;
                }
            }

            return ret;
        }
	}
}