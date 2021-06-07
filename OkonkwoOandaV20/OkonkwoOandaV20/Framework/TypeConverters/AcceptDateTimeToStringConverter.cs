using OkonkwoOandaV20.TradeLibrary.REST;
using System;

namespace OkonkwoOandaV20.Framework.TypeConverters
{
   public class AcceptDateTimeToStringConverter : ITypeConverter<string>
   {
	  public AcceptDateTimeToStringConverter(AcceptDatetimeFormat dateTimeFormat)
	  {
		 _acceptDateTimeFormat = dateTimeFormat;
	  }

	  private AcceptDatetimeFormat _acceptDateTimeFormat;

	  public bool CanConvert(Type objectType)
	  {
		 return objectType == typeof(DateTime) || objectType == typeof(DateTime?);
	  }

	  /// <summary>
	  /// Converts a DateTime value to a formatted string.
	  /// The converted value is accurate within 1 milliSecond.
	  /// </summary>
	  /// <param name="input">The DateTime to be converted to a string</param>
	  /// <returns>A formatted string representation of the input(DateTime).</returns>
	  public string ToOutput(object input)
	  {
		 if (input == null)
		 {
			return null;
		 }

		 if (input is DateTime dateTime)
		 {
			var output = Utilities.ConvertDateTimeUtcToAcceptDateFormat(dateTime, _acceptDateTimeFormat);

			return output;
		 }

		 throw new ArgumentException($"The input type '{input.GetType().Name}' is invalid.");
	  }
   }
}
