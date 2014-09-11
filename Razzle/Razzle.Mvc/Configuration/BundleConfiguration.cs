using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Optimization;
using Camalot.Common.Mvc.Extensions;

namespace Razzle.Mvc.Configuration {
	public class BundleConfiguration {
		public static void RegisterBundles(BundleCollection bundles) {
			bundles.LoadFromWebConfiguration();
		}
	}
}
