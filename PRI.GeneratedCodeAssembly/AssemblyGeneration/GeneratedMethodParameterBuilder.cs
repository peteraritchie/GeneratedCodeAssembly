using System;
using System.CodeDom.Compiler;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	internal class GeneratedMethodParameterBuilder : BuilderBase, IGeneratedMethodParameterBuilder
	{
		private string _name;
		private Type _type = typeof(object);
		private readonly IGeneratedMethodBuilder _generatedMethodBuilder;

		public GeneratedMethodParameterBuilder(IGeneratedMethodBuilder generatedMethodBuilder)
		{
			_generatedMethodBuilder = generatedMethodBuilder;
		}

		public IGeneratedMethodParameterBuilder WithType(Type type)
		{
			_type = type;
			return this;
		}

		public IGeneratedMethodParameterBuilder WithName(string name)
		{
			_name = name;
			return this;
		}

		public IGeneratedMethodBuilder CommitParameter()
		{
			return _generatedMethodBuilder;
		}

		public Type Type => _type;

		protected override void ValidateInternal(Action<string> actionOnInvalid)
		{
			if (_type == null)
				actionOnInvalid("Parameter type cannot be null.");
			else if (_type == typeof(void))
				actionOnInvalid("Parameter type cannot be void.");
			using (var provider = CodeDomProvider.CreateProvider("C#"))
			{
				if (!provider.IsValidIdentifier(_name))
					actionOnInvalid($"{_name} is not a valid parameter name.");
			}
		}
	}
}