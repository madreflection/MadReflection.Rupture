using System;
using System.ComponentModel;

namespace MadReflection.Rupture.Tests.ExplicitConversion
{
	// In these unit tests, the conversion is applied after the extractor returns because T and
	// TResult are separate.  What they're really testing is the DualTypeConverterWithFallback
	// converter outside of Rupture, so that a T can be unboxed and converted to TResult.

	public static class FakeDataReaderExtensions
	{
		private static readonly ValueExtractor _extractor = ValueExtractor.Default;


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

		public static TResult Column<T, TResult>(this FakeDataReader reader, int i)
		{
			if (reader is null)
				throw new ArgumentException(nameof(reader));

			T value = _extractor.Extract<T>(reader.GetValue(i));

			return (TResult)ExtractionConverters.DualTypeConverterWithFallback.ConvertToType(value, typeof(TResult));
		}

		public static TResult Column<T, TResult>(this FakeDataReader reader, string name)
		{
			if (reader is null)
				throw new ArgumentException(nameof(reader));

			T value = _extractor.Extract<T>(reader.GetValue(reader.GetOrdinal(name)));

			return (TResult)ExtractionConverters.DualTypeConverterWithFallback.ConvertToType(value, typeof(TResult));
		}


		private static object ConvertToType(object value, Type destinationType)
		{
			if (value is object)
			{
				if (Nullable.GetUnderlyingType(destinationType) is Type underlyingType)
					return ConvertToType(value, underlyingType);

				if (TypeDescriptor.GetConverter(destinationType) is TypeConverter converter && converter.CanConvertFrom(value.GetType()))
					return converter.ConvertFrom(value);
			}

			return Convert.ChangeType(value, destinationType);
		}
	}
}
