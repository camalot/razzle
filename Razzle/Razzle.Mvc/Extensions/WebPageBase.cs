using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.WebPages;
using Razzle.Contracts.DataContracts;
using Camalot.Common.Extensions;

namespace Razzle.Mvc.Extensions {
	public static partial class RazzleMvcExtensions {
		private static readonly string RazzleViewsBase = "~/Views/Shared/RazzleTemplates/{0}";

		public static HelperResult RenderDocumentation(this WebPageBase page, Namespace model) {
			if(model != null) {
				return page.RenderPage(RazzleViewsBase.With("Documentation/Index.cshtml"), model);
			} else {
				return new HelperResult(a => { });
			}
		}

		public static HelperResult RenderDocumentationNavigation(this WebPageBase page) {
			return page.RenderPage(RazzleViewsBase.With("Documentation/_AssemblyNavigationPartial.cshtml"));
		}

		public static HelperResult RenderTopNavigation(this WebPageBase page) {
			return page.RenderPage(RazzleViewsBase.With("_NavBarPartial.cshtml"));
		}

		public static HelperResult RenderUserNavigation(this WebPageBase page) {
			return page.RenderPage("~/Views/Shared/_NavigationPartial.cshtml");
		}

		public static HelperResult RenderSideNavigation(this WebPageBase page) {
			return page.RenderPage(RazzleViewsBase.With("_SideNavPartial.cshtml"));
		}

		public static HelperResult RenderPoweredBy(this WebPageBase page) {
			return page.RenderPage(RazzleViewsBase.With("_PoweredByPartial.cshtml"));
		}
	}

}
