using System;

namespace MadReflection.Rupture
{
	internal sealed class FunctorConverter : IConverter
	{
		private readonly Func<object, Type, object> _convertToTypeFunc;


		public FunctorConverter(Func<object,Type,object> convertToTypeFunc)
		{
			_convertToTypeFunc = convertToTypeFunc;
		}


		public object ConvertToType(object value, Type destinationType) => _convertToTypeFunc(value, destinationType);
	}
}
