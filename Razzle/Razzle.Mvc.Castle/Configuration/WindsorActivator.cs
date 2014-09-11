using System;
using Castle.Windsor;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(Razzle.Mvc.Castle.Configuration.WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethodAttribute(typeof(Razzle.Mvc.Castle.Configuration.WindsorActivator), "Shutdown")]

namespace Razzle.Mvc.Castle.Configuration {
	public static class WindsorActivator {
		static ContainerBootstrapper bootstrapper;

		public static void PreStart() {
			bootstrapper = ContainerBootstrapper.Bootstrap();
		}

		public static void Shutdown() {
			if(bootstrapper != null)
				bootstrapper.Dispose();
		}

		internal static IWindsorContainer Container {
			get {
				if(bootstrapper == null) { return null; }
				return bootstrapper.Container;
			}
		}
	}
}