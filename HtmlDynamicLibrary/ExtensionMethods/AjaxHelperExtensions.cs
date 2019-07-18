using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;

namespace HtmlDynamicLibrary.ExtensionMethods
{
	public static class AjaxHelperExtensions
	{
		public static MvcHtmlString ModalDialogActionLink(this AjaxHelper @self, string linkText, string actionName, string controllerName, string dialogTitle, RouteValueDictionary routeValues, IDictionary<string, object> htmlAttributes, string onSubmitJsCallback)
		{
			if (routeValues == null)
				routeValues = new RouteValueDictionary();

			var dialogDivId = Guid.NewGuid().ToString();
			return @self.ActionLink(
				linkText,
				actionName,
				controllerName,
				routeValues: routeValues,
				htmlAttributes: htmlAttributes,
				ajaxOptions: new AjaxOptions {
								UpdateTargetId = dialogDivId,
								InsertionMode = InsertionMode.Replace,
								HttpMethod = "GET",
								OnBegin = string.Format(CultureInfo.InvariantCulture, $"prepareModalDialog('{dialogDivId}')"),
								OnFailure = string.Format(CultureInfo.InvariantCulture, $"clearModalDialog('{dialogDivId}'); alert('Ajax call failed')"),
								OnSuccess = string.Format(CultureInfo.InvariantCulture, $"openModalDialog('{dialogDivId}', '{dialogTitle}', {onSubmitJsCallback})")
				});
		}
	}
}