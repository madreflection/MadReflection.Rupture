using System;
using System.Data;

namespace MadReflection.Rupture.Tests
{
	public class FakeDataReader : IDataReader
	{
		#region FakeDataReader members
		private static readonly string[] _names = new string[]  { "null", "DBNull",     "String", "Boolean", "Char", "SByte",  "Byte",  "Int16",  "UInt16",  "Int32", "UInt32", "Int64", "UInt64", "Single", "Double", "Decimal", "DateTime",                "TimeSpan",               "DateString", "TimeString", "Guid",     "Uri",                             "UriString",              "YesNoString" };
		private static readonly object[] _values = new object[] { null,   DBNull.Value, "Hello!", true,      '!',    (sbyte)1, (byte)1, (short)1, (ushort)1, 1,       1U,       1L,      1UL,      1.0F,     1.0D,     1.0M,      new DateTime(2017, 1, 23), new TimeSpan(12, 34, 56), "2017-1-24",  "11:08:54"  , Guid.Empty, new Uri("http://www.google.com/"), "http://www.google.com/", "Y"           };

		private int _recordCount;
		private int _recordsAffected;


		public FakeDataReader()
			: this(1)
		{
		}

		public FakeDataReader(int recordCount)
		{
			_recordCount = Math.Max(recordCount, 0);
		}


		private object InternalGetValue(int i) => _values[i];

		private T InternalGetValue<T>(int i) => (T)InternalGetValue(i);
		#endregion


		#region IDataReader members
		public int Depth => 0;

		public bool IsClosed => false;

		public int RecordsAffected => _recordsAffected;


		public void Close()
		{
		}

		public DataTable GetSchemaTable() => throw new NotImplementedException();

		public bool NextResult()
		{
			_recordCount = -1;
			return false;
		}

		public bool Read()
		{
			if (_recordCount < 0)
				return false;

			++_recordsAffected;

			return _recordCount-- > 0;
		}
		#endregion


		#region IDataRecord members
		public object this[string name] => InternalGetValue(GetOrdinal(name));

		public object this[int i] => InternalGetValue(i);

		public int FieldCount => _values.Length;


		public bool GetBoolean(int i) => InternalGetValue<bool>(i);

		public byte GetByte(int i) => InternalGetValue<byte>(i);

		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length) => throw new NotImplementedException();

		public char GetChar(int i) => InternalGetValue<char>(i);

		public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length) => throw new NotImplementedException();

		public IDataReader GetData(int i) => throw new NotImplementedException();

		public string GetDataTypeName(int i) => GetFieldType(i).Name;

		public DateTime GetDateTime(int i) => InternalGetValue<DateTime>(i);

		public decimal GetDecimal(int i) => InternalGetValue<decimal>(i);

		public double GetDouble(int i) => InternalGetValue<double>(i);

		public Type GetFieldType(int i) => _values[i]?.GetType() ?? typeof(object);

		public float GetFloat(int i) => InternalGetValue<float>(i);

		public Guid GetGuid(int i) => InternalGetValue<Guid>(i);

		public short GetInt16(int i) => InternalGetValue<short>(i);

		public int GetInt32(int i) => InternalGetValue<int>(i);

		public long GetInt64(int i) => InternalGetValue<long>(i);

		public string GetName(int i) => _names[i];

		public int GetOrdinal(string name)
		{
			int index = Array.IndexOf(_names, name);
			if (index < 0)
				throw new ArgumentException();

			return index;
		}

		public string GetString(int i) => InternalGetValue<string>(i);

		public object GetValue(int i) => InternalGetValue(i);

		public int GetValues(object[] values)
		{
			if (values is null)
				throw new ArgumentNullException(nameof(values));

			int count = Math.Min(values.Length, _values.Length);

			for(int i = 0; i < count; ++i)
				values[i] = _values[i];

			return count;
		}

		public bool IsDBNull(int i) => InternalGetValue(i) is DBNull;
		#endregion


		#region IDisposable members
		public void Dispose()
		{
		}
		#endregion
	}
}
