using System;

namespace MadReflection.Rupture
{
	partial class ValueExtractor
	{
		private sealed class Configuration : ValueExtractorConfiguration
		{
			private bool _locked;
			private readonly ValueExtractor _extractor;


			public Configuration(ValueExtractor extractor)
			{
				_extractor = extractor;
			}


			internal override void Lock()
			{
				_locked = true;
			}

			public override void UseNullTester(INullTester nullTester)
			{
				if (nullTester is null)
					throw new ArgumentNullException(nameof(nullTester));

				_extractor._nullTester = nullTester;
			}

			public override void UseNullTester(Func<object, bool> isNullFunc)
			{
				if (isNullFunc is null)
					throw new ArgumentNullException(nameof(isNullFunc));

				_extractor._nullTester = new FunctorNullTester(isNullFunc);
			}

			public override void UseUnwrapper(IUnwrapper unwrapper)
			{
				if (unwrapper is null)
					throw new ArgumentNullException(nameof(unwrapper));

				_extractor._unwrapper = unwrapper;
			}

			public override void UseUnwrapper(Func<object, bool> isWrappedFunc, Func<object, object> unwrapFunc)
			{
				if (isWrappedFunc is null)
					throw new ArgumentNullException(nameof(isWrappedFunc));
				if (unwrapFunc is null)
					throw new ArgumentNullException(nameof(unwrapFunc));

				_extractor._unwrapper = new FunctorUnwrapper(isWrappedFunc, unwrapFunc);
			}

			public override void UseConverter(IConverter converter)
			{
				if (converter is null)
					throw new ArgumentNullException(nameof(converter));

				_extractor._converter = converter;
			}

			public override void UseConverter(Func<object, Type, object> convertToTypeFunc)
			{
				if (convertToTypeFunc is null)
					throw new ArgumentNullException(nameof(convertToTypeFunc));

				_extractor._converter = new FunctorConverter(convertToTypeFunc);
			}

			public override void UseHandler<T>(Func<object, T> handler)
			{
				if (handler is null)
					throw new ArgumentNullException(nameof(handler));

				Type type = typeof(T);
				ThrowIfTypeAlreadyConfigured(type);

				_extractor._extractors[typeof(T)] = handler;
			}

			public override void UseHandler<T>(Action<object, IExtractionContext<T>> handler)
			{
				if (handler is null)
					throw new ArgumentNullException(nameof(handler));

				Type type = typeof(T);
				ThrowIfTypeAlreadyConfigured(type);

				_extractor._extractors[type] = handler;
			}


			private void ThrowIfLocked()
			{
				if (_locked)
					throw new InvalidOperationException("Configuration is locked.");
			}

			private void ThrowIfAnyTypesConfigured()
			{
				if (_extractor._extractors.Count > 0)
					throw new InvalidOperationException("Cannot modify option properties after a type has been configured.");
			}

			private void ThrowIfTypeAlreadyConfigured(Type type)
			{
				if (_extractor._extractors.ContainsKey(type))
					throw new InvalidOperationException("Type is already configured.");
			}
		}
	}
}
