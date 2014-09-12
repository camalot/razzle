using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Razzle.Mvc.Models {
	public class GistModel {
		public GistModel() {
			AllowCopy = true;
		}
		public string Id { get; set; }
		public bool AllowCopy { get; set; }
	}
}
