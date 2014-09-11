using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Razzle.Contracts.DataContracts;

namespace Razzle.Contracts.Services {
	public interface IDocumentationService {

		Namespace Build(string assemblyName);

	}
}
