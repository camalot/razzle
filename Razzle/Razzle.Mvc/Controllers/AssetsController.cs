using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Camalot.Common.Extensions;
using Razzle.Mvc.Models;

namespace Razzle.Mvc.Controllers {
	public class AssetsController : Controller {
		[ChildActionOnly]
		public PartialViewResult Gist(string id, bool allowCopy = true) {
			return this.PartialView(new GistModel {
				Id = id.Require(),
				AllowCopy = allowCopy
			});
		}

		public PartialViewResult Adsense() {
			return this.PartialView(new AdSenseModel {
				PublisherId = "ca-pub-9157593394596390",
				Slot = "4102865437",
				Name = "responsivewin8"
			});
		}
	}
}
