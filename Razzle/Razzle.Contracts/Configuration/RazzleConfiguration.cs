using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Camalot.Common.Attributes;

namespace Razzle.Contracts.Configuration {
	[ConfigurationPath("razzle")]
	[XmlRoot("razzle")]
	public class RazzleConfiguration {
		[XmlElement("documentation")]
		public RazzleDocumentation Documentation { get; set; }
	}

	[XmlRoot("documentation")]
	public class RazzleDocumentation {
		public RazzleDocumentation() {
			NamespaceIgnores = new List<string>();
			Assemblies = new RazzleDocumentationAssemblyList();
		}
		[XmlElement("path")]
		public string Path { get; set; }
		[XmlArray("assemblies")]
		[XmlArrayItem("assembly")]
		public RazzleDocumentationAssemblyList Assemblies {get;set;}
		[XmlArray("ignores")]
		[XmlArrayItem("namespace")]
		public List<string> NamespaceIgnores { get; set; }
	}

	public class RazzleDocumentationAssembly {
		[XmlAttribute("name")]
		public string Name { get; set; }
		[XmlAttribute("namespace")]
		public string Namespace { get; set; }
	}

	public class RazzleDocumentationAssemblyList : List<RazzleDocumentationAssembly> {
		public RazzleDocumentationAssembly this[string name] {
			get {
				return this.First(m => m.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase) || m.Namespace.Equals(name, StringComparison.InvariantCultureIgnoreCase));
			}
		}
	}
}
