using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Razzle.Extensions;
using Camalot.Common.Extensions;

namespace Razzle.Mvc.Extensions {
	public static partial class RazzleMvcExtensions {

		public static string AssemblyView(this ControllerContext context, string name) {
			if(string.IsNullOrWhiteSpace(name)) {
				return "index";
			}
			var viewName = "~/Views/Documentation/Assemblies/{0}.cshtml".With(name.ToRazzleAssemblyViewName());
			var exists = context.ViewExists(viewName);
			if(exists) {
				return viewName;
			} else {
				return "index";
			}
		}

		public static bool PartialViewExists(this ControllerContext context, string viewName) {
			var result = ViewEngines.Engines.FindPartialView(context, viewName);
			return result.View != null;
		}

		public static bool ViewExists(this ControllerContext context, string viewName) {
			var result = ViewEngines.Engines.FindView(context, viewName, null);
			return result.View != null;
		}
	}
}
