using System;

namespace OkonkwoOandaV20.Framework.TypeConverters
{
   public interface ITypeConverter<TOutput>
   {
	  bool CanConvert(Type type);
	  TOutput ToOutput(object input);
   }
}
