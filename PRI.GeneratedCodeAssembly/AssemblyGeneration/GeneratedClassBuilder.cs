using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	internal class GeneratedClassBuilder : BuilderBase, IGeneratedTypeBuilder
	{
		private readonly IGeneratedAssemblyBuilder _generatedAssemblyBuilder;
		private readonly string _name;
		private TypeAttributes _typeVisibilityAttributes = TypeAttributes.Public;
		private string _namespace;
		private readonly ConstructorInfo _generatedCodeAttribute_ctor = typeof(GeneratedCodeAttribute).GetConstructor(new[] { typeof(string), typeof(string) });
		private List<Type> ImplementedInterfaces { get; }
		private List<GeneratedMethodBuilder> Methods { get; }

		public GeneratedClassBuilder(IGeneratedAssemblyBuilder generatedAssemblyBuilder, string name)
		{
			_generatedAssemblyBuilder = generatedAssemblyBuilder;
			_name = name;
			ImplementedInterfaces = new List<Type>();
			Methods = new List<GeneratedMethodBuilder>();
		}

		public IGeneratedMethodBuilder WithMethod(string name)
		{
			var generatedMethodBuilder = new GeneratedMethodBuilder(this);
			generatedMethodBuilder.WithName(name);
			Methods.Add(generatedMethodBuilder);
			return generatedMethodBuilder;
		}

		public IGeneratedTypeBuilder InNamespace(string @namespace)
		{
			_namespace = @namespace;
			return this;
		}

		public IGeneratedTypeBuilder Public()
		{
			_typeVisibilityAttributes = TypeAttributes.Public;
			return this;
		}

		public IGeneratedTypeBuilder Private()
		{
			_typeVisibilityAttributes = TypeAttributes.NotPublic;
			return this;
		}

		public IGeneratedTypeBuilder ImplementsInterface<T>()
		{
			ImplementedInterfaces.Add(typeof(T));
			return this;
		}

		public IGeneratedAssemblyBuilder CommitType()
		{
			return _generatedAssemblyBuilder;
		}

		internal void DefineType(ModuleBuilder moduleBuilder)
		{
			string fqtn;
			if (!string.IsNullOrWhiteSpace(_namespace))
			{
				fqtn = _namespace + '.' + _name;
			}
			else
			{
				fqtn = _name;
			}
			var typeBuilder = moduleBuilder.DefineType(fqtn, TypeAttributes.Class | _typeVisibilityAttributes);
			typeBuilder.SetCustomAttribute(
				new CustomAttributeBuilder(_generatedCodeAttribute_ctor,
					new object[] { BuilderExtensions.GetExecutingAssemblyName(), BuilderExtensions.GetExecutingAssemblyVersion() }));
			foreach (var type in ImplementedInterfaces)
				typeBuilder.AddInterfaceImplementation(type);
			foreach (var method in Methods)
				method.DefineMethod(typeBuilder);

			typeBuilder.CreateType();
		}

		protected override void ValidateInternal(Action<string> actionOnInvalid)
		{
			using (var provider = CodeDomProvider.CreateProvider("C#"))
			{
				if (!provider.IsValidIdentifier(_name))
					actionOnInvalid($"{_name} is not a valid class name.");
				if (!string.IsNullOrWhiteSpace(_namespace))
				{
					var subnames = _namespace.Split('.');
					if (subnames.Any(subname => !provider.IsValidIdentifier(subname)))
					{
						actionOnInvalid($"{_namespace} is not a valid namespace name.");
					}
				}
			}

			foreach (var type in ImplementedInterfaces)
			{
				if (!type.IsInterface)
					actionOnInvalid($"Type {type.FullName} is not an interface.");
			}

			foreach (var method in Methods)
			{
				var results = new List<ValidationResult>();
				method.TryValidate(results);
				foreach (var result in results)
				{
					actionOnInvalid(result.ErrorMessage);
				}
			}
		}
	}
}