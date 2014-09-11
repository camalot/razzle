using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Razzle.Mvc.Configuration;

namespace Razzle {
	public class MvcApplication : System.Web.HttpApplication {
		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfiguration.RegisterRoutes(RouteTable.Routes);
			BundleConfiguration.RegisterBundles(BundleTable.Bundles);
		}
	}
}
