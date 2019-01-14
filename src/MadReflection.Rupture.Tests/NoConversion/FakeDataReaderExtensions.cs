using System;

namespace MadReflection.Rupture.Tests.NoConversion
{
	public static class FakeDataReaderExtensions
	{
		private static readonly ValueExtractor _extractor = ValueExtractor.Create(config =>
		{
			//config.UseNullTester(obj => obj is DBNull);

			//config.UseUnwrapper(
			//	obj => obj is ITestNullable,
			//	obj =>
			//	{
			//		if (((ITestNullable)obj).IsNull)
			//			return null;

			//		return obj.GetType().GetProperty("Value").GetValue(obj);
			//	});
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
