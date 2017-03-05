using System;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	public interface IGeneratedMethodBuilder : IBuilder
	{
		IGeneratedMethodBuilder WithName(string name);
		IGeneratedMethodBuilder WithReturnType(Type returnType);
		IGeneratedMethodBuilder WithGenericReturnType(string typeParameterName);
		IGeneratedTypeBuilder CommitMethod();
		IGeneratedMethodBuilder WithInstruction(IIlInstruction instruction);
		IGeneratedMethodBuilder WithLocal(Type type);
		IGeneratedMethodParameterBuilder WithParameter();
		IGeneratedMethodParameterBuilder WithParameter(string name);
		IGenerateMethodGenericParameterBuilder WithGenericParameter(string name);
	}
}