using OkonkwoOandaV20.Framework.TypeConverters;
using System;

namespace OkonkwoOandaV20.Framework
{
   public class HeaderAttribute : RequestAttribute
   {
	  public HeaderAttribute(string name = null, Type converter = null)
		 : base(name, converter)
	  {
	  }

	  public override object GetValue(object value)
	  {
		 if (Converter != null)
		 {
			var converter = Activator.CreateInstance(Converter);

			if (converter is ITypeConverter<string> typeConverter)
			{
			   var converterValue = typeConverter.ToOutput(value);

			   return converterValue;
			}
		 }

		 return value;
	  }
   }
}
