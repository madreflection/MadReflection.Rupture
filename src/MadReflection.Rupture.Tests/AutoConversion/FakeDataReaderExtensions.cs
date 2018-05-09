using System;
using System.ComponentModel;
using System.Data.SqlTypes;

namespace MadReflection.Rupture.Tests.AutoConversion
{
	public static class FakeDataReaderExtensions
	{
		private static readonly ValueExtractor _extractor = ValueExtractor.Create(config =>
		{
			//config.UseNullTester(obj => obj is DBNull);

			//config.UseUnwrapper(
			//	obj => obj is INullable,
			//	obj =>
			//	{
			//		if (((INullable)obj).IsNull)
			//			return null;

			//		return obj.GetType().GetProperty("Value").GetValue(obj);
			//	});

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

			config.UseConverter(ExtractionConverters.DualTypeConverterWithFallback);
		});


		public static T Column<T>(this MockDataReader reader, int i)
		{
			if (reader == null)
				throw new ArgumentException(nameof(reader));

			object value = reader.GetValue(i);
			if (value is DBNull)
				value = null;

			return ConvertToType<T>(value);
		}

		public static T Column<T>(this MockDataReader reader, string name)
		{
			if (reader == null)
				throw new ArgumentException(nameof(reader));

			object value = reader.GetValue(reader.GetOrdinal(name));
			if (value is DBNull)
				value = null;

			return ConvertToType<T>(value);
		}


		private static T ConvertToType<T>(object value)
		{
			if (value == null)
				return default(T);

			Type destinationType = typeof(T);

			if (TypeDescriptor.GetConverter(destinationType) is TypeConverter converter && converter.CanConvertFrom(value.GetType()))
				return (T)converter.ConvertFrom(value);

			return (T)Convert.ChangeType(value, destinationType);
		}
	}
}
