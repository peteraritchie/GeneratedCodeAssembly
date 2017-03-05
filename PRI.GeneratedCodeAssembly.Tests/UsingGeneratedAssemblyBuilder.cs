using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Reflection.Emit;
using PRI.GeneratedCodeAssembly.AssemblyGeneration;
using PRI.GeneratedCodeAssembly.Tests.Stubs;
using Xunit;
using Customer = PRI.GeneratedCodeAssembly.Tests.Stubs.DTO.Customer;

namespace PRI.GeneratedCodeAssembly.Tests
{
	public class UsingGeneratedAssemblyBuilder
	{
		[Fact]
		public void TryValidateWithNullListThrows()
		{
			var builder = new GeneratedAssemblyBuilder();
			Assert.Throws<ArgumentNullException>(()=>builder.TryValidate(null));
		}

		[Fact]
		public void GeneratedAssemblyBuilderWithoutAssemblyInfoSucceeds()
		{
			/*builder.WithAssemblyName(.WithUniqueName)
			* .InDirectory
			* .WithAssemblyInfo
			* .WithClass(name)
			*  .Public
			*  .InterfaceImplementations.Add
			*  .BaseClass
			*/
			var builder = new GeneratedAssemblyBuilder();
			builder.WithAssemblyName(Guid.NewGuid().ToString("N"))
				.InCurrentDirectory()
				.WithClass("TestClassActivator").InNamespace("PRI.Activators")
				.WithMethod("Create")
					.WithReturnType(typeof(Customer))
					.WithInstruction(new UnaryInstruction(OpCodes.Newobj, typeof(Customer).GetConstructor(Type.EmptyTypes)))
					.WithLocal(typeof(Customer))
					.WithInstruction(new NullaryInstruction(OpCodes.Stloc_0))
					.WithInstruction(new NullaryInstruction(OpCodes.Ldloc_0))
					.WithInstruction(new NullaryInstruction(OpCodes.Ret))
					.WithParameter("number")
						.WithType(typeof(int))
						.CommitParameter()
					.CommitMethod()
				.CommitType().SaveAssembly();
			var results = new List<ValidationResult>();
			builder.TryValidate(results);
			var filePath = Path.Combine(builder.Directory, builder.Name + ".dll");
			Assert.True(File.Exists(filePath));
			File.Delete(filePath);
		}

		[Fact]
		public void GeneratedAssemblyBuilderTypeInRootNamespaceSucceeds()
		{
			/*builder.WithAssemblyName(.WithUniqueName)
			* .InDirectory
			* .WithAssemblyInfo
			* .WithClass(name)
			*  .Public
			*  .InterfaceImplementations.Add
			*  .BaseClass
			*/
			var builder = new GeneratedAssemblyBuilder();
			builder.WithAssemblyName(Guid.NewGuid().ToString("N"))
				.InCurrentDirectory()
				.WithClass("TestClassActivator")
				.WithMethod("Create")
					.WithReturnType(typeof(Customer))
					.WithInstruction(new UnaryInstruction(OpCodes.Newobj, typeof(Customer).GetConstructor(Type.EmptyTypes)))
					.WithLocal(typeof(Customer))
					.WithInstruction(new NullaryInstruction(OpCodes.Stloc_0))
					.WithInstruction(new NullaryInstruction(OpCodes.Ldloc_0))
					.WithInstruction(new NullaryInstruction(OpCodes.Ret))
					.WithParameter("number")
						.WithType(typeof(int))
						.CommitParameter()
					.CommitMethod()
				.CommitType().SaveAssembly();
			var results = new List<ValidationResult>();
			builder.TryValidate(results);
			var filePath = Path.Combine(builder.Directory, builder.Name + ".dll");
			Assert.True(File.Exists(filePath));
			File.Delete(filePath);
		}

