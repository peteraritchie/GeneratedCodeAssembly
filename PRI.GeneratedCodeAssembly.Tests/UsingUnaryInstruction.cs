using System;
using System.Reflection;
using System.Reflection.Emit;
using PRI.GeneratedCodeAssembly.AssemblyGeneration;
using PRI.GeneratedCodeAssembly.Tests.Stubs.DTO;
using Xunit;

namespace PRI.GeneratedCodeAssembly.Tests
{
	public class UsingUnaryInstruction
	{
		private readonly ILGenerator _ilGenerator;

		[Fact]
		public void MethodInfoOperandSucceeds()
		{
			new UnaryInstruction(OpCodes.Callvirt, typeof(Customer2).GetProperty(nameof(Customer2.Age)).GetSetMethod());
		}

		[Fact]
		public void TypeOperandSucceeds()
		{
			new UnaryInstruction(OpCodes.Ldobj, typeof(Customer2));
		}

		[Fact]
		public void FieldInfoOperandSucceeds()
		{
			new UnaryInstruction(OpCodes.Ldfld, typeof(Customer2).GetField(nameof(Customer2.GivenName)));
		}

		[Fact]
		public void ConstructorInfoOperandSucceeds()
		{
			new UnaryInstruction(OpCodes.Newobj, typeof(Customer).GetConstructor(Type.EmptyTypes));
		}

		[Fact]
		public void IntOperandSucceeds()
		{
			new UnaryInstruction(OpCodes.Ldc_I4_S, 10);
		}

		public UsingUnaryInstruction()
		{
			var asmName = new AssemblyName("temp");
			var asmBuilder = AssemblyBuilder.DefineDynamicAssembly
				(asmName, AssemblyBuilderAccess.Run);
			var moduleBuilder = asmBuilder.DefineDynamicModule("temp");

			var typeBuilder = moduleBuilder.DefineType("Program", TypeAttributes.Public);
			var methodBuilder = typeBuilder.DefineMethod("Main",
				MethodAttributes.Static, typeof(void), new[] { typeof(string) });
			_ilGenerator = methodBuilder.GetILGenerator();
		}

		[Fact]
		public void MethodInfoOperandEmitSucceeds()
		{
			var unaryInstruction = new UnaryInstruction(OpCodes.Callvirt, typeof(Customer2).GetProperty(nameof(Customer2.Age)).GetSetMethod());
			unaryInstruction.Emit(_ilGenerator);
		}

		[Fact]
		public void TypeOperandEmitSucceeds()
		{
			var unaryInstruction = new UnaryInstruction(OpCodes.Ldobj, typeof(Customer2));

			unaryInstruction.Emit(_ilGenerator);
		}

		[Fact]
		public void FieldInfoOperandEmitSucceeds()
		{
			var unaryInstruction = new UnaryInstruction(OpCodes.Ldfld, typeof(Customer2).GetField(nameof(Customer2.GivenName)));

			unaryInstruction.Emit(_ilGenerator);
		}

		[Fact]
		public void ConstructorInfoOperandEmitSucceeds()
		{
			var unaryInstruction = new UnaryInstruction(OpCodes.Newobj, typeof(Customer).GetConstructor(Type.EmptyTypes));

			unaryInstruction.Emit(_ilGenerator);
		}

		[Fact]
		public void IntOperandEmitSucceeds()
		{
			var unaryInstruction = new UnaryInstruction(OpCodes.Ldc_I4_S, 10);

			unaryInstruction.Emit(_ilGenerator);
		}

		[Fact]
		public void NullaryInstructionWithConstructorInfoThrows()
		{
			Assert.Throws<InvalidOperationException>(() =>
				new UnaryInstruction(OpCodes.Stloc_0, (ConstructorInfo) null));
		}

		[Fact]
		public void NullaryInstructionWithIntThrows()
		{
			Assert.Throws<InvalidOperationException>(() =>
				new UnaryInstruction(OpCodes.Stloc_0, 0));
		}

		[Fact]
		public void NullaryInstructionWithMethodInfoThrows()
		{
			Assert.Throws<InvalidOperationException>(() =>
				new UnaryInstruction(OpCodes.Stloc_0, (MethodInfo)null));
		}

		[Fact]
		public void NullaryInstructionWithFieldInfoThrows()
		{
			Assert.Throws<InvalidOperationException>(() =>
				new UnaryInstruction(OpCodes.Stloc_0, (FieldInfo)null));
		}

		[Fact]
		public void NullaryInstructionWithTypeThrows()
		{
			Assert.Throws<InvalidOperationException>(() =>
				new UnaryInstruction(OpCodes.Stloc_0, typeof(void)));
		}
	}
}