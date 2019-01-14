using System;

namespace MadReflection.Rupture
{
	internal sealed class FunctorUnwrapper : IUnwrapper
	{
		private readonly Func<object, bool> _isWrappedFunc;
		private readonly Func<object, object> _unwrapFunc;


		public FunctorUnwrapper(Func<object, bool> isWrappedFunc, Func<object, object> unwrapFunc)
		{
			if (isWrappedFunc is null)
				throw new ArgumentNullException(nameof(isWrappedFunc));
			if (unwrapFunc is null)
				throw new ArgumentNullException(nameof(unwrapFunc));

			_isWrappedFunc = isWrappedFunc;
			_unwrapFunc = unwrapFunc;
		}


		public bool IsWrapped(object value) => _isWrappedFunc(value);

		public object Unwrap(object value) => _unwrapFunc(value);
	}
}
