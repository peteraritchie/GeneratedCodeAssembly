using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	[ExcludeFromCodeCoverage]
	public static class OpCodeExtensions
	{
		private static readonly StackBehaviour[] StackPushBehaviours = {
			StackBehaviour.Push1,
			StackBehaviour.Pushi, StackBehaviour.Pushi8,
			StackBehaviour.Pushref,
			StackBehaviour.Pushr4, StackBehaviour.Pushr8,
			StackBehaviour.Varpush
		};

		private static readonly OperandType[] InlineOperandTypes = new[]
		{
			OperandType.ShortInlineVar,
			OperandType.InlineType, OperandType.InlineBrTarget, OperandType.InlineField, OperandType.InlineI,
			OperandType.InlineI8, OperandType.InlineMethod, OperandType.InlineR, OperandType.InlineSig, OperandType.InlineString,
			OperandType.InlineSwitch, OperandType.InlineTok, OperandType.InlineVar, OperandType.ShortInlineBrTarget,
			OperandType.ShortInlineI, OperandType.ShortInlineR
		};

		public static bool RequiresOperands(this OpCode opCode)
		{
			if (opCode.OperandType.IsOneOf(InlineOperandTypes)) return true;
			if (opCode.OperandType == OperandType.InlineNone) return false;
			if (opCode.StackBehaviourPush.IsOneOf(StackPushBehaviours) && !opCode.Name.Split('.').Last().IsInteger())
			{
				return true;
			}
			if (opCode.FlowControl == FlowControl.Call || opCode.FlowControl == FlowControl.Branch || opCode.FlowControl == FlowControl.Cond_Branch) return true;
			return false;
		}

		public static bool IsOneOf(this StackBehaviour value, IEnumerable<StackBehaviour> coll)
		{
			if (coll == null) throw new ArgumentNullException(nameof(coll));
			return coll.Contains(value);
		}

		public static bool IsOneOf(this OperandType value, IEnumerable<OperandType> coll)
		{
			if (coll == null) throw new ArgumentNullException(nameof(coll));
			return coll.Contains(value);
		}
		public static bool IsInteger(this string textValue)
		{
			int intValue;
			if(int.TryParse(textValue, NumberStyles.Integer, CultureInfo.InvariantCulture, out intValue))
			{
				return true;
			}
			return false;
		}
	}
}