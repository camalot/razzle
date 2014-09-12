using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace Razzle.Extensions {
	public static partial class RazzleExtensions {
		public static string ToRazzleAssemblyViewName(this string s) {
			return s.REReplace(@"(\.|\-|\+|\%|\@)",string.Empty);
		}
	}
}
