using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.ComponentModel.DataAnnotations
{
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Delegate | AttributeTargets.Enum
		| AttributeTargets.Interface | AttributeTargets.Module | AttributeTargets.Struct, AllowMultiple = true, Inherited = false)]
	public class VersionAttribute : DynamicAttribute
	{
		/// <summary>
		/// When you make incompatible API changes
		/// </summary>
		public int MajorVersion { get; private set; }
		/// <summary>
		/// When you add functionality in a backwards-compatible manner
		/// </summary>
		public int MinorVersion { get; private set; }
		/// <summary>
		/// When you make backwards-compatible bug fixes
		/// </summary>
		public int PathVersion { get; private set; }
		public DateTime DateVersion { get; private set; }
		public string ChangeLog { get; set; }

		public VersionAttribute(int major, int minor, int path, string dateVersion, string changelog)
			: base()
		{
			this.MajorVersion = major;
			this.MinorVersion = minor;
			this.PathVersion = path;
			DateTime parsedDate;
			DateTime.TryParse(dateVersion, out parsedDate);
			this.DateVersion = parsedDate;
			this.ChangeLog = changelog;
		}
	}
}