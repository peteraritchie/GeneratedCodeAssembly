using System;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	public interface IGeneratedMethodParameterBuilder : IBuilder
	{
		IGeneratedMethodParameterBuilder WithType(Type type);
		IGeneratedMethodParameterBuilder WithName(string name);
		IGeneratedMethodBuilder CommitParameter();
		Type Type { get; }
	}
}