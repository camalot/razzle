using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camalot.Common.Extensions;

namespace Razzle.Extensions {
	public static partial class RazzleContractsExtensions {

		public static string ToSafeName(this Type type) {
			if(type.IsGenericType) {
				var gparams = type.GetGenericArguments();
				var len = type.Name.IndexOf("`");
				if(len <= 0) {
					len = type.Name.Length;
				}
				return "{0}{1}".With(gparams.Count() > 0 ? type.Name.Substring(0, len) : type.Name, gparams.Count() > 0 ? "<{0}>".With(String.Join(", ", gparams.Select(g => g.ToSafeName()))) : "");
			} else {
				return type.Name;
			}
		}



		public static string ToSafeFullName(this Type type) {
			return "{1}.{0}".With(ToSafeName(type), type.Namespace);
		}

		public static string ToSafeUrlFullName(this Type type) {
			return "{1}.{0}".With(type.Name.REReplace(@"\[|\]", ""), type.Namespace);
		}
	}
}
