using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	[ExcludeFromCodeCoverage]
	class GenerateMethodGenericParameterBuilder : BuilderBase, IGenerateMethodGenericParameterBuilder
	{
		private readonly IGeneratedMethodBuilder _generatedMethodBuilder;
		private bool _hasClassConstraint;
		private bool _hasNewConstraint;

		public GenerateMethodGenericParameterBuilder(IGeneratedMethodBuilder generatedMethodBuilder, string name)
		{
			_generatedMethodBuilder = generatedMethodBuilder;
			Name = name;
		}

		public string Name { get; }

		public IGenerateMethodGenericParameterBuilder WithNewConstraint()
		{
			_hasNewConstraint = true;
			return this;
		}

		public IGenerateMethodGenericParameterBuilder WithClassConstraint()
		{
			_hasClassConstraint = true;
			return this;
		}

		// TODO: and test coverage
		protected override void ValidateInternal(Action<string> actionOnInvalid)
		{
		}

		internal GenericParameterAttributes GetAttributes()
		{
			GenericParameterAttributes result = 0;
			if(_hasClassConstraint) result |= GenericParameterAttributes.ReferenceTypeConstraint;
			if (_hasNewConstraint) result |= GenericParameterAttributes.DefaultConstructorConstraint;
			return result;
		}

		public IGeneratedMethodBuilder CommitGenericParameter()
		{
			return _generatedMethodBuilder;
		}
	}
}