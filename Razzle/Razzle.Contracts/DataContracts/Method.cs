using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;
using Razzle.Extensions;

namespace Razzle.Contracts.DataContracts {
	public class Method {
		public Method() {
			Parameters = new List<Parameter>();
			GenericParameters = new List<DataContracts.Type>();
		}
		public string Name { get; set; }
		public Xml.Member Documentation { get; set; }
		public string XmlName { get; set; }
		public bool Ignore { get { return Documentation != null && Documentation.Ignore; } }
		public System.Type ReturnType { get; set; }
		public System.Type Parent { get; set; }
		public IList<Parameter> Parameters { get; set; }
		public IList<DataContracts.Type> GenericParameters { get; set; }
		public bool IsStatic { get; set; }
		public bool IsExtension {
			get {
				return ExtensionOf != null;
			}
		}
		public System.Type ExtensionOf { get; set; }

		public string Id {
			get {
				return "{0}.{1}".With(Parent.ToSafeFullName(), Name).Slug();
			}
		}

		public override string ToString() {
			if(ExtensionOf == null) {
				return "{0}{2} ( {1} )".With(Name, String.Join(", ", Parameters.Select(p => p.ToString())), GenericParameters != null && GenericParameters.Count > 0 ? "<{0}>".With(String.Join(", ", GenericParameters.Select(g => g.ToString()))) : "");
			} else {
				return "{0}{2} ( {1} )".With(
					Name,
					String.Join(", ", Parameters.Skip(1).Select(p => p.ToString())),
					GenericParameters != null && GenericParameters.Count > 0 ? "<{0}>".With(String.Join(", ", GenericParameters.Select(g => g.ToString()))) : ""
				);
			}
		}
	}
}
