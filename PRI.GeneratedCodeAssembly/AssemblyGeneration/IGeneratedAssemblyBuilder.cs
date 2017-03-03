namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	public interface IGeneratedAssemblyBuilder : IBuilder
	{
		IGeneratedAssemblyBuilder WithAssemblyName(string assemblyName);

		IGeneratedAssemblyBuilder InDirectory(string directory);
		IGeneratedAssemblyBuilder InCurrentDirectory();
		IGeneratedAssemblyBuilder WithAssemblyInfo(string company, string productName, string version, string description);
		IGeneratedTypeBuilder WithClass(string name);
		void SaveAssembly();
		string Directory { get; }
		string AssemblyCompany { get; }
		string AssemblyProductName { get; }
		string AssemblyVersion { get; }
		string AssemblyDescription { get; }
		string Name { get; }
	}
}