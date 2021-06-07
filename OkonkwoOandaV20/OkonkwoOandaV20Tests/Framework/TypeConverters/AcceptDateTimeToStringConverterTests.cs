using Microsoft.VisualStudio.TestTools.UnitTesting;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.TypeConverters;
using OkonkwoOandaV20.TradeLibrary.REST;
using System;
using System.Text.RegularExpressions;

namespace OkonkwoOandaV20Tests.Framework.TypeConverters
{
   [TestClass]
   public class AcceptDateTimeToStringConverterTests
   {
	  [TestMethod]
	  public void success_CanConvert_type_DateTime()
	  {
		 // arrange
		 var acceptDateTimeFormat = AcceptDatetimeFormat.RFC3339;
		 var converter = new AcceptDateTimeToStringConverter(acceptDateTimeFormat);

		 // act
		 var canConvertDateTime = converter.CanConvert(typeof(DateTime));
		 var canConvertDateTimeNullable = converter.CanConvert(typeof(DateTime?));

		 // assert
		 Assert.IsTrue(canConvertDateTime);
		 Assert.IsTrue(canConvertDateTimeNullable);
	  }

	  [TestMethod]
	  public void failure_CanConvert_type_not_DateTime()
	  {
		 // arrange
		 var acceptDateTimeFormat = AcceptDatetimeFormat.RFC3339;
		 var converter = new AcceptDateTimeToStringConverter(acceptDateTimeFormat);

		 // act
		 var canConvertObject = converter.CanConvert(typeof(object));

		 // assert
		 Assert.IsFalse(canConvertObject);
	  }

	  [TestMethod]
	  public void success_ToOuput_RFC3339_returns_correct_value()
	  {
		 // arrange
		 var rgxDate = "([0-9]+)-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[01])";
		 var rgxHour = "([01][0-9]|2[0-3])";
		 var rgxMinute = "([0-5][0-9])";
		 var rgxSeconds = "([0-5][0-9]|60)";
		 var rgxMilliSeconds = "(\\.[0-9]+)";
		 var rgxOffset = "([\\+|\\-]([01][0-9]|2[0-3]):[0-5][0-9])";

		 var regexRFC3339 = new Regex($"^{rgxDate}[Tt]{rgxHour}:{rgxMinute}:{rgxSeconds}{rgxMilliSeconds}?(([Zz])|{rgxOffset})$");

		 var acceptDateTimeFormat = AcceptDatetimeFormat.RFC3339;
		 var converter = new AcceptDateTimeToStringConverter(acceptDateTimeFormat);
		 var testDateTime = DateTime.UtcNow;

		 // act
		 var convertedTestDateTime = converter.ToOutput(testDateTime);
		 var convertedToCorrectFormat = regexRFC3339.IsMatch(convertedTestDateTime);
		 var restoredTestDateTime = Utilities.ConvertAcceptDateFormatDateToDateTimeUtc(convertedTestDateTime, acceptDateTimeFormat);

		 // assert
		 Assert.IsTrue(convertedToCorrectFormat);
		 Assert.AreEqual(testDateTime, restoredTestDateTime);
	  }

	  [TestMethod]
	  public void success_ToOuput_Unix_returns_correct_value()
	  {
		 // arrange
		 var rgxSeconds = "([0-9]+)";
		 var rgxMilliSeconds = "([0-9]+)";

		 var regexUnix = new Regex($"^{rgxSeconds}.{rgxMilliSeconds}$");

		 var acceptDateTimeFormat = AcceptDatetimeFormat.Unix;
		 var converter = new AcceptDateTimeToStringConverter(acceptDateTimeFormat);
		 var testDateTime = DateTime.UtcNow;

		 // act
		 var convertedTestDateTime = converter.ToOutput(testDateTime);
		 var convertedToCorrectFormat = regexUnix.IsMatch(convertedTestDateTime);
		 var restoredTestDateTime = Utilities.ConvertAcceptDateFormatDateToDateTimeUtc(convertedTestDateTime, acceptDateTimeFormat);

		 // assert
		 Assert.IsTrue(convertedToCorrectFormat);
		 Assert.AreEqual(testDateTime.Date, restoredTestDateTime.Value.Date);
		 Assert.AreEqual(testDateTime.Hour, restoredTestDateTime.Value.Hour);
		 Assert.AreEqual(testDateTime.Minute, restoredTestDateTime.Value.Minute);
		 Assert.AreEqual(testDateTime.Second, restoredTestDateTime.Value.Second);
		 Assert.IsTrue(Math.Abs(testDateTime.Millisecond - restoredTestDateTime.Value.Millisecond) <= 1);
	  }
   }
}
