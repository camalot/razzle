using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Razzle.Contracts.Services;
using Camalot.Common.Extensions;
using System.Configuration;
using System.Reflection;
using MoreLinq;
using Razzle.Contracts.DataContracts;
using Razzle.Extensions;
using System.IO;
using System.Security.AccessControl;
using System.ComponentModel;
using System.Web.Hosting;
using Camalot.Common.Configuration;
using Razzle.Contracts.Configuration;

namespace Razzle.Services {
	public class DocumentationService : IDocumentationService{
		public DocumentationService(IConfigurationReader configurationReader) {
			Configuration = configurationReader.Get<RazzleConfiguration>();
		}

		private RazzleConfiguration Configuration { get; set; }
		private AppDomain DocumentationDomain { get; set; }
		private IEnumerable<Assembly> DocumentationAssemblies {
			get {
				return GetDocumentationDomain().GetAssemblies().Where(a => !a.IsDynamic);
			}
		}

		private string DataDirectory {
			get {
				var path = Configuration.Documentation.Path;
				if(Path.IsPathRooted(path)) {
					return path;
				} else {
					return HostingEnvironment.MapPath(path);
				}
			}
		}

		private IEnumerable<string> NamespaceIgnoreList {
			get {
				return Configuration.Documentation.NamespaceIgnores;
			}
		}

		Namespace IDocumentationService.Build(string assemblyName) {
			if(string.IsNullOrWhiteSpace(assemblyName)) {
				return null;
			}

			var assembly = GetAssemblyDocumentationNames(assemblyName);

			if(assembly == null) {
				return null;
			}

			var rootNS = new Namespace {
				Name = assembly.Namespace,
			};
			var xml = LoadXml(assemblyName);
			// get the ignore list and lets always ignore ".Properties" namespace
			var NSIgnoreList = NamespaceIgnoreList.Union(new string[] { "{0}.Properties".With(assembly.Name) });

			DocumentationAssemblies.Where(a =>
					(
						a.GetName().Name.Equals(assembly.Namespace, StringComparison.InvariantCultureIgnoreCase) || 
						a.GetName().Name.Equals(assembly.Name, StringComparison.InvariantCultureIgnoreCase)
					)
				).ForEach(a => {
					a.GetTypes()
						.Where(t => ((t.IsInNamespace(assembly.Namespace) || t.IsInNamespace(assembly.Name)) || (t.IsInChildNamespace(assembly.Namespace) || t.IsInChildNamespace(assembly.Name))) && !NSIgnoreList.Contains(t.Namespace))
						.Select(t => t.Namespace)
						.Distinct()
						.ForEach(ns => {
							if(string.IsNullOrWhiteSpace(rootNS.AssemblyVersion)) {
								rootNS.AssemblyVersion = a.GetName().Version.ToString();
							}
							var processedNS = ProcessNamespace(ns, xml);
							if(processedNS != null && processedNS.Classes.Count > 0) {
								rootNS.Namespaces.Add(processedNS);
							}
						});
				});
			return rootNS;
		}


		/// <summary>
		/// Gets the documentation domain.
		/// </summary>
		/// <returns></returns>
		private AppDomain GetDocumentationDomain() {
			if(DocumentationDomain == null) {
				// create a new app domain for processing.
				DocumentationDomain = AppDomain.CurrentDomain; // AppDomain.CreateDomain("Documentation");
				var dir = new DirectoryInfo(DataDirectory);
				dir.GetFiles("*.dll").ForEach(a => {
					DocumentationDomain.Load(AssemblyName.GetAssemblyName(a.FullName));
				});
			}
			return DocumentationDomain;
		}

		/// <summary>
		/// Loads the XML.
		/// </summary>
		/// <param name="assemblyName">Name of the assembly.</param>
		/// <returns></returns>
		/// <exception cref="System.IO.FileNotFoundException"></exception>
		private XmlDocument LoadXml(string assemblyName) {
			var file = DocumentationPathFromAssemblyName(assemblyName);
			if(file.Exists) {
				var doc = new XmlDocument();
				using(var fs = new FileStream(file.FullName, FileMode.Open, FileSystemRights.Read, FileShare.Read, 2048, FileOptions.None)) {
					doc.Load(fs);
				}
				return doc;
			} else {
				throw new FileNotFoundException();
			}
		}


		private FileInfo DocumentationPathFromAssemblyName(string assemblyName) {
			var assembly = GetAssemblyDocumentationNames(assemblyName);
			var fn = "{0}.xml".With(assembly.Name);
			var dir = DataDirectory;
			var file = new FileInfo(Path.Combine(dir, fn));
			if(file.Exists) {
				return file;
			} else {
				fn = "{0}.xml".With(assembly.Namespace);
				file = new FileInfo(Path.Combine(dir, fn));
				if(file.Exists) {
					return file;
				} else {
					throw new FileNotFoundException();
				}
			}
		}

