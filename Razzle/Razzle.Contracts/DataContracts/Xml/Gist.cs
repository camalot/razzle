using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Razzle.Contracts.DataContracts.Xml {
	public class Gist {
		[XmlAttribute("id")]
		public string Id { get; set; }
		[XmlText]
		public string Description { get; set; }
	}
}
