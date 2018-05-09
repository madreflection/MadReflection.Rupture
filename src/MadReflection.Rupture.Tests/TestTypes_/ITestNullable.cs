using System;
using System.Collections.Generic;
using System.Text;

namespace MadReflection.Rupture.Tests
{
	// Similar to System.Data.SqlClient.INullable but it has a Value property, thank you.
	public interface ITestNullable
	{
		bool IsNull { get; }
		object Value { get; }
	}
}
