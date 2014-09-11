using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MoreLinq;
using System.Linq;
using System;

namespace Razzle.Mvc.Castle.Installers {

	public class ControllersInstaller : IWindsorInstaller {
		public void Install(IWindsorContainer container, IConfigurationStore store) {
			AppDomain.CurrentDomain.GetAssemblies().Where(a => !a.IsDynamic).ForEach(a => container.Register(Classes.FromAssembly(a)
		.BasedOn<IController>()
		.If(m => m.Name.EndsWith("Controller"))
		.LifestyleTransient()));



			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
		}
	}

	public class ServicesInstaller : IWindsorInstaller {
		public void Install(IWindsorContainer container, IConfigurationStore store) {
			container.Register(Classes.FromAssemblyNamed("Razzle.Services")
				.Where(Component.IsInNamespace("Razzle.Services"))
				.WithService.AllInterfaces().LifestyleTransient());

			container.Register(Classes.FromAssemblyNamed("Camalot.Common")
				.Where(Component.IsInNamespace("Camalot.Common.Configuration"))
				.WithService.AllInterfaces().LifestyleTransient());
		}
	}
}