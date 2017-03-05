#pragma warning disable CS0659
namespace PRI.GeneratedCodeAssembly.Tests.Stubs.Domain
{
	public class Customer2
	{
		public string GivenName { get; set; }
		public string SurName { get; set; }
		public long Age;

#pragma warning disable 659
		public override bool Equals(object obj)
#pragma warning restore 659
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Customer2)) return false;
			return ((Customer2)obj).Age == Age && Equals(((Customer2)obj).GivenName, GivenName) && Equals(((Customer2)obj).SurName, SurName);
		}
	}
}
#pragma warning restore CS0659
