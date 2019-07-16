using System.Web.Mvc;

namespace HtmlDynamicLibrary.ExtensionMethods
{
	public static class TagBuilderExtensions
	{
		public static void MergeAttributeValues(this TagBuilder @self, string key, string value, bool replaceExisting = false)
		{
			if (@self.Attributes.ContainsKey(key))
			{
				if (replaceExisting)
					@self.Attributes[key] += $" {value}";
			}
			else
				@self.Attributes.Add(key, value);
		}
	}
}