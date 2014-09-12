using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Razzle.Mvc.Configuration {
	public class RouteConfiguration {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
					name: "Documentation_Default",
					url: "documentation/{action}/{*id}",
					defaults: new { controller = "Documentation", action = "Assembly", id = UrlParameter.Optional }
			);

			routes.MapRoute(
					name: "Default",
					url: "{controller}/{action}/{id}",
					defaults: new { controller = "Documentation", action = "Assembly", id = UrlParameter.Optional }
			);
		}
	}
}
