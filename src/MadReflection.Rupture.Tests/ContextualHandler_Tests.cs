using System;
using NUnit.Framework;

namespace MadReflection.Rupture.Tests
{
	public class ContextualHandler_Tests
	{

		[TestCase]
		public void ContextualHandler_Throws_InvalidCastException_If_No_Result_Assigned()
		{
			// Arrange
			ValueExtractor extractor = ValueExtractor.Create(config =>
			{
				config.UseHandler<string>(
					(val, ctx) =>
					{
					});
			});

			// Act
			TestDelegate test = () => extractor.Extract<string>("");

			// Assert
			Assert.That(test, Throws.TypeOf<InvalidCastException>());
		}

		[TestCase(true)]
		[TestCase(false)]
		[TestCase("Y")]
		[TestCase("N")]
		public void Unbox_DataReader_Columns_with_Custom_Handlers(object input)
		{
			// Arrange
			ValueExtractor extractor = ValueExtractor.Create(config =>
			{
				config.UseHandler<bool>(
					(obj, ctx) =>
					{
						if (obj is bool b)
						{
							ctx.Result = b;
							return;
						}

						if (obj is string s)
						{
							ctx.Result = s == "Y";
							return;
						}
					});
			});

			// Act
			bool result = extractor.Extract<bool>(input);

			// Assert
		}


	}
}
