using System;
using System.Reflection.Emit;
using PRI.GeneratedCodeAssembly.AssemblyGeneration;
using Xunit;

namespace PRI.GeneratedCodeAssembly.Tests
{
	public class IncorrectlyUsingNullaryInstruction
	{
		[Fact]
		public void NonNullaryInstructionThrowsDuringConstruction()
		{
			Assert.Throws<InvalidOperationException>(() => new NullaryInstruction(OpCodes.Unbox));
		}
	}
}