# GeneratedCodeAssembly
Cache generated code to assembly for quick re-load

*GeneratedCodeAssembly* is a class to facilitate code generation while also caching it to an assembly for re-loading later to avoid re-generation.

### Example

```csharp
	var assemblyName = Guid.NewGuid().ToString("N");

	var generatedCodeAssembly = new GeneratedCodeAssembly(builder=>
	{
		builder.WithAssemblyName(assemblyName)
			.InCurrentDirectory()
			.WithClass("TestClassActivator").InNamespace("PRI.Activators")
				.ImplementsInterface<IActivator>()
				.WithMethod("Create")
					.WithReturnType(typeof(Customer))
					.WithGenericParameter("T").CommitGenericParameter()
					.WithGenericReturnType("T")
					.WithInstruction(new UnaryInstruction(OpCodes.Newobj, typeof(Customer)
						.GetConstructor(Type.EmptyTypes)))
					.WithLocal(typeof(Customer))
					.WithInstruction(new NullaryInstruction(OpCodes.Stloc_0))
					.WithInstruction(new NullaryInstruction(OpCodes.Ldloc_0))
					.WithInstruction(new NullaryInstruction(OpCodes.Ret))
				.CommitMethod()
			.CommitType();
	});
	var activator = generatedCodeAssembly.CreateInstance<IActivator>("PRI.Activators.TestClassActivator");
	var testClass = activator.Create<Customer>();
```
In this example, GeneratedCodeAssembly is instantiated and the type builder is configured via a fluent interface to create a class that implements the interface `IActivator` and the `IActivator.Create<T>` method.  It then goes on to implment that method to create and return an new instances of `Customer`.

This example makes use of an `IActivator` interface as follows:

```csharp
	public interface IActivator
	{
		T Create<T>();
	}
```

The fluent code builder is very powerful, but does not support all the features of `Reflection.Emit` yet.

Eventually I'll document the builder more thoroughly but in the meantime, some notes:
 - If you do not provide assembly info (company name, product name, version, and description) via `WithAssemblyInfo` assembly info, the info from the currently executing assembly will be used.

### Caveats/possible future features
*GeneratedCodeAssembly* does not update an assembly, it only creates new assemblies.  This is partially due to the fact that when you generate code you're likely going to try to run it right away, which makes the generated assembly in-use and unable to be updated.

The builder needs to be expanded to support more `Reflection.Emit` features.

Assembly verification is not performed and the IL that is generated is up to you; so you could easily create an unverifable/unsafe assembly.  Any grave syntax errors will cause the assembly not to be generated and some cryptic `Reflection.Emit` exceptions will be raised.

It is up to you to track the filename and version of the assembly, *GeneratedCodeAssembly* just checks for existance--it's not smart enough to know that the assembly may contain out-of-date code.  You can version the assembly (automaticly from the executing assembly; so if you auto-increment that, then generated assembly version will be copied) you'll be able to compare version info.

Code coverage is about 96% and will be expanded in some obvious areas.