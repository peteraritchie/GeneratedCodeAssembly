using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using PRI.GeneratedCodeAssembly.AssemblyGeneration;
using Xunit;

namespace PRI.GeneratedCodeAssembly.Tests
{
	public class UsingGenerateMethodBuilder
	{
		readonly GeneratedAssemblyBuilder _assemblyBuilder;
		private GeneratedClassBuilder _classBuilder;
		private IGeneratedMethodBuilder _generatedMethodBuilder;
		public UsingGenerateMethodBuilder()
		{
			_assemblyBuilder = new GeneratedAssemblyBuilder();
			_classBuilder = new GeneratedClassBuilder(_assemblyBuilder, "Name");
			_generatedMethodBuilder = _classBuilder.WithMethod("Method");
		}

		[Fact]
		public void WithNullReturnThrows()
		{
			_generatedMethodBuilder.WithReturnType(null);
			var ex = Assert.Throws<ValidationException>(() => _generatedMethodBuilder.Validate());
			Assert.Equal("ReturnType is null.", ex.Message);
		}

		[Fact]
		public void WithGenericReturnThrows()
		{
			_generatedMethodBuilder.WithGenericReturnType("T");
			var ex = Assert.Throws<ValidationException>(() => _generatedMethodBuilder.Validate());
			Assert.Equal("Generic return type name T not specified in method generic parameters.", ex.Message);
		}

		[Fact]
		public void WithNullInstructionThrows()
		{
			_generatedMethodBuilder.WithReturnType(typeof(void));
			_generatedMethodBuilder.WithInstruction(null);
			var ex = Assert.Throws<ValidationException>(() => _generatedMethodBuilder.Validate());
			Assert.Equal("Null instruction is not supported.", ex.Message);
		}

		[Fact]
		public void WithNoInstructionsThrows()
		{
			_generatedMethodBuilder.WithReturnType(typeof(void));
			var ex = Assert.Throws<ValidationException>(() => _generatedMethodBuilder.Validate());
			Assert.Equal("Method requires instructions, none were provided.", ex.Message);
		}

		[Fact]
		public void WithVoidLocalTypeThrows()
		{
			_generatedMethodBuilder.WithReturnType(typeof(void));
			_generatedMethodBuilder.WithInstruction(new NullaryInstruction(OpCodes.Ret));
			_generatedMethodBuilder.WithLocal(typeof(void));
			var ex = Assert.Throws<ValidationException>(() => _generatedMethodBuilder.Validate());
			Assert.Equal("Void local type is not supported.", ex.Message);
		}

		[Fact]
		public void WithNullLocalTypeThrows()
		{
			_generatedMethodBuilder.WithReturnType(typeof(void));
			_generatedMethodBuilder.WithInstruction(new NullaryInstruction(OpCodes.Ret));
			_generatedMethodBuilder.WithLocal(null);
			var ex = Assert.Throws<ValidationException>(() => _generatedMethodBuilder.Validate());
			Assert.Equal("Null local type is not supported.", ex.Message);
		}

		[Fact]
		public void WithInvalidMethodNameThrows()
		{
			_generatedMethodBuilder.WithReturnType(typeof(void));
			_generatedMethodBuilder.WithInstruction(new NullaryInstruction(OpCodes.Ret));
			_generatedMethodBuilder.WithName("\"");
			var ex = Assert.Throws<ValidationException>(() => _generatedMethodBuilder.Validate());
			Assert.Equal("\" is not a valid method name.", ex.Message);
		}
	}
}