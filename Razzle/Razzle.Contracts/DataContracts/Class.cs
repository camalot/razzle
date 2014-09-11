using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace Razzle.Contracts.DataContracts {
	public class Class {
		public Class() {
			Methods = new List<Method>();
			Properties = new List<Property>();
		}
		public string Name { get; set; }
		public string Description { get; set; }
		public string Namespace { get; set; }
		public string Assembly { get; set; }
		public Xml.Member Documentation { get; set; }
		public string XmlName { get; set; }
		public bool Ignore { get { return Documentation != null && Documentation.Ignore; } }
		public bool IsStatic { get; set; }

		public IList<Method> Methods { get; set; }

		public IList<Property> Properties { get; set; }

		public string Id {
			get {
				return "{0}.{1}".With(Namespace, Name).Slug();
			}
		}

		public override string ToString() {
			return "{0}.{1}".With(Namespace, Name);
		}
	}
}
