using System;
using System.IO;
using System.Reflection.Emit;
using PRI.GeneratedCodeAssembly.AssemblyGeneration;
using PRI.GeneratedCodeAssembly.Interfaces;
using PRI.GeneratedCodeAssembly.Tests.Stubs.DTO;
using Xunit;

namespace PRI.GeneratedCodeAssembly.Tests
{
	public class UsingGeneratedCodeAssembly
	{
		[Fact]
		public void AssemblyFileThatDoesNotExistIsGenerated()
		{
			var assemblyName = Guid.NewGuid().ToString("N");
			var fileName = $"{assemblyName}.dll";
			Assert.False(File.Exists(fileName));
#pragma warning disable S1481 // Unused local variables should be removed
			var generatedCodeAssembly = new GeneratedCodeAssembly(builder =>
#pragma warning restore S1481 // Unused local variables should be removed
			{
				builder.WithAssemblyName(assemblyName)
					.InCurrentDirectory()
					.WithClass("TestClassActivator").InNamespace("PRI.Activators")
					.ImplementsInterface<IActivator>()
					.WithMethod("Create")
					.WithReturnType(typeof(Customer))
					.WithGenericParameter("T").CommitGenericParameter()
					.WithGenericReturnType("T")
					.WithInstruction(new UnaryInstruction(OpCodes.Newobj, typeof(Customer).GetConstructor(Type.EmptyTypes)))
					.WithLocal(typeof(Customer))
					.WithInstruction(new NullaryInstruction(OpCodes.Stloc_0))
					.WithInstruction(new NullaryInstruction(OpCodes.Ldloc_0))
					.WithInstruction(new NullaryInstruction(OpCodes.Ret))
					.CommitMethod()
					.CommitType().SaveAssembly();
			});
			Assert.True(File.Exists(fileName));
			File.Delete(fileName);
		}

		// TODO: test with bad type name in CreateInstance
		[Fact]
		public void GeneratedAssemblyFileCreatesInstance()
		{
			var assemblyName = Guid.NewGuid().ToString("N");
			var fileName = $"{assemblyName}.dll";
			Assert.False(File.Exists(fileName));
			var generatedCodeAssembly = new GeneratedCodeAssembly(builder=>
			{
				builder.WithAssemblyName(assemblyName)
					.InCurrentDirectory()
					.WithClass("TestClassActivator").InNamespace("PRI.Activators")
						.ImplementsInterface<IActivator>()
						.WithMethod("Create")
							.WithReturnType(typeof(Customer))
							.WithGenericParameter("T").CommitGenericParameter()
							.WithGenericReturnType("T")
							.WithInstruction(new UnaryInstruction(OpCodes.Newobj, typeof(Customer)
								.GetConstructor(Type.EmptyTypes)))
							.WithLocal(typeof(Customer))
							.WithInstruction(new NullaryInstruction(OpCodes.Stloc_0))
							.WithInstruction(new NullaryInstruction(OpCodes.Ldloc_0))
							.WithInstruction(new NullaryInstruction(OpCodes.Ret))
						.CommitMethod()
					.CommitType();
			});
			var activator = generatedCodeAssembly.CreateInstance<IActivator>("PRI.Activators.TestClassActivator");
			Assert.NotNull(activator);
			Assert.Equal("PRI.Activators.TestClassActivator", activator.GetType().FullName);
			var testClass = activator.Create<Customer>();
			Assert.NotNull(testClass);
			Assert.IsType<Customer>(testClass);
			Assert.True(File.Exists(fileName));
			File.Delete(fileName);
		}
	}
}