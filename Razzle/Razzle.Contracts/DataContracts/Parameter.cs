using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace Razzle.Contracts.DataContracts {
	public class Parameter {
		public string Name { get; set; }
		public DataContracts.Type Type { get; set; }
		public bool IsOut { get; set; }
		public bool IsIn { get; set; }
		public bool IsOptional { get; set; }

		public override string ToString() {
			return "{4}{2}{3}{0} {1}{5}".With(Type.ToString(), Name, IsOut ? "{out}" : "", IsIn ? "{in}" : "", IsOptional ? "[" : "", IsOptional ? "]" : "");
		}
	}
}
