using System.Reflection.Emit;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	public interface IIlInstruction
	{
		OpCode OpCode { get; }
		void Emit(ILGenerator ilGenerator);
	}
}