using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace HtmlDynamicLibrary.Helpers
{
	public class MetadataAttributes
	{
		struct MetadataAttribute
		{
			public string AttributeName { get; set; }
			public IDictionary<string, object> Property { get; set; }

		}

		public ModelMetadata Metadata { get; private set; }

		IList<MetadataAttribute> metadataAttributes = new List<MetadataAttribute>();

		public MetadataAttributes(ModelMetadata metadata)
		{
			Metadata = metadata;
			LoadAttributes(Metadata);
		}

		private static IList<Attribute> GetModelMetadataAttributes(ModelMetadata metadata)
		{
			string retVal = String.Empty;

			var customTypeDescriptor = new AssociatedMetadataTypeTypeDescriptionProvider(metadata.ContainerType).GetTypeDescriptor(metadata.ContainerType);
			if (customTypeDescriptor != null)
			{
				var descriptor = customTypeDescriptor.GetProperties().Find(metadata.PropertyName, true);
				return (new List<Attribute>(descriptor.Attributes.OfType<Attribute>())).ToList<Attribute>();
			}

			return null;
		}

		private void LoadAttributes(ModelMetadata metadata)
		{
			//TO-DO: Refazer os métodos para tornar-los mais dinâmicos...

			if (metadata != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "CommonAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("IsRequired", metadata.IsRequired);
				metaAttribute.Property.Add("IsReadOnly", metadata.IsReadOnly);
				metaAttribute.Property.Add("Description", metadata.Description);
				metaAttribute.Property.Add("Watermark", metadata.Watermark);

				metadataAttributes.Add(metaAttribute);
			}

			DataTypeAttribute dataTypeAttribute = GetModelMetadataAttributes(metadata).OfType<DataTypeAttribute>().FirstOrDefault();
			DataTypeFieldAttribute dataTypeFieldAttribute = GetModelMetadataAttributes(metadata).OfType<DataTypeFieldAttribute>().FirstOrDefault();
			RegularExpressionAttribute regularExpressionAttribute = GetModelMetadataAttributes(metadata).OfType<RegularExpressionAttribute>().FirstOrDefault();
			StringLengthAttribute stringLengthAttribute = GetModelMetadataAttributes(metadata).OfType<StringLengthAttribute>().FirstOrDefault();
			MinLengthAttribute minLengthAttribute = GetModelMetadataAttributes(metadata).OfType<MinLengthAttribute>().FirstOrDefault();
			MaxLengthAttribute maxLengthAttribute = GetModelMetadataAttributes(metadata).OfType<MaxLengthAttribute>().FirstOrDefault();
			DisplayAttribute displayAttribute = GetModelMetadataAttributes(metadata).OfType<DisplayAttribute>().FirstOrDefault();
			RequiredAttribute requiredAttribute = GetModelMetadataAttributes(metadata).OfType<RequiredAttribute>().FirstOrDefault();
			RangeAttribute rangeAttribute = GetModelMetadataAttributes(metadata).OfType<RangeAttribute>().FirstOrDefault();
			DisplayFormatAttribute displayFormatAttribute = GetModelMetadataAttributes(metadata).OfType<DisplayFormatAttribute>().FirstOrDefault();
			CreditCardAttribute creditCardAttribute = GetModelMetadataAttributes(metadata).OfType<CreditCardAttribute>().FirstOrDefault();
			CustomValidationAttribute customValidationAttribute = GetModelMetadataAttributes(metadata).OfType<CustomValidationAttribute>().FirstOrDefault();
			EmailAddressAttribute emailAddressAttribute = GetModelMetadataAttributes(metadata).OfType<EmailAddressAttribute>().FirstOrDefault();
			FileExtensionsAttribute fileExtensionsAttribute = GetModelMetadataAttributes(metadata).OfType<FileExtensionsAttribute>().FirstOrDefault();
			TimestampAttribute timestampAttribute = GetModelMetadataAttributes(metadata).OfType<TimestampAttribute>().FirstOrDefault();
			ViewDisabledAttribute viewDisabledAttribute = GetModelMetadataAttributes(metadata).OfType<ViewDisabledAttribute>().FirstOrDefault();
			TextAreaAttribute textAreaAttribute = GetModelMetadataAttributes(metadata).OfType<TextAreaAttribute>().FirstOrDefault();

			OnlyNumberAttribute onlyNumberAttribute = GetModelMetadataAttributes(metadata).OfType<OnlyNumberAttribute>().FirstOrDefault();
			CurrencyAttribute currencyAttribute = GetModelMetadataAttributes(metadata).OfType<CurrencyAttribute>().FirstOrDefault();
			NoEspecialCharsAttribute noEspecialCharsAttribute = GetModelMetadataAttributes(metadata).OfType<NoEspecialCharsAttribute>().FirstOrDefault();

			if (dataTypeAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "DataTypeAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("DataType", dataTypeAttribute.DataType);
				metaAttribute.Property.Add("ErrorMessage", dataTypeAttribute.ErrorMessage);
				metaAttribute.Property.Add("ErrorMessageResourceName", dataTypeAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("RequiresValidationContext", dataTypeAttribute.RequiresValidationContext);

				metadataAttributes.Add(metaAttribute);
			}

			if (dataTypeFieldAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "DataTypeFieldAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("DataType", dataTypeFieldAttribute.DataType);
				metaAttribute.Property.Add("ErrorMessage", dataTypeFieldAttribute.ErrorMessage);
				metaAttribute.Property.Add("ErrorMessageResourceName", dataTypeFieldAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("RequiresValidationContext", dataTypeFieldAttribute.RequiresValidationContext);
				metaAttribute.Property.Add("Cols", dataTypeFieldAttribute.Cols);
				metaAttribute.Property.Add("Rows", dataTypeFieldAttribute.Rows);
				metaAttribute.Property.Add("Wrap", (dataTypeFieldAttribute.HardWrap ? "hard" : null));
				metaAttribute.Property.Add("MinLength", dataTypeFieldAttribute.MinLength);
				metaAttribute.Property.Add("MaxLength", dataTypeFieldAttribute.MaxLength);

				metadataAttributes.Add(metaAttribute);
			}

			if (regularExpressionAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "RegularExpressionAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("Pattern", regularExpressionAttribute.Pattern);
				metaAttribute.Property.Add("ErrorMessage", regularExpressionAttribute.ErrorMessage);
				metaAttribute.Property.Add("ErrorMessageResourceName", regularExpressionAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("RequiresValidationContext", regularExpressionAttribute.RequiresValidationContext);

				metadataAttributes.Add(metaAttribute);
			}

			if (stringLengthAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "StringLengthAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("MinimumLength", stringLengthAttribute.MinimumLength);
				metaAttribute.Property.Add("MaximumLength", stringLengthAttribute.MaximumLength);
				metaAttribute.Property.Add("ErrorMessage", stringLengthAttribute.ErrorMessage);
				metaAttribute.Property.Add("FormatErrorMessage", stringLengthAttribute.FormatErrorMessage(metadata.PropertyName));
				metaAttribute.Property.Add("ErrorMessageResourceName", stringLengthAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("RequiresValidationContext", stringLengthAttribute.RequiresValidationContext);

				metadataAttributes.Add(metaAttribute);
			}

			if (minLengthAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "MinLengthAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("Length", minLengthAttribute.Length);
				metaAttribute.Property.Add("TypeId", minLengthAttribute.TypeId);
				metaAttribute.Property.Add("ErrorMessage", minLengthAttribute.ErrorMessage);
				metaAttribute.Property.Add("ErrorMessageResourceName", minLengthAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("RequiresValidationContext", minLengthAttribute.RequiresValidationContext);

				metadataAttributes.Add(metaAttribute);
			}

			if (maxLengthAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "MaxLengthAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("Length", maxLengthAttribute.Length);
				metaAttribute.Property.Add("TypeId", maxLengthAttribute.TypeId);
				metaAttribute.Property.Add("ErrorMessage", maxLengthAttribute.ErrorMessage);
				metaAttribute.Property.Add("ErrorMessageResourceName", maxLengthAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("RequiresValidationContext", maxLengthAttribute.RequiresValidationContext);

				metadataAttributes.Add(metaAttribute);
			}

			if (displayAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "DisplayAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("ShortName", displayAttribute.ShortName);
				metaAttribute.Property.Add("Name", displayAttribute.Name);
				metaAttribute.Property.Add("Prompt", displayAttribute.Prompt);
				metaAttribute.Property.Add("GroupName", displayAttribute.GroupName);
				metaAttribute.Property.Add("Description", displayAttribute.Description);

				metadataAttributes.Add(metaAttribute);
			}

			if (requiredAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "RequiredAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("IsRequired", true);
				metaAttribute.Property.Add("AllowEmptyStrings", requiredAttribute.AllowEmptyStrings);
				metaAttribute.Property.Add("ErrorMessage", requiredAttribute.ErrorMessage);
				metaAttribute.Property.Add("ErrorMessageResourceName", requiredAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("RequiresValidationContext", requiredAttribute.RequiresValidationContext);

				metadataAttributes.Add(metaAttribute);
			}

			if (rangeAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "RangeAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("OperandType", rangeAttribute.OperandType);
				metaAttribute.Property.Add("AllowEmptyStrings", rangeAttribute.Minimum);
				metaAttribute.Property.Add("Maximum", rangeAttribute.Maximum);
				metaAttribute.Property.Add("ErrorMessage", rangeAttribute.ErrorMessage);
				metaAttribute.Property.Add("ErrorMessageResourceName", rangeAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("RequiresValidationContext", rangeAttribute.RequiresValidationContext);

				metadataAttributes.Add(metaAttribute);
			}

			if (displayFormatAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "DisplayFormatAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("DataFormatString", displayFormatAttribute.DataFormatString);
				metaAttribute.Property.Add("ApplyFormatInEditMode", displayFormatAttribute.ApplyFormatInEditMode);
				metaAttribute.Property.Add("ConvertEmptyStringToNull", displayFormatAttribute.ConvertEmptyStringToNull);
				metaAttribute.Property.Add("HtmlEncode", displayFormatAttribute.HtmlEncode);
				metaAttribute.Property.Add("NullDisplayText", displayFormatAttribute.NullDisplayText);
				metaAttribute.Property.Add("IsDefaultAttribute", displayFormatAttribute.IsDefaultAttribute());

				metadataAttributes.Add(metaAttribute);
			}

			if (creditCardAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "CreditCardAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("DataType", creditCardAttribute.DataType);
				metaAttribute.Property.Add("CustomDataType", creditCardAttribute.CustomDataType);
				metaAttribute.Property.Add("DisplayFormat", creditCardAttribute.DisplayFormat);
				metaAttribute.Property.Add("ErrorMessage", creditCardAttribute.ErrorMessage);
				metaAttribute.Property.Add("FormatErrorMessage", stringLengthAttribute.FormatErrorMessage(metadata.PropertyName));
				metaAttribute.Property.Add("ErrorMessageResourceName", creditCardAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("ErrorMessageResourceType", creditCardAttribute.ErrorMessageResourceType);
				metaAttribute.Property.Add("RequiresValidationContext", creditCardAttribute.RequiresValidationContext);

				metadataAttributes.Add(metaAttribute);
			}

			if (customValidationAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "CustomValidationAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("ValidatorType", customValidationAttribute.ValidatorType);
				metaAttribute.Property.Add("Method", customValidationAttribute.Method);
				metaAttribute.Property.Add("ErrorMessage", customValidationAttribute.ErrorMessage);
				metaAttribute.Property.Add("FormatErrorMessage", customValidationAttribute.FormatErrorMessage(metadata.PropertyName));
				metaAttribute.Property.Add("ErrorMessageResourceName", customValidationAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("ErrorMessageResourceType", customValidationAttribute.ErrorMessageResourceType);
				metaAttribute.Property.Add("RequiresValidationContext", customValidationAttribute.RequiresValidationContext);

				metadataAttributes.Add(metaAttribute);
			}

			if (emailAddressAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "EmailAddressAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("DataType", emailAddressAttribute.DataType);
				metaAttribute.Property.Add("CustomDataType", emailAddressAttribute.CustomDataType);
				metaAttribute.Property.Add("DisplayFormat", emailAddressAttribute.DisplayFormat);
				metaAttribute.Property.Add("ErrorMessage", emailAddressAttribute.ErrorMessage);
				metaAttribute.Property.Add("FormatErrorMessage", emailAddressAttribute.FormatErrorMessage(metadata.PropertyName));
				metaAttribute.Property.Add("ErrorMessageResourceName", emailAddressAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("ErrorMessageResourceType", emailAddressAttribute.ErrorMessageResourceType);
				metaAttribute.Property.Add("RequiresValidationContext", emailAddressAttribute.RequiresValidationContext);

				metadataAttributes.Add(metaAttribute);
			}

			if (fileExtensionsAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "FileExtensionsAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("DataType", fileExtensionsAttribute.DataType);
				metaAttribute.Property.Add("CustomDataType", fileExtensionsAttribute.CustomDataType);
				metaAttribute.Property.Add("DisplayFormat", fileExtensionsAttribute.DisplayFormat);
				metaAttribute.Property.Add("ErrorMessage", fileExtensionsAttribute.ErrorMessage);
				metaAttribute.Property.Add("FormatErrorMessage", fileExtensionsAttribute.FormatErrorMessage(metadata.PropertyName));
				metaAttribute.Property.Add("ErrorMessageResourceName", fileExtensionsAttribute.ErrorMessageResourceName);
				metaAttribute.Property.Add("ErrorMessageResourceType", fileExtensionsAttribute.ErrorMessageResourceType);
				metaAttribute.Property.Add("RequiresValidationContext", fileExtensionsAttribute.RequiresValidationContext);

				metadataAttributes.Add(metaAttribute);
			}

			if (timestampAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "FileExtensionsAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("TypeId", timestampAttribute.TypeId);

				metadataAttributes.Add(metaAttribute);
			}

			if (viewDisabledAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "ViewDisabledAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("Declared", true);

				metadataAttributes.Add(metaAttribute);
			}

			if (textAreaAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "TextAreaAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("Cols", textAreaAttribute.Cols);
				metaAttribute.Property.Add("Rows", textAreaAttribute.Rows);
				metaAttribute.Property.Add("Wrap", (textAreaAttribute.HardWrap ? "hard" : null));
				metaAttribute.Property.Add("MinLength", textAreaAttribute.MinLength);
				metaAttribute.Property.Add("MaxLength", textAreaAttribute.MaxLength);

				metadataAttributes.Add(metaAttribute);
			}

			if (onlyNumberAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "OnlyNumberAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("Declared", true);
				metaAttribute.Property.Add("ClassDecorator", "onlyNumber");

				metadataAttributes.Add(metaAttribute);
			}

			if (currencyAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "CurrencyAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("Declared", true);
				metaAttribute.Property.Add("ClassDecorator", "onlyNumber");
				metaAttribute.Property.Add("Pattern", "currency");

				metadataAttributes.Add(metaAttribute);
			}

			if (noEspecialCharsAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "NoEspecialCharsAttribute",
					Property = new Dictionary<string, object>()
				};

				metaAttribute.Property.Add("Declared", true);
				metaAttribute.Property.Add("ClassDecorator", "NoCaracEsp");

				metadataAttributes.Add(metaAttribute);
			}
		}

		public bool HasAttribute<TResult>(string attributeName)
		{
			if (!attributeName.Contains("Attribute")) attributeName += "Attribute";

			try
			{
				var ret = metadataAttributes.FirstOrDefault(a => a.AttributeName == attributeName);
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool HasParameter<TResult>(string attributeName, string parameterName)
		{
			if (!attributeName.Contains("Attribute")) attributeName += "Attribute";

			try
			{
				var ret = metadataAttributes.FirstOrDefault(a => a.AttributeName == attributeName).Property[parameterName];
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public TResult GetValue<TResult>(string attributeName, string parameterName)
		{
			if (!attributeName.Contains("Attribute")) attributeName += "Attribute";

			TResult ret = default(TResult);
			try
			{
				ret = (TResult)metadataAttributes.FirstOrDefault(a => a.AttributeName == attributeName).Property[parameterName];
				return ret;
			}
			catch (Exception ex)
			{
				return ret;
			}
		}

		public static TResult GetValueDirect<TResult>(ModelMetadata metadata, string attributeName, string parameterName)
		{
			TResult ret = default(TResult);

			if (!attributeName.Contains("Attribute")) attributeName += "Attribute";

			if (metadata != null)
			{
				IList<Attribute> teste1 = GetModelMetadataAttributes(metadata);
				//string name = metadata.PropertyName[parameterName];
			}

			return ret;
		}
	}
}