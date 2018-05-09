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
			using (MockDataReader reader = new MockDataReader())
			{
				while (reader.Read())
				{
					// Act
					long id = reader.Column<long>("Decimal");
					int? junk = reader.Column<int?>("DBNull");
					Uri uri = reader.Column<Uri>("UriString");
				}
			}

			// Assert
		}
	}
}
