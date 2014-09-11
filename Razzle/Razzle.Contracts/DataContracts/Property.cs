using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;
using Razzle.Extensions;

namespace Razzle.Contracts.DataContracts {
	public class Property {
		public string Name { get; set; }
		public Xml.Member Documentation { get; set; }
		public string XmlName { get; set; }
		public bool Ignore { get { return Documentation != null && Documentation.Ignore; } }
		public System.Type ReturnType { get; set; }
		public System.Type Parent { get; set; }
		public bool IsStatic { get; set; }
		public bool IsReadOnly { get; set; }
		public string Id {
			get {
				return "{0}.{1}".With(Parent.ToSafeFullName(), Name).Slug();
			}
		}

		public override string ToString() {
			return Name;
		}
	}
}
