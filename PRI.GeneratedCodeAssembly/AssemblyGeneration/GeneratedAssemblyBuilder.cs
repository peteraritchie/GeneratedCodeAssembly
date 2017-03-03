using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	internal class GeneratedAssemblyBuilder : BuilderBase, IGeneratedAssemblyBuilder
	{
		public string Directory { get; private set; }
		public string AssemblyCompany { get; private set; }
		public string AssemblyProductName { get; private set; }
		public string AssemblyVersion { get; private set; }
		public string AssemblyDescription { get; private set; }
		public string Name { get; private set; }

		public GeneratedAssemblyBuilder()
		{
			ClassBuilders = new List<GeneratedClassBuilder>();
		}

		public IGeneratedAssemblyBuilder WithAssemblyName(string assemblyName)
		{
			Name = assemblyName;
			return this;
		}

		public IGeneratedAssemblyBuilder WithUniqueAssemblyName()
		{
			Name = Guid.NewGuid().ToString("N");
			return this;
		}

		public IGeneratedAssemblyBuilder InDirectory(string directory)
		{
			Directory = directory;
			return this;
		}

		public IGeneratedAssemblyBuilder InCurrentDirectory()
		{
			Directory = @".\";
			return this;
		}

		public IGeneratedAssemblyBuilder WithAssemblyInfo(string company, string productName, string version, string description)
		{
			AssemblyCompany = company;
			AssemblyProductName = productName;
			AssemblyVersion = version;
			AssemblyDescription = description;
			return this;
		}

		public IGeneratedTypeBuilder WithClass(string name)
		{
			var generatedClassBuilder = new GeneratedClassBuilder(this, name);
			ClassBuilders.Add(generatedClassBuilder);

			return generatedClassBuilder;
		}

		private List<GeneratedClassBuilder> ClassBuilders { get; }

		public void SaveAssembly()
		{
			Validate();
			var assemblyName = new AssemblyName(Name);
			var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(assemblyName,
				AssemblyBuilderAccess.RunAndSave, Directory);
			assemblyBuilder.SetAssemblyInfo(AssemblyCompany,
				AssemblyProductName,
				AssemblyVersion,
				AssemblyDescription);
			var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName.Name, assemblyName.Name + ".dll");
			foreach (var classBuilder in ClassBuilders)
			{
				classBuilder.DefineType(moduleBuilder);
			}

			assemblyBuilder.Save(assemblyName.Name + ".dll");
		}

		protected override void ValidateInternal(Action<string> actionOnInvalid)
		{
			if (Directory == null)
				actionOnInvalid("Directory is null.");
			else
			{
				if (Directory.IndexOfAny(Path.GetInvalidPathChars()) != -1)
					actionOnInvalid($"Directory {Directory} contains invalid characters.");
				if (!System.IO.Directory.Exists(Directory))
					actionOnInvalid($"Directory {Directory} does not exist.");
			}
			if (Name == null)
				actionOnInvalid("Assembly name is null.");
			else
			{
				if (Name.IndexOfAny(Path.GetInvalidFileNameChars()) != -1)
					actionOnInvalid($"Assembly name {Name} contains invalid characters.");
			}
			foreach (var classBuilder in ClassBuilders)
			{
				var results = new List<ValidationResult>();
				classBuilder.TryValidate(results);
				foreach (var result in results)
				{
					actionOnInvalid(result.ErrorMessage);
				}
			}
		}
	}
}