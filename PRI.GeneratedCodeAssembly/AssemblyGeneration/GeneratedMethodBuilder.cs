using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	internal class GeneratedMethodBuilder : BuilderBase, IGeneratedMethodBuilder
	{
		private string _name;
		private Type _returnType = typeof(void);
		private readonly List<IGeneratedMethodParameterBuilder> _parameters;
		private readonly IGeneratedTypeBuilder _generatedTypeBuilder;
		private readonly List<IIlInstruction> _ilInstructions = new List<IIlInstruction>();
		private readonly List<Type> _localTypes = new List<Type>();
		private readonly List<GenerateMethodGenericParameterBuilder> _genericParameters = new List<GenerateMethodGenericParameterBuilder>();
		private string _genericReturnTypeParameterName;

		public GeneratedMethodBuilder(IGeneratedTypeBuilder generatedTypeBuilder)
		{
			_generatedTypeBuilder = generatedTypeBuilder;
			_parameters = new List<IGeneratedMethodParameterBuilder>();
		}

		public IGeneratedMethodBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public IGeneratedTypeBuilder CommitMethod()
		{
			return _generatedTypeBuilder;
		}

		public IGeneratedMethodBuilder WithReturnType(Type returnType)
		{
			_returnType = returnType;
			return this;
		}

		public IGeneratedMethodParameterBuilder WithParameter()
		{
			var builder = new GeneratedMethodParameterBuilder(this);
			_parameters.Add(builder);
			return builder;
		}

		public IGeneratedMethodParameterBuilder WithParameter(string name)
		{
			var builder = WithParameter();
			builder.WithName(name);
			return builder;
		}

		internal void DefineMethod(TypeBuilder typeBuilder)
		{
			MethodBuilder methodBuilder;
			var methodInterface = typeBuilder.GetInterfaces().FirstOrDefault(i => i.GetMethods().Any(m => m.Name == _name));
			if (methodInterface != null)
				methodBuilder = typeBuilder.DefineMethod(_name, MethodAttributes.Final | MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot,
					_returnType, _parameters.Select(e => e.Type).ToArray());
			else
				methodBuilder = typeBuilder.DefinePublicMethod(_name, _returnType, _parameters.Select(e => e.Type).ToArray());
			Type returnType = null;

			if (_genericParameters.Any())
			{
				var genericParameterNames = _genericParameters.Select(e => e.Name).ToArray();
				var genericParameters = methodBuilder.DefineGenericParameters(genericParameterNames);
				foreach (var param in genericParameters)
				{
					if (!string.IsNullOrWhiteSpace(_genericReturnTypeParameterName) && param.Name == _genericReturnTypeParameterName)
					{
						returnType = param;
					}
				}
			}
			if(returnType != null)
					methodBuilder.SetReturnType(returnType);
			// specifically done after SetReturnType
			if (methodInterface != null)
				typeBuilder.DefineMethodOverride(methodBuilder, methodInterface.GetMethod(_name));
			// TODO:
			var ilGenerator = methodBuilder.GetILGenerator();
			foreach (var type in _localTypes)
				ilGenerator.DeclareLocal(type);
			foreach(var instruction in _ilInstructions)
				instruction.Emit(ilGenerator);
		}

		protected override void ValidateInternal(Action<string> actionOnInvalid)
		{
			if (_returnType == null && string.IsNullOrWhiteSpace(_genericReturnTypeParameterName))
				actionOnInvalid("ReturnType is null.");
			if (!string.IsNullOrWhiteSpace(_genericReturnTypeParameterName))
			{
				if (_genericParameters.All(e => e.Name != _genericReturnTypeParameterName))
				{
					actionOnInvalid(
						$"Generic return type name {_genericReturnTypeParameterName} not specified in method generic parameters.");
				}
			}
			if (!_ilInstructions.Any())
				actionOnInvalid("Method requires instructions, none were provided.");
			if (_ilInstructions.Any(e => e == null))
				actionOnInvalid("Null instruction is not supported.");
			if (_localTypes.Any(e => e == null))
				actionOnInvalid("Null local type is not supported.");
			if (_localTypes.Any(e => e == typeof(void)))
				actionOnInvalid("Void local type is not supported.");
			using (var provider = CodeDomProvider.CreateProvider("C#"))
			{
				if (!provider.IsValidIdentifier(_name))
					actionOnInvalid($"{_name} is not a valid method name.");
			}
			// TODO: check parameters
		}

		public IGeneratedMethodBuilder WithInstruction(IIlInstruction instruction)
		{
			_ilInstructions.Add(instruction);
			return this;
		}

		public IGeneratedMethodBuilder WithLocal(Type type)
		{
			_localTypes.Add(type);
			return this;
		}

		public IGenerateMethodGenericParameterBuilder WithGenericParameter(string name)
		{
			var result = new GenerateMethodGenericParameterBuilder(this, name);
			_genericParameters.Add(result);
			return result;
		}

		public IGeneratedMethodBuilder WithGenericReturnType(string typeParameterName)
		{
			_genericReturnTypeParameterName = typeParameterName;
			return this;
		}
	}
}