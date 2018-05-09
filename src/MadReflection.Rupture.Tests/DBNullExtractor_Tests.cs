using System;
using NUnit.Framework;

namespace MadReflection.Rupture.Tests
{
	[TestFixture]
	public class DBNullExtractor_Tests
	{
		private readonly ValueExtractor _extractor = ValueExtractor.Create(config =>
		{
			config.UseNullTester(obj => obj is DBNull);
		});


		[TestCase]
		public void Extract_Int32_Throws_On_DBNull()
		{
			// Arrange

			// Act
			TestDelegate test = () => _extractor.Extract<int>(DBNull.Value);

			// Assert
			Assert.That(test, Throws.TypeOf<InvalidCastException>());
		}

		[TestCase]
		public void Extract_NullableInt32_Translates_DBNull_To_Null()
		{
			// Arrange

			// Act
			int? result = _extractor.Extract<int?>(DBNull.Value);

			// Assert
			Assert.That(result, Is.Null);
		}

		[TestCase]
		public void Extract_String_Translates_DBNull_To_Null()
		{
			// Arrange

			// Act
			string result = _extractor.Extract<string>(DBNull.Value);

			// Assert
			Assert.That(result, Is.Null);
		}
	}
}
