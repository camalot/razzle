using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace Razzle.Extensions {
	public static partial class RazzleExtensions {
		public static bool IsInNamespace(this Type type, string @namespace) {
			return type != null && !string.IsNullOrWhiteSpace(type.Namespace) && type.Namespace.Equals(@namespace, StringComparison.InvariantCultureIgnoreCase);
		}

		public static bool IsInChildNamespace(this Type type, string rootNamespace) {
			return type != null && !string.IsNullOrWhiteSpace(type.Namespace) && type.Namespace.StartsWith(rootNamespace, StringComparison.InvariantCultureIgnoreCase) && !type.Namespace.Equals(rootNamespace, StringComparison.InvariantCultureIgnoreCase);
		}

	}
}
