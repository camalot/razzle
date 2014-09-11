using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Razzle.Contracts.DataContracts.Xml {
	public class Reference {
		[XmlAttribute("cref")]
		public string Link { get; set; }
		[XmlText]
		public string Description { get; set; }
	}
}
