using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Razzle.Contracts.Services;
using Camalot.Common.Extensions;
using Razzle.Mvc.Extensions;
using Razzle.Extensions;

namespace Razzle.Mvc.Controllers {
	public class DocumentationController : Controller {

		public DocumentationController( IDocumentationService documentationService) {
			DocumentationService = documentationService.Require();
		}

		private IDocumentationService DocumentationService { get; set; }

		public ActionResult Assembly(String id) {
			var model = DocumentationService.Build(id);
			
			return View(viewName: this.ControllerContext.AssemblyView(id), model: model);
		}

		public PartialViewResult Navigation(string id) {
			var model = DocumentationService.Build(id);
			return this.PartialView(model);
		}
	}
}
