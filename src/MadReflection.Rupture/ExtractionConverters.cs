using System;
using System.ComponentModel;
using System.Reflection;

namespace MadReflection.Rupture
{
	/// <summary>
	/// Provides pre-defined implementations of <see cref="IConverter"/>.
	/// </summary>
	public static class ExtractionConverters
	{
		/// <summary>
		/// Implements <see cref="IConverter"/> by calling <see cref="Convert.ChangeType(object, Type)"/>.
		/// </summary>
		public static readonly IConverter SystemDefault = new SystemDefaultImpl();

		/// <summary>
		/// Implements <see cref="IConverter"/> using a type converter defined on either the source or destination type, if available, and falling back on <see cref="Convert.ChangeType(object, Type)"/>.
		/// </summary>
		public static readonly IConverter DualTypeConverterWithFallback = new DualTypeConverterWithFallbackImpl();


		private class SystemDefaultImpl : IConverter
		{
			public object ConvertToType(object value, Type destinationType) => Convert.ChangeType(value, destinationType);
		}

		private class DualTypeConverterWithFallbackImpl : IConverter
		{
			public object ConvertToType(object value, Type destinationType)
			{
				if (destinationType is null)
					throw new ArgumentNullException(nameof(destinationType));

				Type underlyingTargetType;
				if (value == null)
				{
					if (destinationType.GetTypeInfo().IsValueType)
					{
						underlyingTargetType = Nullable.GetUnderlyingType(destinationType);
						if (underlyingTargetType != null)
							return null;

						throw new InvalidCastException($"Cannot cast null to '{destinationType.Name}'.");
					}

					return null;
				}

				Type sourceType = value.GetType();

				if (destinationType.GetTypeInfo().IsAssignableFrom(sourceType.GetTypeInfo()))
					return value;

				// We already know that the input value is not null, therefore the output won't be,
				// either.  If the target type is a nullable value type, we need to convert to the
				// underlying value type, which will end up being boxed upon return, anyway.
				underlyingTargetType = Nullable.GetUnderlyingType(destinationType);
				if (underlyingTargetType != null)
					return ConvertToType(value, underlyingTargetType);

				// Make a bilateral attempt to use a type converter.
				TypeConverter converter = TypeDescriptor.GetConverter(sourceType);
				if (converter != null && converter.CanConvertTo(destinationType))
					return converter.ConvertTo(value, destinationType);

				converter = TypeDescriptor.GetConverter(destinationType);
				if (converter != null && converter.CanConvertFrom(sourceType))
					return converter.ConvertFrom(value);

				// If all else fails, try using the default.  Chances are slim that this will actually
				// work after the two type converters didn't pan out but it just might work for types
				// that implement IConvertible but don't have type converter.
				return Convert.ChangeType(value, destinationType);
			}
		}
	}
}
