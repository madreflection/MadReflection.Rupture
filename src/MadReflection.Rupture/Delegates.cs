namespace MadReflection.Rupture
{
	/// <summary>
	/// A delegate type for a value extractor function.
	/// </summary>
	/// <param name="configuration">The configuration object that configures a value extractor.</param>
	public delegate void ValueExtractorConfigurator(ValueExtractorConfiguration configuration);

	internal delegate T ExtractorFunc<T>(object value);
}
