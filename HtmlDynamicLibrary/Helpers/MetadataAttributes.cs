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
				MetadataAttribute commonAttribute = new MetadataAttribute()
				{
					AttributeName = "Common" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "DisplayName", metadata.DisplayName },
						{ "ShortDisplayName", metadata.ShortDisplayName },
						{ "IsRequired", metadata.IsRequired },
						{ "IsReadOnly", metadata.IsReadOnly },
						{ "IsNullableValueType", metadata.IsNullableValueType },
						{ "Description", metadata.Description },
						{ "Watermark", metadata.Watermark },
						{ "ShowForDisplay", metadata.ShowForDisplay },
						{ "ShowForEdit", metadata.ShowForEdit },

						{ "DataTypeName", metadata.DataTypeName },
						{ "IsComplexType", metadata.IsComplexType },
						{ "EditFormatString", metadata.EditFormatString },
						{ "HideSurroundingHtml", metadata.HideSurroundingHtml },
						{ "HtmlEncode", metadata.HtmlEncode },
						{ "ConvertEmptyStringToNull", metadata.ConvertEmptyStringToNull },
						{ "NullDisplayText", metadata.NullDisplayText },
						{ "SimpleDisplayText", metadata.SimpleDisplayText },
						{ "TemplateHint", metadata.TemplateHint },
						{ "DisplayFormatString", metadata.DisplayFormatString },
					}
				};
				metadataAttributes.Add(commonAttribute);
			}

			HtmlAttributesAttribute htmlAttributesAttribute = GetModelMetadataAttributes(metadata).OfType<HtmlAttributesAttribute>().FirstOrDefault();
			if (htmlAttributesAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "HtmlAttributes" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "ID", htmlAttributesAttribute.ID },
						{ "Name", htmlAttributesAttribute.Name },
						{ "Class", htmlAttributesAttribute.Class },
						{ "Style", htmlAttributesAttribute.Style },
						{ "Width", htmlAttributesAttribute.Width },
						{ "Height", htmlAttributesAttribute.Height },
						{ "Placeholder", htmlAttributesAttribute.Placeholder },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			DataTypeAttribute dataTypeAttribute = GetModelMetadataAttributes(metadata).OfType<DataTypeAttribute>().FirstOrDefault();
			if (dataTypeAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "DataType" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "DataType", dataTypeAttribute.DataType },
						{ "ErrorMessage", dataTypeAttribute.ErrorMessage },
						{ "ErrorMessageResourceName", dataTypeAttribute.ErrorMessageResourceName },
						{ "RequiresValidationContext", dataTypeAttribute.RequiresValidationContext },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			DataTypeFieldAttribute dataTypeFieldAttribute = GetModelMetadataAttributes(metadata).OfType<DataTypeFieldAttribute>().FirstOrDefault();
			if (dataTypeFieldAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "DataTypeField" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "DataType", dataTypeFieldAttribute.DataType },
						{ "ErrorMessage", dataTypeFieldAttribute.ErrorMessage },
						{ "ErrorMessageResourceName", dataTypeFieldAttribute.ErrorMessageResourceName },
						{ "RequiresValidationContext", dataTypeFieldAttribute.RequiresValidationContext },
						{ "Cols", dataTypeFieldAttribute.Cols },
						{ "Rows", dataTypeFieldAttribute.Rows },
						{ "Wrap", (dataTypeFieldAttribute.HardWrap ? "hard" : null) },
						{ "MinLength", dataTypeFieldAttribute.MinLength },
						{ "MaxLength", dataTypeFieldAttribute.MaxLength },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			RegularExpressionAttribute regularExpressionAttribute = GetModelMetadataAttributes(metadata).OfType<RegularExpressionAttribute>().FirstOrDefault();
			if (regularExpressionAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "RegularExpression" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "Pattern", regularExpressionAttribute.Pattern },
						{ "ErrorMessage", regularExpressionAttribute.ErrorMessage },
						{ "ErrorMessageResourceName", regularExpressionAttribute.ErrorMessageResourceName },
						{ "RequiresValidationContext", regularExpressionAttribute.RequiresValidationContext },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			StringLengthAttribute stringLengthAttribute = GetModelMetadataAttributes(metadata).OfType<StringLengthAttribute>().FirstOrDefault();
			if (stringLengthAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "StringLength" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "MinimumLength", stringLengthAttribute.MinimumLength },
						{ "MaximumLength", stringLengthAttribute.MaximumLength },
						{ "ErrorMessage", stringLengthAttribute.ErrorMessage },
						{ "FormatErrorMessage", stringLengthAttribute.FormatErrorMessage(metadata.PropertyName) },
						{ "ErrorMessageResourceName", stringLengthAttribute.ErrorMessageResourceName },
						{ "RequiresValidationContext", stringLengthAttribute.RequiresValidationContext },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			MinLengthAttribute minLengthAttribute = GetModelMetadataAttributes(metadata).OfType<MinLengthAttribute>().FirstOrDefault();
			if (minLengthAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "MinLength" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "Length", minLengthAttribute.Length },
						{ "TypeId", minLengthAttribute.TypeId },
						{ "ErrorMessage", minLengthAttribute.ErrorMessage },
						{ "ErrorMessageResourceName", minLengthAttribute.ErrorMessageResourceName },
						{ "RequiresValidationContext", minLengthAttribute.RequiresValidationContext },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			MaxLengthAttribute maxLengthAttribute = GetModelMetadataAttributes(metadata).OfType<MaxLengthAttribute>().FirstOrDefault();
			if (maxLengthAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "MaxLength" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "Length", maxLengthAttribute.Length },
						{ "TypeId", maxLengthAttribute.TypeId },
						{ "ErrorMessage", maxLengthAttribute.ErrorMessage },
						{ "ErrorMessageResourceName", maxLengthAttribute.ErrorMessageResourceName },
						{ "RequiresValidationContext", maxLengthAttribute.RequiresValidationContext },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			DisplayAttribute displayAttribute = GetModelMetadataAttributes(metadata).OfType<DisplayAttribute>().FirstOrDefault();
			if (displayAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "Display" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "ShortName", displayAttribute.ShortName },
						{ "Name", displayAttribute.Name },
						{ "Prompt", displayAttribute.Prompt },
						{ "GroupName", displayAttribute.GroupName },
						{ "Description", displayAttribute.Description },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			RequiredAttribute requiredAttribute = GetModelMetadataAttributes(metadata).OfType<RequiredAttribute>().FirstOrDefault();
			if (requiredAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "Required" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "IsRequired", true },
						{ "AllowEmptyStrings", requiredAttribute.AllowEmptyStrings },
						{ "ErrorMessage", requiredAttribute.ErrorMessage },
						{ "ErrorMessageResourceName", requiredAttribute.ErrorMessageResourceName },
						{ "RequiresValidationContext", requiredAttribute.RequiresValidationContext },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			RangeAttribute rangeAttribute = GetModelMetadataAttributes(metadata).OfType<RangeAttribute>().FirstOrDefault();
			if (rangeAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "Range" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "OperandType", rangeAttribute.OperandType },
						{ "AllowEmptyStrings", rangeAttribute.Minimum },
						{ "Maximum", rangeAttribute.Maximum },
						{ "ErrorMessage", rangeAttribute.ErrorMessage },
						{ "ErrorMessageResourceName", rangeAttribute.ErrorMessageResourceName },
						{ "RequiresValidationContext", rangeAttribute.RequiresValidationContext },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			DisplayFormatAttribute displayFormatAttribute = GetModelMetadataAttributes(metadata).OfType<DisplayFormatAttribute>().FirstOrDefault();
			if (displayFormatAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "DisplayFormat" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "DataFormatString", displayFormatAttribute.DataFormatString },
						{ "ApplyFormatInEditMode", displayFormatAttribute.ApplyFormatInEditMode },
						{ "ConvertEmptyStringToNull", displayFormatAttribute.ConvertEmptyStringToNull },
						{ "HtmlEncode", displayFormatAttribute.HtmlEncode },
						{ "NullDisplayText", displayFormatAttribute.NullDisplayText },
						{ "IsDefault" + "Attribute", displayFormatAttribute.IsDefaultAttribute() },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			CreditCardAttribute creditCardAttribute = GetModelMetadataAttributes(metadata).OfType<CreditCardAttribute>().FirstOrDefault();
			if (creditCardAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "CreditCard" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "DataType", creditCardAttribute.DataType },
						{ "CustomDataType", creditCardAttribute.CustomDataType },
						{ "DisplayFormat", creditCardAttribute.DisplayFormat },
						{ "ErrorMessage", creditCardAttribute.ErrorMessage },
						{ "FormatErrorMessage", stringLengthAttribute.FormatErrorMessage(metadata.PropertyName) },
						{ "ErrorMessageResourceName", creditCardAttribute.ErrorMessageResourceName },
						{ "ErrorMessageResourceType", creditCardAttribute.ErrorMessageResourceType },
						{ "RequiresValidationContext", creditCardAttribute.RequiresValidationContext },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			CustomValidationAttribute customValidationAttribute = GetModelMetadataAttributes(metadata).OfType<CustomValidationAttribute>().FirstOrDefault();
			if (customValidationAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "CustomValidation" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "ValidatorType", customValidationAttribute.ValidatorType },
						{ "Method", customValidationAttribute.Method },
						{ "ErrorMessage", creditCardAttribute.ErrorMessage },
						{ "FormatErrorMessage", stringLengthAttribute.FormatErrorMessage(metadata.PropertyName) },
						{ "ErrorMessageResourceName", creditCardAttribute.ErrorMessageResourceName },
						{ "ErrorMessageResourceType", creditCardAttribute.ErrorMessageResourceType },
						{ "RequiresValidationContext", creditCardAttribute.RequiresValidationContext },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			EmailAddressAttribute emailAddressAttribute = GetModelMetadataAttributes(metadata).OfType<EmailAddressAttribute>().FirstOrDefault();
			if (emailAddressAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "EmailAddress" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "DataType", emailAddressAttribute.DataType },
						{ "CustomDataType", emailAddressAttribute.CustomDataType },
						{ "DisplayFormat", emailAddressAttribute.DisplayFormat },
						{ "ErrorMessage", creditCardAttribute.ErrorMessage },
						{ "FormatErrorMessage", stringLengthAttribute.FormatErrorMessage(metadata.PropertyName) },
						{ "ErrorMessageResourceName", creditCardAttribute.ErrorMessageResourceName },
						{ "ErrorMessageResourceType", creditCardAttribute.ErrorMessageResourceType },
						{ "RequiresValidationContext", creditCardAttribute.RequiresValidationContext },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			FileExtensionsAttribute fileExtensionsAttribute = GetModelMetadataAttributes(metadata).OfType<FileExtensionsAttribute>().FirstOrDefault();
			if (fileExtensionsAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "FileExtensions" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "DataType", emailAddressAttribute.DataType },
						{ "CustomDataType", emailAddressAttribute.CustomDataType },
						{ "DisplayFormat", emailAddressAttribute.DisplayFormat },
						{ "ErrorMessage", creditCardAttribute.ErrorMessage },
						{ "FormatErrorMessage", stringLengthAttribute.FormatErrorMessage(metadata.PropertyName) },
						{ "ErrorMessageResourceName", creditCardAttribute.ErrorMessageResourceName },
						{ "ErrorMessageResourceType", creditCardAttribute.ErrorMessageResourceType },
						{ "RequiresValidationContext", creditCardAttribute.RequiresValidationContext },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			TimestampAttribute timestampAttribute = GetModelMetadataAttributes(metadata).OfType<TimestampAttribute>().FirstOrDefault();
			if (timestampAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "FileExtensions" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "TypeId", timestampAttribute.TypeId },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			ViewDisabledAttribute viewDisabledAttribute = GetModelMetadataAttributes(metadata).OfType<ViewDisabledAttribute>().FirstOrDefault();
			if (viewDisabledAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "ViewDisabled" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "Declared", true },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			TextAreaAttribute textAreaAttribute = GetModelMetadataAttributes(metadata).OfType<TextAreaAttribute>().FirstOrDefault();
			if (textAreaAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "TextArea" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "Cols", textAreaAttribute.Cols },
						{ "Rows", textAreaAttribute.Rows },
						{ "Wrap", (textAreaAttribute.HardWrap ? "hard" : null) },
						{ "MinLength", textAreaAttribute.MinLength },
						{ "MaxLength", textAreaAttribute.MaxLength },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			OnlyNumberAttribute onlyNumberAttribute = GetModelMetadataAttributes(metadata).OfType<OnlyNumberAttribute>().FirstOrDefault();
			if (onlyNumberAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "OnlyNumber" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "Declared", true },
						{ "ClassDecorator", "onlyNumber" },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			CurrencyAttribute currencyAttribute = GetModelMetadataAttributes(metadata).OfType<CurrencyAttribute>().FirstOrDefault();
			if (currencyAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "Currency" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "Declared", true },
						{ "ClassDecorator", "onlyNumber" },
						{ "Pattern", "currency" },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			NoEspecialCharsAttribute noEspecialCharsAttribute = GetModelMetadataAttributes(metadata).OfType<NoEspecialCharsAttribute>().FirstOrDefault();
			if (noEspecialCharsAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "NoEspecialChars" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "Declared", true },
						{ "ClassDecorator", "noCaracEsp" },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}

			ProgressAttribute progressAttribute = GetModelMetadataAttributes(metadata).OfType<ProgressAttribute>().FirstOrDefault();
			if (progressAttribute != null)
			{
				MetadataAttribute metaAttribute = new MetadataAttribute()
				{
					AttributeName = "Progress" + "Attribute",
					Property = new Dictionary<string, object>()
					{
						{ "MaxValue", progressAttribute.MaxValue },
					}
				};
				metadataAttributes.Add(metaAttribute);
			}
		}

		private static Dictionary<string, object> GetPropertyAttributes(PropertyInfo property)
		{
			Dictionary<string, object> attribs = new Dictionary<string, object>();
			if (property == null) return attribs;
			
			foreach (CustomAttributeData attribData in property?.GetCustomAttributesData())
			{
				if (attribData.ConstructorArguments.Count == 1)
				{
					string typeName = attribData.Constructor.DeclaringType.Name;
					if (typeName.EndsWith("Attribute")) typeName = typeName.Substring(0, typeName.Length - 9);
					attribs[typeName] = attribData.ConstructorArguments[0].Value;
				}
			}

			return attribs;
		}

		private string ReturnCorrectedAttributeName(string attributeName)
		{
			if (!attributeName.Contains("Attribute") || attributeName.Contains("Attributes"))
				return attributeName += "Attribute";
			return attributeName;
		}

		public bool HasAttribute<TResult>(string attributeName)
		{
			try
			{
				var ret = metadataAttributes.FirstOrDefault(a => a.AttributeName == ReturnCorrectedAttributeName(attributeName));

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public bool HasParameter<TResult>(string attributeName, string parameterName)
		{
			try
			{
				var ret = metadataAttributes.FirstOrDefault(a => a.AttributeName == ReturnCorrectedAttributeName(attributeName)).Property[parameterName];

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public TResult GetValue<TResult>(string attributeName, string parameterName)
		{
			TResult ret = default(TResult);
			try
			{
				ret = (TResult)metadataAttributes.FirstOrDefault(a => a.AttributeName == ReturnCorrectedAttributeName(attributeName)).Property[parameterName];

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

			if (metadata != null)
			{
				IList<Attribute> teste1 = GetModelMetadataAttributes(metadata);
				//string name = metadata.PropertyName[parameterName];
			}

			return ret;
		}

		public static Dictionary<string, Dictionary<string, string>> GetChangelogs(ModelMetadata metadata)
		{
			if (metadata == null) return null;

			VersionAttribute versionAttribute = GetModelMetadataAttributes(metadata).OfType<VersionAttribute>().FirstOrDefault();
			if (versionAttribute != null)
			{
				Dictionary<string, Dictionary<string, string>> versionChangelog = new Dictionary<string, Dictionary<string, string>>();
				Dictionary<string, string> versions = new Dictionary<string, string>();

				versions.Add($"{versionAttribute.MajorVersion}.{versionAttribute.MinorVersion}.{versionAttribute.PathVersion}", versionAttribute.ChangeLog);
				versionChangelog.Add(metadata.PropertyName, versions);

				return versionChangelog;
			}

			return null;
		}
	}
}