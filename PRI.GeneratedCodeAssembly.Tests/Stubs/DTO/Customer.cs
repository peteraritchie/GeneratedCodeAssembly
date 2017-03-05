#pragma warning disable CS0659
namespace PRI.GeneratedCodeAssembly.Tests.Stubs.DTO
{
	public class Customer
	{
		public string GivenName { get; set; }
		public string SurName;
		public long Age;

#pragma warning disable 659
		public override bool Equals(object obj)
#pragma warning restore 659
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof (Customer)) return false;
			return Equals(((Customer) obj).GivenName, GivenName) && Equals(((Customer) obj).SurName, SurName) && ((Customer) obj).Age == Age;
		}
	}
}
#pragma warning restore CS0659
