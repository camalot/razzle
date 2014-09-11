﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Razzle.Contracts.DataContracts.Xml {
	public class Param {
		[XmlAttribute("name")]
		public string Name { get; set; }
		[XmlText]
		public string Value { get; set; }
	}
}
