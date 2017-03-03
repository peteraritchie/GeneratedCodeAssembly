using System;
using System.Reflection.Emit;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	public class  NullaryInstruction : IIlInstruction
	{
		public NullaryInstruction(OpCode opCode)
		{
			OpCode = opCode;
			if (opCode.RequiresOperands())
				throw new InvalidOperationException($"OpCode {opCode} is not nullary.");
		}

		public OpCode OpCode { get; }

		public void Emit(ILGenerator ilGenerator)
		{
			ilGenerator.Emit(OpCode);
		}
	}
}