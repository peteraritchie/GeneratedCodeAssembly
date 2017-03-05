using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using PRI.GeneratedCodeAssembly.AssemblyGeneration;
using Xunit;

namespace PRI.GeneratedCodeAssembly.Tests
{
	public class UsingGenerateMethodParameterBuilder
	{
		private readonly IGeneratedMethodParameterBuilder _generatedMethodParameterBuilder;

		public UsingGenerateMethodParameterBuilder()
		{
			 var assemblyBuilder = new GeneratedAssemblyBuilder();
			var classBuilder = new GeneratedClassBuilder(assemblyBuilder, "Name");
			var methodBuilder = classBuilder.WithMethod("Method");
			_generatedMethodParameterBuilder = methodBuilder.WithParameter();
		}

		[Fact]
		public void WithNullParameterTypeThrows()
		{
			_generatedMethodParameterBuilder.WithType(null);
			var ex = Assert.Throws<ValidationException>(() => _generatedMethodParameterBuilder.Validate());
			Assert.Equal("Parameter type cannot be null.", ex.Message);
		}

		[Fact]
		public void WithVoidParameterTypeThrows()
		{
			_generatedMethodParameterBuilder.WithType(typeof(void));
			var ex = Assert.Throws<ValidationException>(() => _generatedMethodParameterBuilder.Validate());
			Assert.Equal("Parameter type cannot be void.", ex.Message);
		}

		[Fact]
		public void WithInvalidNameThrows()
		{
			_generatedMethodParameterBuilder.WithType(typeof(int));
			_generatedMethodParameterBuilder.WithName("\"");
			var ex = Assert.Throws<ValidationException>(() => _generatedMethodParameterBuilder.Validate());
			Assert.Equal("\" is not a valid parameter name.", ex.Message);
		}
#if false

		[Fact]
		public void WithNoInstructionsThrows()
		{
			_methodBuilder.WithReturnType(typeof(void));
			var ex = Assert.Throws<ValidationException>(() => _methodBuilder.Validate());
			Assert.Equal("Method requires instructions, none were provided.", ex.Message);
		}

		[Fact]
		public void WithVoidLocalTypeThrows()
		{
			_methodBuilder.WithReturnType(typeof(void));
			_methodBuilder.WithInstruction(new NullaryInstruction(OpCodes.Ret));
			_methodBuilder.WithLocal(typeof(void));
			var ex = Assert.Throws<ValidationException>(() => _methodBuilder.Validate());
			Assert.Equal("Void local type is not supported.", ex.Message);
		}

		[Fact]
		public void WithNullLocalTypeThrows()
		{
			_methodBuilder.WithReturnType(typeof(void));
			_methodBuilder.WithInstruction(new NullaryInstruction(OpCodes.Ret));
			_methodBuilder.WithLocal(null);
			var ex = Assert.Throws<ValidationException>(() => _methodBuilder.Validate());
			Assert.Equal("Null local type is not supported.", ex.Message);
		}

		[Fact]
		public void WithInvalidMethodNameThrows()
		{
			_methodBuilder.WithReturnType(typeof(void));
			_methodBuilder.WithInstruction(new NullaryInstruction(OpCodes.Ret));
			_methodBuilder.WithName("\"");
			var ex = Assert.Throws<ValidationException>(() => _methodBuilder.Validate());
			Assert.Equal("\" is not a valid method name.", ex.Message);
		}
#endif
	}
}