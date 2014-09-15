using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Camalot.Common.Extensions;
using Camalot.Common.Mvc.Attributes;
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

		public ActionResult Logo(int id, bool light = false) {
			//var sizes = new int[] { 152, 144, 120, 114, 72, 57, 32, 16 };
			//if(!sizes.Contains(id)) {
			//	return this.HttpNotFound();
			//}
			this.Response.ContentType = "image/svg+xml";
			return this.PartialView(new LogoModel {
				Color = light ? "ffffff" : "000000",
				Size = id.RequireBetween(0,512)
			});
		}
	}
}
