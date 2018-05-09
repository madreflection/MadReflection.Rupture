using System;
using NUnit.Framework;

namespace MadReflection.Rupture.Tests
{
	[TestFixture]
	public class DefaultExtractor_Tests
	{
		private readonly ValueExtractor _extractor = ValueExtractor.Default;


		[TestCase(1)]
		public void Extract_Int32_Unboxes(object input)
		{
			// Arrange

			// Act
			int result = _extractor.Extract<int>(input);

			// Assert
			Assert.That(result, Is.EqualTo(input));
		}

		[TestCase(null)]
		public void Extract_Int32_Throws_On_Null(object input)
		{
			// Arrange

			// Act
			TestDelegate test = () => _extractor.Extract<int>(input);

			// Assert
			Assert.That(test, Throws.TypeOf<InvalidCastException>());
		}

		[TestCase(1)]
		[TestCase(null)]
		public void Extract_NullableInt32_Unboxes(object input)
		{
			// Arrange

			// Act
			int? result = _extractor.Extract<int?>(input);

			// Assert
			Assert.That(result, Is.EqualTo(input));
		}

		[TestCase("asdf")]
		[TestCase("")]
		public void Extract_String_Casts(object input)
		{
			// Arrange

			// Act
			string result = _extractor.Extract<string>(input);

			// Assert
			Assert.That(result, Is.EqualTo(input));
		}

		[TestCase(1)]
		public void Extract_String_Throws_On_Incorrect_Type(object input)
		{
			// Arrange

			// Act
			TestDelegate test = () => _extractor.Extract<string>(input);

			// Assert
			Assert.That(test, Throws.TypeOf<InvalidCastException>());
		}
	}
}