		private RazzleDocumentationAssembly GetAssemblyDocumentationNames(string namespaceName) {
			var rda = this.Configuration.Documentation.Assemblies[namespaceName];
			return rda;
		}

		/// <summary>
		/// Processes the namespace.
		/// </summary>
		/// <param name="namespace">The namespace.</param>
		/// <param name="xml">The XML.</param>
		/// <returns></returns>
		private Namespace ProcessNamespace(string @namespace, XmlDocument xml) {
			var nm = new Namespace {
				Name = @namespace
			};
			DocumentationAssemblies.ForEach(a => {
				var classes = new List<Class>();
				a.GetTypes().Where(t => t.IsPublic && t.IsClass && t.IsInNamespace(@namespace)).ForEach(t => {
					var pclass = ProcessType(t, xml);
					nm.Classes.Add(pclass);
				});
			});

			return nm;
		}

		/// <summary>
		/// Processes the type.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="xml">The XML.</param>
		/// <returns></returns>
		private Class ProcessType(System.Type type, XmlDocument xml) {
			return new Class {
				Name = type.ToSafeName(),
				Namespace = type.Namespace,
				Assembly = type.Assembly.GetName().Name,
				Description = type.GetCustomAttributeValue<DescriptionAttribute, String>(x => x.Description).Or(""),
				XmlName = type.GetXmlDocumentationName(),
				Documentation = xml.GetDocumenation(type),
				Methods = ProcessMethods(type, xml),
				IsStatic = type.IsAbstract && type.IsSealed,
				Properties = ProcessProperties(type, xml)
			};
		}

		private IList<Property> ProcessProperties(System.Type type, XmlDocument xml) {
			return type.GetProperties(System.Reflection.BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.DeclaredOnly)
				.Where(m => true).Select(m => new Property {
					Name = m.Name,
					Documentation = xml.GetDocumenation(m),
					XmlName = m.GetXmlDocumentationName(),
					IsStatic = m.GetSetMethod() != null ? m.GetSetMethod().IsStatic : m.GetGetMethod() != null ? m.GetGetMethod().IsStatic : false,
					IsReadOnly = m.GetSetMethod() == null,
					Parent = type,
					ReturnType = m.PropertyType
				}).OrderBy(x => x.Name).ToList();
		}

		/// <summary>
		/// Processes the methods.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="xml">The XML.</param>
		/// <returns></returns>
		private IList<Method> ProcessMethods(System.Type type, XmlDocument xml) {

			return type.GetMethods(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.DeclaredOnly).Where(m =>
				!m.IsConstructor &&
				!m.Name.StartsWith("get_", StringComparison.CurrentCulture) &&
				!m.Name.StartsWith("set_", StringComparison.CurrentCulture) &&
				!m.Name.StartsWith("add_", StringComparison.CurrentCulture) &&
				!m.Name.StartsWith("remove_", StringComparison.CurrentCulture) &&
					// exclude overrides because I don't care about them. Unless, base definition is in this assembly.
				(m.GetBaseDefinition() == null || m.GetBaseDefinition() == m || m.GetBaseDefinition().DeclaringType.Assembly == type.Assembly)
				).Select(m => new Method {
					Name = m.Name,
					Documentation = xml.GetDocumenation(m),
					XmlName = m.GetXmlDocumentationName(),
					GenericParameters = ProcessMethodGenericParameters(m),
					Parameters = ProcessParams(m),
					ExtensionOf = m.ExtensionOf(),
					Parent = type,
					IsStatic = m.IsStatic,
					ReturnType = m.ReturnType
				}).OrderBy(x => x.Name).ThenBy(x => x.ExtensionOf == null ? "" : x.ExtensionOf.ToSafeFullName()).ThenBy(x => x.Parameters.Count).ToList();
		}

		/// <summary>
		/// Processes the method generic parameters.
		/// </summary>
		/// <param name="m">The m.</param>
		/// <returns></returns>
		private IList<Contracts.DataContracts.Type> ProcessMethodGenericParameters(System.Reflection.MethodInfo m) {
			if(m.IsGenericMethod) {
				return m.GetGenericArguments().Select(t => new Contracts.DataContracts.Type { BaseType = t, Name = t.ToSafeName() }).ToList();
			} else {
				return default(IList<Contracts.DataContracts.Type>);
			}
		}

		/// <summary>
		/// Processes the parameters.
		/// </summary>
		/// <param name="m">The m.</param>
		/// <returns></returns>
		private IList<Parameter> ProcessParams(System.Reflection.MethodInfo m) {
			return m.GetParameters().Select(p => new Parameter {
				Type = new Contracts.DataContracts.Type { BaseType = p.ParameterType, Name = p.ParameterType.ToSafeName() },
				Name = p.Name,
				IsOut = p.IsOut,
				IsIn = p.IsIn,
				IsOptional = p.IsOptional
			}).ToList();
		}
	}
}
