using System;
using System.Collections.Generic;
using System.Reflection;

namespace MadReflection.Rupture
{
	/// <summary>
	/// Provides value extraction functionality.
	/// </summary>
	public partial class ValueExtractor
	{
		private INullTester _nullTester;
		private IUnwrapper _unwrapper;
		private IConverter _converter;
		private readonly Dictionary<Type, Delegate> _extractors = new Dictionary<Type, Delegate>();


		private ValueExtractor()
		{
		}


		/// <summary>
		/// Gets a <see cref="ValueExtractor"/> instance with the default configuration.
		/// </summary>
		public static readonly ValueExtractor Default = Create(config => { });

		/// <summary>
		/// Creates a <see cref="ValueExtractor"/> instance configured using the provided configuration method.
		/// </summary>
		/// <param name="configurator">A method that sets configuration options for a new <see cref="ValueExtractor"/> instance.</param>
		/// <returns>A new <see cref="ValueExtractor"/> instance.</returns>
		public static ValueExtractor Create(ValueExtractorConfigurator configurator)
		{
			if (configurator == null)
				throw new ArgumentNullException(nameof(configurator));

			ValueExtractor extractor = new ValueExtractor();

			configurator(new Configuration(extractor));

			return extractor;
		}


		/// <summary>
		/// Extracts a value.
		/// </summary>
		/// <typeparam name="T">The type of the value to extract.</typeparam>
		/// <param name="value">The value to examine.</param>
		/// <returns></returns>
		public T Extract<T>(object value)
		{
			ExtractorFunc<T> extract = GetFinalExtractor<T>();

			return extract(value);
		}

		private ExtractorFunc<T> GetFinalExtractor<T>()
		{
			Type type = typeof(T);

			if (!_extractors.TryGetValue(type, out Delegate del) || !(del is ExtractorFunc<T> actual))
			{
				lock (_extractors)
				{
					_extractors.TryGetValue(type, out del);

					actual = del as ExtractorFunc<T>;
					if (actual == null)
					{
						MethodInfo factoryMethod = null;
						if (Nullable.GetUnderlyingType(type) is Type underlyingValueType)
							factoryMethod = ReflectionHelper.GetPrivateGenericMethod(typeof(ValueExtractor), nameof(GetNullableValueTypeExtractor), underlyingValueType);
						else if (type.GetTypeInfo().IsValueType)
							factoryMethod = ReflectionHelper.GetPrivateGenericMethod(typeof(ValueExtractor), nameof(GetValueTypeExtractor), type);
						else
							factoryMethod = ReflectionHelper.GetPrivateGenericMethod(typeof(ValueExtractor), nameof(GetReferenceTypeExtractor), type);

						Func<Delegate, ExtractorFunc<T>> factory = factoryMethod.CreateDelegate<Func<Delegate, ExtractorFunc<T>>>(this);
						actual = factory(del);
						_extractors[type] = actual;
					}
				}
			}

			return actual;
		}

		private ExtractorFunc<T?> GetNullableValueTypeExtractor<T>(Delegate del)
			where T : struct
		{
			Func<object, T> basicHandler = GetBasicHandler<T>(del);

			return value =>
			{
				if (value is null || IsNull(value))
					return null;

				if (IsWrapped(value))
				{
					value = Unwrap(value);
					if (value is null || IsNull(value))
						return null;
				}

				return basicHandler(value);
			};
		}

		private ExtractorFunc<T> GetValueTypeExtractor<T>(Delegate del)
			where T : struct
		{
			Func<object, T> basicHandler = GetBasicHandler<T>(del);

			return value =>
			{
				if (value is null || IsNull(value))
					throw CannotCastNullToValueType(typeof(T));

				if (IsWrapped(value))
				{
					value = Unwrap(value);
					if (value is null || IsNull(value))
						throw CannotCastNullToValueType(typeof(T));
				}

				return basicHandler(value);
			};
		}

		private ExtractorFunc<T> GetReferenceTypeExtractor<T>(Delegate del)
			where T : class
		{
			Func<object, T> basicHandler = GetBasicHandler<T>(del);

			return value =>
			{
				if (value is null || IsNull(value))
					return null;

				if (IsWrapped(value))
				{
					value = Unwrap(value);
					if (value is null || IsNull(value))
						return null;
				}

				return basicHandler(value);
			};
		}

		private Func<object, T> GetBasicHandler<T>(Delegate del)
		{
			if (del is null)
				return value => (T)value;

			if (del is Func<object, T>)
				return (Func<object, T>)del;

			if (del is Action<object, IExtractionContext<T>> contextualHandler)
			{
				return value =>
				{
					var context = new ExtractionContext<T>(this);

					contextualHandler(value, context);

					if (context.ResultAssigned)
						return context.Result;

					throw InvalidCast(value.GetType(), typeof(T));
				};
			}

			throw new InvalidOperationException("Something went very wrong internally.");
		}


		private bool IsNull(object value) => _nullTester != null && _nullTester.IsNull(value);

		private bool IsWrapped(object value) => _unwrapper != null && _unwrapper.IsWrapped(value);

		private object Unwrap(object value) => _unwrapper != null ? _unwrapper.Unwrap(value) : value;

		internal static InvalidCastException InvalidCast(Type sourceType, Type destinationType) => new InvalidCastException($"Unable to cast from '{sourceType.Name}' to '{destinationType.Name}'");

		internal static InvalidCastException CannotCastNullToValueType(Type destinationType) => new InvalidCastException($"Cannot cast null to value type '{destinationType.Name}'.");
	}
}
