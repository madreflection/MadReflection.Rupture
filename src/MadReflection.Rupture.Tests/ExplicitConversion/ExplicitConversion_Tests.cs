using NUnit.Framework;

namespace MadReflection.Rupture.Tests.ExplicitConversion
{
	[TestFixture]
	public class ExplicitConversion_Tests
	{
		[Test]
		public void Unbox_ExplicitConvert_DataReader_Columns()
		{
			// Arrange
			using (MockDataReader reader = new MockDataReader())
			{
				if (reader.Read())
				{
					// Act
					int? i1 = reader.Column<int?>("Int32");
					long id = reader.Column<decimal, long>("Decimal");
					long? idx = reader.Column<decimal?, long?>("Decimal");
				}
			}

			// Assert
		}
	}
}
