#pragma warning disable CS0659
namespace PRI.GeneratedCodeAssembly.Tests.Stubs.DTO
{
	public class Customer2
	{
		public string GivenName;
		public string SurName;
		public long Age { get; set; }

#pragma warning disable 659
		public override bool Equals(object obj)
#pragma warning restore 659
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			if (obj.GetType() != typeof(Customer2)) return false;
			return Equals(((Customer2)obj).GivenName, GivenName) && Equals(((Customer2)obj).SurName, SurName) && ((Customer2)obj).Age == Age;
		}
	}
}
#pragma warning restore CS0659
