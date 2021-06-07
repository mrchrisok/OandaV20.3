using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OkonkwoOandaV20.TradeLibrary.REST;
using System;

namespace OkonkwoOandaV20.Framework.JsonConverters
{
   public class AcceptDateTimeConverter : AbstractJsonConverter
   {

	  public AcceptDateTimeConverter(AcceptDatetimeFormat dateTimeFormat)
	  {
		 _acceptDateTimeFormat = dateTimeFormat;
	  }

	  private AcceptDatetimeFormat _acceptDateTimeFormat;

	  public override bool CanConvert(Type objectType)
	  {
		 return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
	  }

	  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	  {
		 var jsonToken = JToken.Load(reader);
		 if (jsonToken.Type == JTokenType.Null)
		 {
			return null;
		 }

		 var dateTimeValue = reader.Value.ToString();
		 var dateTime = Utilities.ConvertAcceptDateFormatDateToDateTimeUtc(dateTimeValue, _acceptDateTimeFormat);

		 return dateTime;
	  }

	  public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	  {
		 var convertedTime = Utilities.ConvertDateTimeUtcToAcceptDateFormat((DateTime)value, _acceptDateTimeFormat);

		 writer.WriteValue(convertedTime);
	  }
   }
}
