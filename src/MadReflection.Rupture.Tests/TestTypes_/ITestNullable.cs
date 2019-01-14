namespace MadReflection.Rupture.Tests
{
	// Similar to System.Data.SqlClient.INullable but it has a Value property so that reflection
	// isn't needed to get the value.
	public interface ITestNullable
	{
		bool IsNull { get; }
		object Value { get; }
	}
}
