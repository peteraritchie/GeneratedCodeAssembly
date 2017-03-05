using System;
using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace PRI.GeneratedCodeAssembly.AssemblyGeneration
{
	[ExcludeFromCodeCoverage]
	public static class BuilderExtensions
	{
		private static string executingAssemblyNameText;
		private static Func<string> getExecutingAssemblyName = () =>
		{
			var att =
				Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false).SingleOrDefault() as AssemblyProductAttribute;
			executingAssemblyNameText = att == null ? "PRI.GeneratedCodeAssembly" : att.Product;
			getExecutingAssemblyName = () => executingAssemblyNameText;
			return getExecutingAssemblyName();
		};
		public static string GetExecutingAssemblyName() => getExecutingAssemblyName();

		private static string executingAssemblyVersionText;
		private static Func<string> getExecutingAssemblyVersion = () =>
		{
			executingAssemblyVersionText = Assembly.GetExecutingAssembly().GetName().Version.ToString();
			getExecutingAssemblyName = () => executingAssemblyVersionText;
			return getExecutingAssemblyName();
		};

		public static string GetExecutingAssemblyVersion() => getExecutingAssemblyVersion();

/*
		public static TypeBuilder DefinePublicClass(this ModuleBuilder moduleBuilder, string name)
		{
			if (moduleBuilder == null) throw new ArgumentNullException(nameof(moduleBuilder));
			var typeBuilder = moduleBuilder.DefineType(name, TypeAttributes.Class | TypeAttributes.Public);
			typeBuilder.SetCustomAttribute(
				new CustomAttributeBuilder(typeof(GeneratedCodeAttribute).GetConstructor(new[] { typeof(string), typeof(string) }),
					new object[] { getExecutingAssemblyName(), getExecutingAssemblyVersion() }));
			return typeBuilder;
		}
*/

		public static MethodBuilder DefineInterfaceMethod(this TypeBuilder typeBuilder, Type interfaceType, string name, Type returnType, params Type[] parameterTypes)
		{
			if (typeBuilder == null) throw new ArgumentNullException(nameof(typeBuilder));
			var method = typeBuilder.DefineMethod(name, MethodAttributes.Final | MethodAttributes.Public | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.NewSlot, returnType,
				parameterTypes);
			typeBuilder.DefineMethodOverride(method, interfaceType.GetMethod(name));
			return method;
		}

		public static MethodBuilder DefinePublicMethod(this TypeBuilder typeBuilder, string name, Type returnType,
			params Type[] parameterTypes)
		{
			if (typeBuilder == null) throw new ArgumentNullException(nameof(typeBuilder));
			return typeBuilder.DefineMethod(name,
				MethodAttributes.Public, returnType,
				parameterTypes);
		}

/*
		public static MethodBuilder DefinePublicVirtualInterfaceMethod(this TypeBuilder typeBuilder, string name, Type returnType)
		{
			if (typeBuilder == null) throw new ArgumentNullException(nameof(typeBuilder));
			return typeBuilder.DefineMethod(name,
				MethodAttributes.Virtual | MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.ReuseSlot, returnType,
				Type.EmptyTypes);
		}
*/

/*
		public static ILGenerator EmitReturnNewInstance(this ILGenerator il, Type typeToConstruct)
		{
			if (il == null) throw new ArgumentNullException(nameof(il));
			if (typeToConstruct == null) throw new ArgumentNullException(nameof(typeToConstruct));

			var ctor = typeToConstruct.GetConstructor(Type.EmptyTypes);
			if (ctor == null)
			{
				throw new MissingMethodException("There is no constructor without defined parameters for this object");
			}
			return EmitReturnNewInstance(il, ctor);
		}
*/

		public static ILGenerator EmitReturnNewInstance(this ILGenerator il, ConstructorInfo ctor)
		{
			il.DeclareLocal(ctor.ReflectedType ?? ctor.DeclaringType);
			il.Emit(OpCodes.Newobj, ctor);
			il.Emit(OpCodes.Stloc_0);
			il.Emit(OpCodes.Ldloc_0);
			il.Emit(OpCodes.Ret);
			return il;
		}

		private static readonly ConstructorInfo AssemblyVersionAttribute_ctor = typeof(AssemblyVersionAttribute).GetConstructor(
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
			null,
			new[] { typeof(string) },
			null
		);

/*
		public static void CopyAssemblyVersion(this AssemblyBuilder assemblyBuilder)
		{
			var executingAssemblyVersion = getExecutingAssemblyVersion();
			SetAssemblyVersion(assemblyBuilder, executingAssemblyVersion);
		}
*/

		public static void SetAssemblyVersion(this AssemblyBuilder assemblyBuilder, string version)
		{
			assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(AssemblyVersionAttribute_ctor,
				new object[] { version }));
		}

		private static readonly ConstructorInfo AssemblyProductAttribute_ctor = typeof(AssemblyProductAttribute).GetConstructor(
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
			null,
			new[] { typeof(string) },
			null
		);

		public static void SetAssemblyProduct(this AssemblyBuilder assemblyBuilder, string productName)
		{
			assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(AssemblyProductAttribute_ctor,
				new object[] { productName }));
		}

		public static void SetAssemblyInfo(this AssemblyBuilder assemblyBuilder, string company, string productName, string version, string description)
		{
			if (!string.IsNullOrWhiteSpace(productName)) assemblyBuilder.SetAssemblyProduct(productName);
			if (!string.IsNullOrWhiteSpace(version)) assemblyBuilder.SetAssemblyVersion(version);
			if (!string.IsNullOrWhiteSpace(description)) assemblyBuilder.SetAssemblyDescription(description);
			if (!string.IsNullOrWhiteSpace(company)) assemblyBuilder.SetAssemblyCompany(company);
			if (!string.IsNullOrWhiteSpace(company)) assemblyBuilder.SetAssemblyCopyright($"Copyright © {company} {DateTime.Now.Year}");
		}

		private static readonly ConstructorInfo AssemblyDescriptionAttribute_ctor = typeof(AssemblyDescriptionAttribute).GetConstructor(
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
			null,
			new[] { typeof(string) },
			null
		);

		public static void SetAssemblyDescription(this AssemblyBuilder assemblyBuilder, string description)
		{
			assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(AssemblyDescriptionAttribute_ctor,
				new object[] { description }));
		}

		private static readonly ConstructorInfo AssemblyCompanyAttribute_ctor = typeof(AssemblyCompanyAttribute).GetConstructor(
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
			null,
			new[] { typeof(string) },
			null
		);

		public static void SetAssemblyCompany(this AssemblyBuilder assemblyBuilder, string company)
		{
			assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(AssemblyCompanyAttribute_ctor,
				new object[] { company }));
		}

		private static readonly ConstructorInfo AssemblyCopyrightAttribute_ctor = typeof(AssemblyCopyrightAttribute).GetConstructor(
			BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
			null,
			new[] { typeof(string) },
			null
		);

		public static void SetAssemblyCopyright(this AssemblyBuilder assemblyBuilder, string copyright)
		{
			assemblyBuilder.SetCustomAttribute(new CustomAttributeBuilder(AssemblyCopyrightAttribute_ctor,
				new object[] { copyright }));
		}
	}
}