		[Fact]
		public void WithNullDirectoryThrows()
		{
			var builder = new GeneratedAssemblyBuilder();
			var ex = Assert.Throws<ValidationException>(() => builder.WithAssemblyName(Guid.NewGuid().ToString("N"))
				.SaveAssembly()
			);
			Assert.Equal("Directory is null.", ex.Message);
		}

		[Fact]
		public void WithBadDirectoryPathThrows()
		{
			var builder = new GeneratedAssemblyBuilder();
			var path = $"x{Path.GetInvalidPathChars()[0]}x";
			var ex = Assert.Throws<ValidationException>(() =>
			{
				builder
					.WithUniqueAssemblyName()
					.InDirectory(path)
					.SaveAssembly();
			});
			Assert.Equal($"Directory {path} contains invalid characters.", ex.Message);
		}

		[Fact]
		public void WithNonExistantDirectoryPathThrows()
		{
			var builder = new GeneratedAssemblyBuilder();
			var ex = Assert.Throws<ValidationException>(() =>
			{
				builder
					.WithUniqueAssemblyName()
					.InDirectory(@".\bleah")
					.SaveAssembly();
			});
			Assert.Equal(@"Directory .\bleah does not exist.", ex.Message);
		}

		[Fact]
		public void WithInvalidClassThrows()
		{
			var builder = new GeneratedAssemblyBuilder();
			var ex = Assert.Throws<ValidationException>(() =>
			{
				builder
					.WithUniqueAssemblyName()
					.InCurrentDirectory()
					.WithClass("Program").CommitType()
					.WithClass("\"").CommitType()
					.SaveAssembly();
			});
			Assert.Equal("\" is not a valid class name.", ex.Message);
		}

		[Fact]
		public void WithNullNameThrows()
		{
			var builder = new GeneratedAssemblyBuilder();
			var ex = Assert.Throws<ValidationException>(() =>
			{
				builder
					.InCurrentDirectory()
					.SaveAssembly();
			});
			Assert.Equal("Assembly name is null.", ex.Message);
		}

		[Fact]
		public void WithBadNameThrows()
		{
			var builder = new GeneratedAssemblyBuilder();
			var path = $"x{Path.GetInvalidFileNameChars()[0]}x";
			var ex = Assert.Throws<ValidationException>(() =>
			{
				builder
					.WithAssemblyName(path)
					.InCurrentDirectory()
					.SaveAssembly();
			});
			Assert.Equal($"Assembly name {path} contains invalid characters.", ex.Message);
		}

		[Fact]
		public void GeneratedAssemblyBuilderWithAssemblyInfoSucceeds()
		{
			var builder = new GeneratedAssemblyBuilder();
			builder.WithAssemblyName(Guid.NewGuid().ToString("N"))
				.WithAssemblyInfo("company", "productName", "1.0.0.0", "description")
				.InCurrentDirectory()
				.WithClass("TestClassActivator").InNamespace("PRI.Activators")
					.ImplementsInterface<ISimple>()
					.WithMethod("SimpleMethod")
						.WithReturnType(typeof(void))
						.WithInstruction(new NullaryInstruction(OpCodes.Ret))
						.CommitMethod()
					.WithMethod("Create")
						.WithReturnType(typeof(Customer))
						.WithInstruction(new UnaryInstruction(OpCodes.Newobj, typeof(Customer).GetConstructor(Type.EmptyTypes)))
						.WithLocal(typeof(Customer))
						.WithInstruction(new NullaryInstruction(OpCodes.Stloc_0))
						.WithInstruction(new NullaryInstruction(OpCodes.Ldloc_0))
						.WithInstruction(new NullaryInstruction(OpCodes.Ret))
						.WithParameter()
							.WithType(typeof(int))
							.CommitParameter()
						.CommitMethod()
					.CommitType()
				.SaveAssembly();
			var results = new List<ValidationResult>();
			builder.TryValidate(results);
			var filePath = Path.Combine(builder.Directory, builder.Name + ".dll");
			Assert.True(File.Exists(filePath));
			File.Delete(filePath);
		}
	}
}