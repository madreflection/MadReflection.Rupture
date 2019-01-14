using System;

namespace MadReflection.Rupture.Tests.AutoConversion
{
	public static class FakeDataReaderExtensions
	{
		private static readonly ValueExtractor _extractor = ValueExtractor.Create(config =>
		{
			config.UseConverter(ExtractionConverters.DualTypeConverterWithFallback);
		});


		public static T Column<T>(this FakeDataReader reader, int i)
		{
			if (reader is null)
				throw new ArgumentException(nameof(reader));

			return _extractor.Extract<T>(reader.GetValue(i));
		}

		public static T Column<T>(this FakeDataReader reader, string name)
		{
			if (reader is null)
				throw new ArgumentException(nameof(reader));

			return _extractor.Extract<T>(reader.GetValue(reader.GetOrdinal(name)));
		}
	}
}
