using System;

namespace MadReflection.Rupture
{
	internal sealed class FunctorNullTester : INullTester
	{
		private readonly Func<object, bool> _isNullFunc;


		public FunctorNullTester(Func<object,bool> isNullFunc)
		{
			if (isNullFunc == null)
				throw new ArgumentNullException(nameof(isNullFunc));

			_isNullFunc = isNullFunc;
		}


		public bool IsNull(object value) => _isNullFunc(value);
	}
}
