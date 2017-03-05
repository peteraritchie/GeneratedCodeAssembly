namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	public interface IGeneratedTypeBuilder : IBuilder
	{
		IGeneratedTypeBuilder Public();
		IGeneratedTypeBuilder Private();
		IGeneratedAssemblyBuilder CommitType();
		IGeneratedTypeBuilder ImplementsInterface<T>();
		IGeneratedMethodBuilder WithMethod(string name);
		IGeneratedTypeBuilder InNamespace(string @namespace);
	}
}