using System;
using System.Reflection;
using System.Reflection.Emit;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	public class UnaryInstruction : IIlInstruction
	{
		private readonly Action<ILGenerator, OpCode> _emit;

		public UnaryInstruction(OpCode opCode, Type type)
		{
			OpCode = opCode; // TODO: Verify types of opcodes that won't work with Type?
			if (!opCode.RequiresOperands())
				throw new InvalidOperationException($"OpCode {opCode} does not support pushing Type references.");
			_emit = (ilGenerator, code) => ilGenerator.Emit(OpCode, type);
		}

		public UnaryInstruction(OpCode opCode, FieldInfo fieldInfo)
		{
			OpCode = opCode; // TODO: Verify types of opcodes that won't work with FieldInfo?
			if (!opCode.RequiresOperands())
				throw new InvalidOperationException($"OpCode {opCode} does not support pushing FieldInfo references.");
			_emit = (ilGenerator, code) => ilGenerator.Emit(OpCode, fieldInfo);
		}

		public UnaryInstruction(OpCode opCode, MethodInfo methodInfo)
		{
			OpCode = opCode; // TODO: Verify types of opcodes that won't work with MethodInfo?
			if (!opCode.RequiresOperands() || opCode.StackBehaviourPush != StackBehaviour.Varpush)
				throw new InvalidOperationException($"OpCode {opCode} does not support pushing MethodInfo references.");
			_emit = (ilGenerator, code) => ilGenerator.Emit(OpCode, methodInfo);
		}

		public UnaryInstruction(OpCode opCode, int value)
		{
			OpCode = opCode;
			if (!opCode.RequiresOperands() || (opCode.StackBehaviourPush != StackBehaviour.Pushi && opCode.StackBehaviourPush != StackBehaviour.Varpush))
				throw new InvalidOperationException($"OpCode {opCode} does not support pushing int values.");
			// TODO: more verification
			_emit = (iLGenerator, code) => iLGenerator.Emit(OpCode, value);
		}

		public UnaryInstruction(OpCode opCode, ConstructorInfo constructorInfo)
		{
			OpCode = opCode; // TODO: Verify types of opcodes that work with ConstructorInfo?
			if (opCode.StackBehaviourPush != StackBehaviour.Pushref)
				throw new InvalidOperationException($"OpCode {opCode} does not support pushing ConstructorInfo references.");
			_emit = (ilGenerator, code) => ilGenerator.Emit(OpCode, constructorInfo);
		}

		public OpCode OpCode { get; }
		public void Emit(ILGenerator ilGenerator)
		{
			_emit(ilGenerator, OpCode);
		}
	}
}