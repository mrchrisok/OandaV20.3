using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.TradeLibrary.REST;
using System.Text.RegularExpressions;

namespace OkonkwoOandaV20Tests.Framework
{
   [TestClass]
   public class UtilitiesTests
   {
	  [TestMethod]
	  public void failure_ConvertDateTimeUtcToAcceptDateFormat_fails_when_not_DateTimeUtc()
	  {
		 // arrange
		 var acceptDateTimeFormat = AcceptDatetimeFormat.RFC3339;
		 var testDateTime = DateTime.Now;

		 Type exceptionType = null;

		 // act
		 try
		 {
			Utilities.ConvertDateTimeUtcToAcceptDateFormat(testDateTime, acceptDateTimeFormat);
		 }
		 catch (Exception ex)
		 {
			exceptionType = ex.GetType();
		 }

		 // assert
		 Assert.IsTrue(exceptionType == typeof(ArgumentException));
	  }

	  [TestMethod]
	  public void success_ConvertDateTimeUtcToAcceptDateFormat_returns_correct_Unix_value()
	  {
		 // arrange
		 var secondsPattern = "([0-9]+)";
		 var milliSecondsPattern = "([0-9]+)";

		 var regexUnix = new Regex($"^{secondsPattern}.{milliSecondsPattern}$");

		 var acceptDateTimeFormat = AcceptDatetimeFormat.Unix;
		 var testDateTime = DateTime.UtcNow;

		 // act
		 var convertedTestDateTime = Utilities.ConvertDateTimeUtcToAcceptDateFormat(testDateTime, acceptDateTimeFormat);
		 var convertedToCorrectFormat = regexUnix.IsMatch(convertedTestDateTime);

		 // assert
		 Assert.IsTrue(convertedToCorrectFormat);
	  }

	  [TestMethod]
	  public void success_ConvertAcceptDateFormatDateToDateTimeUtc_restores_RFC3339_value()
	  {
		 // arrange
		 var acceptDateTimeFormat = AcceptDatetimeFormat.RFC3339;
		 var testDateTime = DateTime.UtcNow;
		 var acceptTestDateTime = Utilities.ConvertDateTimeUtcToAcceptDateFormat(testDateTime, acceptDateTimeFormat);

		 // act
		 var restoredTestDateTime = Utilities.ConvertAcceptDateFormatDateToDateTimeUtc(acceptTestDateTime, acceptDateTimeFormat);

		 // assert
		 Assert.AreEqual(testDateTime, restoredTestDateTime);
	  }

	  [TestMethod]
	  public void success_ConvertDateTimeUtcToAcceptDateFormat_restores_Unix_value()
	  {
		 // arrange
		 var acceptDateTimeFormat = AcceptDatetimeFormat.Unix;
		 var testDateTime = DateTime.UtcNow;
		 var acceptTestDateTime = Utilities.ConvertDateTimeUtcToAcceptDateFormat(testDateTime, acceptDateTimeFormat);

		 // act
		 var restoredTestDateTime = Utilities.ConvertAcceptDateFormatDateToDateTimeUtc(acceptTestDateTime, acceptDateTimeFormat);

		 // assert
		 Assert.AreEqual(testDateTime.Date, restoredTestDateTime?.Date);
		 Assert.AreEqual(testDateTime.Hour, restoredTestDateTime?.Hour);
		 Assert.AreEqual(testDateTime.Minute, restoredTestDateTime?.Minute);
		 Assert.AreEqual(testDateTime.Second, restoredTestDateTime?.Second);
		 Assert.IsTrue(Math.Abs(testDateTime.Millisecond - restoredTestDateTime.Value.Millisecond) <= 1);
	  }
   }
}
