using System;

namespace MadReflection.Rupture
{
	/// <summary>
	/// Represents the operation involved in testing if a value is null-like.
	/// </summary>
	public interface INullTester
	{
		/// <summary>
		/// Determines if an object is a null-like value.
		/// </summary>
		/// <param name="value">The object to test.</param>
		/// <returns>If value represents a null-like value, true; otherwise, false.</returns>
		bool IsNull(object value);
	}
}
