using System;

namespace MadReflection.Rupture
{
	/// <summary>
	/// Represents the operation involved in converting a value to the desired type.
	/// </summary>
	public interface IConverter
	{
		/// <summary>
		/// Converts a value to a different type.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="destinationType">The desired type of the output.</param>
		/// <returns>The converted value.</returns>
		object ConvertToType(object value, Type destinationType);
	}
}
