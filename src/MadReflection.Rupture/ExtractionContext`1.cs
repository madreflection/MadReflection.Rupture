using System;

namespace MadReflection.Rupture
{
	internal class ExtractionContext<T> : IExtractionContext<T>
	{
		private readonly ValueExtractor _extractor;

		private T _result;
		private bool _resultAssigned;


		internal ExtractionContext(ValueExtractor extractor)
		{
			_extractor = extractor;

			_result = default;
			_resultAssigned = false;
		}


		public T Result
		{
			internal get => _result;
			set
			{
				_result = value;
				_resultAssigned = true;
			}
		}

		internal bool ResultAssigned => _resultAssigned;


		public void ThrowInvalidCastException(Type sourceType)
		{
			throw ValueExtractor.InvalidCast(sourceType, typeof(T));
		}

		public void ThrowCannotCastNullToValueType()
		{
			throw ValueExtractor.CannotCastNullToValueType(typeof(T));
		}
	}
}
