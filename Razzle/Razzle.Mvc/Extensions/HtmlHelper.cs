using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Camalot.Common.Extensions;
using Camalot.Common.Mvc.Extensions;
using Razzle.Extensions;
using Razzle.Mvc.Models;
using MoreLinq;
using Razzle.Mvc.Castle;
using Camalot.Common.Configuration;
using Razzle.Contracts.Configuration;

namespace Razzle.Mvc.Extensions {
	public static partial class RazzleMvcExtensions {
		public static IHtmlString SiteName(this HtmlHelper helper) {
			return SiteSetting(helper, "Name");
		}

		public static IHtmlString SiteDescription(this HtmlHelper helper) {
			return SiteSetting(helper, "Description");
		}

		public static IHtmlString SiteSetting(this HtmlHelper helper, string name) {
			var value = ConfigurationManager.AppSettings["site:{0}".With(name.Require())];
			return new MvcHtmlString(value.Require());
		}

		public static IHtmlString DirectLink(this HtmlHelper helper, string id) {
			return helper.Partial("_DirectLink", id);
		}

		public static IHtmlString StaticIcon(this HtmlHelper helper, bool isStatic) {
			return helper.Icon("fa-flag", isStatic, "Static");
		}

		public static IHtmlString ReadonlyIcon(this HtmlHelper helper, bool isReadonly) {
			return helper.Icon("fa-lock", isReadonly, "Readonly");
		}

		public static IHtmlString ExtensionIcon(this HtmlHelper helper, bool isExtension) {
			return helper.Icon("fa-plug", isExtension, "Extension");
		}

		public static IHtmlString Icon(this HtmlHelper helper, string icon, bool show = true, string title = "") {
			return helper.Partial("_IconPartial", new IconHelperModel {
				Show = show,
				Title = title,
				Icon = icon
			});
		}
		public static void RenderNavigation(this HtmlHelper helper) {
			var config = Resolver.Resolve<IConfigurationReader>().Get<RazzleConfiguration>();
			var dataDir = new DirectoryInfo(HostingEnvironment.MapPath(config.Documentation.Path));
			dataDir.GetFiles("*.dll").ForEach(f => {
				var name = f.NameWithoutExtension();
				helper.RenderAction("Navigation", new { controller = "Documentation", id = f.NameWithoutExtension() });
			});

		}
	}
}
