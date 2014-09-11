using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Camalot.Common.Extensions;

namespace Razzle.Contracts.Configuration.Handlers {
	public class RazzleSectionHandler : IConfigurationSectionHandler {
		/// <summary>
		/// Creates the bundle configuration from the configContext.
		/// </summary>
		/// <param name="parent">The parent.</param>
		/// <param name="configContext">The configuration context.</param>
		/// <param name="section">The section.</param>
		/// <returns></returns>
		/// <ignore>true</ignore>
		public RazzleConfiguration Create(object parent, object configContext, XmlNode section) {
			return section.CreateConfigurationInstanceFromConfigurationNode<RazzleConfiguration>();
		}

		#region IConfigurationSectionHandler Members

		/// <summary>
		/// Creates a configuration section handler.
		/// </summary>
		/// <param name="parent"></param>
		/// <param name="configContext">Configuration context object.</param>
		/// <param name="section"></param>
		/// <returns>The created section handler object.</returns>
		object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section) {
			return this.Create(parent, configContext, (XmlElement)section);
		}

		#endregion
	}

}
