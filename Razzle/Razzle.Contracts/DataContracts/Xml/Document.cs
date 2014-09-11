using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Razzle.Contracts.DataContracts.Xml {
	[XmlRoot("doc")]
	public class Document {
		[XmlArrayItem("member")]
		[XmlArray("members")]
		public IList<Member> Members { get; set; }
	}
}
