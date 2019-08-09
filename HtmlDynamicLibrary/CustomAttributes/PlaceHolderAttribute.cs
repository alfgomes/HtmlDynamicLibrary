using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
	public class PlaceHolderAttribute : DynamicAttribute, IMetadataAware
	{
		public readonly string Text;

		public PlaceHolderAttribute(string text)
		{
			this.Text = text;
		}

		public void OnMetadataCreated(ModelMetadata metadata)
		{
			metadata.AdditionalValues["placeholder"] = Text;
		}
	}
}