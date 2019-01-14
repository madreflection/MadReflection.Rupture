using System;
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

			// Act
			using (FakeDataReader reader = new FakeDataReader())
			{
				if (reader.Read())
				{
					int? i = reader.Column<int?>("Int32");

					long id = reader.Column<decimal, long>("Decimal");
					long? idx = reader.Column<decimal?, long?>("Decimal");

					Uri uri = reader.Column<string, Uri>("UriString");
				}
			}

			// Assert
		}
	}
}
