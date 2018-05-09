namespace MadReflection.Rupture
{
	/// <summary>
	/// Represents the operations involved in unwrapping a value.
	/// </summary>
	/// <remarks>
	/// This facility is designed to enable unwrapping of types in the System.Data.SqlTypes
	/// namespace, such as extracting a nullable Int32 from a SqlInt32.
	/// </remarks>
	public interface IUnwrapper
	{
		/// <summary>
		/// Determines if a value is wrapped.
		/// </summary>
		/// <param name="value">The value to test.</param>
		/// <returns>If the value is wrapped, true; otherwise, false.</returns>
		/// <remarks>
		/// If the value that is wrapped is null, this should still return true.
		/// </remarks>
		bool IsWrapped(object value);

		/// <summary>
		/// Unwraps a value.
		/// </summary>
		/// <param name="value">The value to unwrap.</param>
		/// <returns>The value contained in the wrapper.</returns>
		/// <remarks>
		/// This method is only called if <see cref="IsWrapped(object)"/> returns true.  It should
		/// not need to consider the case that <paramref name="value"/> does not represent a wrapped
		/// value.
		/// </remarks>
		object Unwrap(object value);
	}
}
