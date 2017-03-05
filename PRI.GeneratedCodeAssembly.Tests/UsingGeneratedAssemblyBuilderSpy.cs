using Xunit;

namespace PRI.GeneratedCodeAssembly.Tests
{
	public class UsingGeneratedAssemblyBuilderSpy
	{
		[Fact]
		public void GeneratedAssemblyBuilderSpyGeneratesCorrectAssemblyPath()
		{
			var spy = new GeneratedAssemblyBuilderSpy();
			spy.WithAssemblyName("MyAssembly").InDirectory("MyDirectory");

			Assert.Equal(@"MyDirectory\MyAssembly.dll", spy.AssemblyPath);
		}
	}
}