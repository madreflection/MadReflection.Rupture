using System;

namespace MadReflection.Rupture
{
	/// <summary>
	/// An object that can be used to configure <see cref="ValueExtractor"/> objects.
	/// </summary>
	public abstract class ValueExtractorConfiguration
	{
		internal ValueExtractorConfiguration()
		{
		}


		internal abstract void Lock();

		/// <summary>
		/// Configures value extraction to perform checking for null-like values.
		/// </summary>
		/// <param name="nullTester">An object that implements null-testing functionality.</param>
		/// <remarks>
		/// The provided null tester does not need to test for null references as it will never
		/// receive them.  This is designed to enable testing for non-null representations of null
		/// such as DBNull.
		/// </remarks>
		public abstract void UseNullTester(INullTester nullTester);

		/// <summary>
		/// Configures value extraction to perform checking for null-like values.
		/// </summary>
		/// <param name="isNullFunc">A function that implements null-testing functionality.</param>
		/// <remarks>
		/// The provided null tester does not need to test for null references as it will never
		/// receive them.  This is designed to enable testing for non-null representations of null
		/// such as DBNull.
		/// </remarks>
		public abstract void UseNullTester(Func<object, bool> isNullFunc);

		/// <summary>
		/// Configures unwrapping functionality.
		/// </summary>
		/// <param name="unwrapper">An object that implements unwrapping functionality.</param>
		public abstract void UseUnwrapper(IUnwrapper unwrapper);

		/// <summary>
		/// Configures unwrapping functionality.
		/// </summary>
		/// <param name="isWrappedFunc">A function that tests if a value is wrapped in another object.</param>
		/// <param name="unwrapFunc">A function that unwraps</param>
		/// <remarks>
		/// <paramref name="unwrapFunc"/> is only called if <paramref name="isWrappedFunc"/> returns true.
		/// </remarks>
		public abstract void UseUnwrapper(Func<object, bool> isWrappedFunc, Func<object, object> unwrapFunc);

		/// <summary>
		/// Configures type conversion functionality.
		/// </summary>
		/// <param name="converter">An object that performs type conversion.</param>
		/// <remarks>
		/// The <see cref="ExtractionConverters"/> class provides singleton implementations with different
		/// levels of conversion functionality.
		/// </remarks>
		public abstract void UseConverter(IConverter converter);

		/// <summary>
		/// Configures type conversion functionality.
		/// </summary>
		/// <param name="convertToTypeFunc">A function that performs type conversion.</param>
		public abstract void UseConverter(Func<object, Type, object> convertToTypeFunc);

		/// <summary>
		/// Configures value extraction for a type.
		/// </summary>
		/// <typeparam name="T">The type of the value for which to configure value extraction.</typeparam>
		/// <param name="handler">A value extraction handler for <typeparamref name="T"/>.</param>
		public abstract void UseHandler<T>(Func<object, T> handler);

		/// <summary>
		/// Configures value extraction for a type.
		/// </summary>
		/// <typeparam name="T">The type of the value for which to configure value extraction.</typeparam>
		/// <param name="handler">A value extraction handler for <typeparamref name="T"/>.</param>
		/// <remarks>
		/// The handler receives a context that it uses to set the result or use internal facilities
		/// such as throwing exceptions identical to those thrown internally.
		/// </remarks>
		public abstract void UseHandler<T>(Action<object, IExtractionContext<T>> handler);
	}
}
