using NUnit.Framework;

namespace MadReflection.Rupture.Tests
{
	[TestFixture]
	public class NullWrappedExtractor_Tests
	{
		private readonly ValueExtractor _extractor = ValueExtractor.Create(config =>
		{
			config.UseNullTester(obj => (obj as ITestNullable)?.IsNull ?? false);
			config.UseUnwrapper(
				obj => obj is ITestNullable,
				obj => ((ITestNullable)obj).Value
				);
		});


		[TestCase(1)]
		public void Extract_Int32_Unwraps(int input)
		{
			// Arrange
			WrappedInt32 value = new WrappedInt32(input);

			// Act
			int result = _extractor.Extract<int>(value);

			// Assert
			Assert.That(result, Is.EqualTo(input));
		}

		[TestCase(1)]
		[TestCase(2)]
		[TestCase(null)]
		public void Extract_NullableInt32_Unwraps(int? input)
		{
			// Arrange
			WrappedInt32 value = new WrappedInt32(input);

			// Act
			int? result = _extractor.Extract<int?>(value);

			// Assert
			Assert.That(result, Is.EqualTo(input));
		}
	}
}
