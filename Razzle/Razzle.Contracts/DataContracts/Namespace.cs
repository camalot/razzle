using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace Razzle.Contracts.DataContracts {
	public class Namespace {
		public Namespace() {
			Classes = new List<Class>();
			Namespaces = new List<Namespace>();
		}
		public string Name { get; set; }
		public IList<Namespace> Namespaces { get; set; }
		public IList<Class> Classes { get; set; }
		public bool Ignore { get { return Classes.Where(c => !c.Ignore).Count() == 0 && Namespaces.Where(c => !c.Ignore).Count() == 0; } }
		public String AssemblyVersion { get; set; }

		public string Id {
			get {
				return Name.Slug();
			}
		}

		public override string ToString() {
			return this.Name;
		}
	}
}
