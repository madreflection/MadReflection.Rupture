using System;
using System.ComponentModel;

namespace MadReflection.Rupture.Tests.ExplicitConversion
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

			//		return ((ITestNullable)obj).Value;
			//	});

			//config.UseHandler<bool>(
			//	(obj, ctx) =>
			//	{
			//		if (obj is bool b)
			//		{
			//			ctx.Result = b;
			//			return;
			//		}

			//		if (obj is string s)
			//		{
			//			ctx.Result = s == "Y";
			//			return;
			//		}
			//	});
		});


		public static T Column<T>(this MockDataReader reader, int i)
		{
			if (reader == null)
				throw new ArgumentException(nameof(reader));

			return _extractor.Extract<T>(reader.GetValue(i));
		}

		public static T Column<T>(this MockDataReader reader, string name)
		{
			if (reader == null)
				throw new ArgumentException(nameof(reader));

			return _extractor.Extract<T>(reader.GetValue(reader.GetOrdinal(name)));
		}

		public static TResult Column<T, TResult>(this MockDataReader reader, int i)
		{
			if (reader == null)
				throw new ArgumentException(nameof(reader));

			T value = _extractor.Extract<T>(reader.GetValue(i));

			return (TResult)ConvertToType(value, typeof(TResult));
		}

		public static TResult Column<T, TResult>(this MockDataReader reader, string name)
		{
			if (reader == null)
				throw new ArgumentException(nameof(reader));

			T value = _extractor.Extract<T>(reader.GetValue(reader.GetOrdinal(name)));

			return (TResult)ConvertToType(value, typeof(TResult));
		}


		private static object ConvertToType(object value, Type destinationType)
		{
			if (value != null)
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
