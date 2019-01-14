using System;
using NUnit.Framework;

namespace MadReflection.Rupture.Tests.AutoConversion
{
	[TestFixture]
	public class AutoConversion_Tests
	{
		[Test]
		public void Unbox_AutoConvert_DataReader_Columns()
		{
			// Arrange

			// Act
			using (FakeDataReader reader = new FakeDataReader())
			{
				while (reader.Read())
				{
					int? i = reader.Column<int?>("Int32");

					long id = reader.Column<long>("Decimal");
					long? idx = reader.Column<long?>("Decimal");

					Uri uri = reader.Column<Uri>("UriString");
				}
			}

			// Assert
		}
	}
}
