using System.Reflection;
using NUnitLite;

namespace MadReflection.Rupture.Tests.Properties
{
	internal static class Program
	{
		private static int Main(string[] args)
		{
			return new AutoRun(typeof(Program).GetTypeInfo().Assembly).Execute(args);
		}
	}
}
