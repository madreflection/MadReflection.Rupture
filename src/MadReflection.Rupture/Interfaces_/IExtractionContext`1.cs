using System;

namespace MadReflection.Rupture
{
	/// <summary>
	/// Represents the context of a value extraction for use by a handler.
	/// </summary>
	/// <typeparam name="T">The desired output type.</typeparam>
	public interface IExtractionContext<T>
	{
		/// <summary>
		/// Sets the result of a value extraction handler.
		/// </summary>
		T Result { set; }


		/// <summary>
		/// Throws a general <see cref="InvalidCastException"/>.
		/// </summary>
		/// <param name="sourceType">The type of the value before the conversion attempt</param>
		/// <remarks>
		/// This method is intended to allow handlers to throw exceptions that are consistent with
		/// those thrown internally by the library.
		/// </remarks>
		void ThrowInvalidCastException(Type sourceType);

		/// <summary>
		/// Throws an <see cref="InvalidCastException"/> when a handler cannot be cast null to a value type.
		/// </summary>
		/// <remarks>
		/// This method is intended to allow handlers to throw exceptions that are consistent with
		/// those thrown internally by the library.
		/// </remarks>
		void ThrowCannotCastNullToValueType();
	}
}
