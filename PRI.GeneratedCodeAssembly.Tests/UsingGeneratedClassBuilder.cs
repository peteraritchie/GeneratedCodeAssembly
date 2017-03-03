using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using PRI.GeneratedCodeAssembly.AssemblyGeneration;
using PRI.GeneratedCodeAssembly.Tests.Stubs;
using PRI.GeneratedCodeAssembly.Tests.Stubs.Domain;
using Xunit;

namespace PRI.GeneratedCodeAssembly.Tests
{
	public class UsingGeneratedClassBuilder
	{
		readonly GeneratedAssemblyBuilder _assemblyBuilder;
		private GeneratedClassBuilder _generatedClassBuilder;

		public UsingGeneratedClassBuilder()
		{
			_assemblyBuilder = new GeneratedAssemblyBuilder();
			_generatedClassBuilder = new GeneratedClassBuilder(_assemblyBuilder, "Name");
		}

		[Fact]
		public void WithInvalidNamespaceThrows()
		{
			_generatedClassBuilder.InNamespace("\"");
			var ex = Assert.Throws<ValidationException>(() => _generatedClassBuilder.Validate());
			Assert.Equal("\" is not a valid namespace name.", ex.Message);
		}

		[Fact]
		public void WithNonInterfaceInterfaceThrows()
		{
			_generatedClassBuilder.ImplementsInterface<Customer>();
			var ex = Assert.Throws<ValidationException>(() => _generatedClassBuilder.Validate());
			Assert.Equal($"Type {typeof(Customer).FullName} is not an interface.", ex.Message);
		}

		[Fact]
		public void WithInterfaceInterfaceSucceeds()
		{
			_generatedClassBuilder.ImplementsInterface<ISimple>();
			_generatedClassBuilder.WithMethod("SimpleMethod")
				.WithReturnType(typeof(void))
				.WithInstruction(new NullaryInstruction(OpCodes.Ret))
				.CommitMethod()
				.CommitType();
			Assert.DoesNotThrow(() => _generatedClassBuilder.Validate());
		}

		[Fact]
		public void WithInvalidMethodThrows()
		{
			_generatedClassBuilder.WithMethod("\"").WithInstruction(new NullaryInstruction(OpCodes.Ret));
			var ex = Assert.Throws<ValidationException>(() => _generatedClassBuilder.Validate());
			Assert.Equal("\" is not a valid method name.", ex.Message);
		}

		[Fact]
		public void TryValidateWithNullListThrows()
		{
			Assert.Throws<ArgumentNullException>(()=>_generatedClassBuilder.TryValidate(null));
		}
	}
}