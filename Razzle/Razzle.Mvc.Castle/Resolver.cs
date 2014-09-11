using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Razzle.Mvc.Castle.Configuration;

namespace Razzle.Mvc.Castle {
	public static class Resolver {
		public static T Resolve<T>() {
			return WindsorActivator.Container.Resolve<T>();
		}

		public static T Resolve<T>(String key) {
			return WindsorActivator.Container.Resolve<T>(key);
		}

		public static T Resolve<T>(String key, object arguments) {
			return WindsorActivator.Container.Resolve<T>(key, arguments);
		}

		public static T Resolve<T>(object arguments) {
			return WindsorActivator.Container.Resolve<T>(arguments);
		}

		public static T Resolve<T>(IDictionary arguments) {
			return WindsorActivator.Container.Resolve<T>(arguments);
		}

		public static T Resolve<T>(String key, IDictionary arguments) {
			return WindsorActivator.Container.Resolve<T>(key, arguments);
		}
	}
}
