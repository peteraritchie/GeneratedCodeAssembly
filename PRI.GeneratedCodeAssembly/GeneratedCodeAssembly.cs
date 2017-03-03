using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using PRI.GeneratedCodeAssembly.AssemblyGeneration;

[assembly: InternalsVisibleTo("PRI.GeneratedCodeAssembly.Tests")]

namespace PRI.GeneratedCodeAssembly
{
	public class GeneratedCodeAssembly
	{
		private readonly GeneratedAssemblyBuilderSpy _builderSpy;

		public GeneratedCodeAssembly(Action<IGeneratedAssemblyBuilder> builderAction)
		{
			if (builderAction == null)
				throw new ArgumentNullException(nameof(builderAction));
			_builderSpy = new GeneratedAssemblyBuilderSpy();
			builderAction(_builderSpy);
		}

		public T CreateInstance<T>(string typeName)
		{
			var assemblyPath = _builderSpy.AssemblyPath;
			if (!File.Exists(assemblyPath))
			{
				_builderSpy.ReallySaveAssembly();
			}
			var assembly = Assembly.ReflectionOnlyLoadFrom(assemblyPath);
			var type = Type.GetType($"{typeName}, {assembly.FullName}");
			if(type == null)
				throw new InvalidOperationException($"Type {typeName} could not be found in assembly {assemblyPath}");
			var instance = Activator.CreateInstance(type);
			if (!(instance is T))
				throw new InvalidOperationException($"Type {typeName} is not of type {typeof(T).FullName}");
			return (T)instance;
		}
	}

	[ExcludeFromCodeCoverage]
	internal class GeneratedAssemblyBuilderSpy : IGeneratedAssemblyBuilder
	{
		private readonly GeneratedAssemblyBuilder _wrappedBuilder = new GeneratedAssemblyBuilder();
		public string AssemblyPath => Path.Combine(Directory, Name + ".dll");

		public void Validate()
		{
			// Method intentionally left empty.
		}

		public bool TryValidate(ICollection<ValidationResult> validationResults)
		{
			return true;
		}

		public IGeneratedAssemblyBuilder WithAssemblyName(string assemblyName)
		{
			_wrappedBuilder.WithAssemblyName(assemblyName);
			return this;
		}

		public IGeneratedAssemblyBuilder InDirectory(string directory)
		{
			_wrappedBuilder.InDirectory(directory);
			return this;
		}

		public IGeneratedAssemblyBuilder InCurrentDirectory()
		{
			_wrappedBuilder.InCurrentDirectory();
			return this;
		}

		public IGeneratedAssemblyBuilder WithAssemblyInfo(string company, string productName, string version, string description)
		{
			_wrappedBuilder.WithAssemblyInfo(company, productName, version, description);
			return this;
		}

		public IGeneratedTypeBuilder WithClass(string name) => _wrappedBuilder.WithClass(name);

		public void SaveAssembly()
		{
			// ignore in this implementation
		}

		public void ReallySaveAssembly() => _wrappedBuilder.SaveAssembly();

		public string Directory => _wrappedBuilder.Directory;
		public string AssemblyCompany => _wrappedBuilder.AssemblyCompany;
		public string AssemblyProductName => _wrappedBuilder.AssemblyProductName;
		public string AssemblyVersion => _wrappedBuilder.AssemblyVersion;
		public string AssemblyDescription => _wrappedBuilder.AssemblyDescription;
		public string Name => _wrappedBuilder.Name;
	}
}