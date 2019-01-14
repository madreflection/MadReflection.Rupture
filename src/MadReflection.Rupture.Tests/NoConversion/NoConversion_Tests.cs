using System;
using NUnit.Framework;

namespace MadReflection.Rupture.Tests.NoConversion
{
	[TestFixture]
	public class NoConversion_Tests
	{
		[TestCase]
		public void Unbox_DataReader_Columns_with_Default_Handlers()
		{
			// Arrange

			// Act
			using (FakeDataReader reader = new FakeDataReader())
			{
				while (reader.Read())
				{
					int? refNullNull = reader.Column<int?>("null");

					bool boolean = reader.Column<bool>("Boolean");
					char ch = reader.Column<char>("Char");
					sbyte int8 = reader.Column<sbyte>("SByte");
					byte uint8 = reader.Column<byte>("Byte");
					short int16 = reader.Column<short>("Int16");
					ushort uint16 = reader.Column<ushort>("UInt16");
					int int32 = reader.Column<int>("Int32");
					uint uint32 = reader.Column<uint>("UInt32");
					long int64 = reader.Column<long>("Int64");
					ulong uint64 = reader.Column<ulong>("UInt64");
					float sng = reader.Column<float>("Single");
					double dbl = reader.Column<double>("Double");
					decimal dec = reader.Column<decimal>("Decimal");
					string str = reader.Column<string>("String");
					DateTime dt = reader.Column<DateTime>("DateTime");
					TimeSpan ts = reader.Column<TimeSpan>("TimeSpan");
					Guid guid = reader.Column<Guid>("Guid");
					Uri uri = reader.Column<Uri>("Uri");
				}
			}

			// Assert
		}
	}
}
