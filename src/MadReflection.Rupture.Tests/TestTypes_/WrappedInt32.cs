namespace MadReflection.Rupture.Tests
{
	// Minimally approximates System.Data.SqlClient.SqlInt32.
	public struct WrappedInt32 : ITestNullable
	{
		private int? _value;


		public WrappedInt32(int? value)
		{
			_value = value;
		}


		public int Value => (int)_value;


		bool ITestNullable.IsNull => _value == null;

		object ITestNullable.Value => (int)_value;
	}
}
