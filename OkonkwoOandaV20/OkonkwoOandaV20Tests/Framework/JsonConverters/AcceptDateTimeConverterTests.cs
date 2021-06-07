using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.REST;
using System;
using System.Text;

namespace OkonkwoOandaV20Tests.Framework.JsonConverters
{
   [TestClass]
   public class AcceptDateTimeConverterTests
   {
	  [TestMethod]
	  public void success_CanConvert_type_DateTime()
	  {
		 //arrange
		 var acceptDateTimeFormat = AcceptDatetimeFormat.RFC3339;
		 var converter = new AcceptDateTimeConverter(acceptDateTimeFormat);

		 //act
		 var canConvertDateTime = converter.CanConvert(typeof(DateTime));
		 var canConvertDateTimeNullable = converter.CanConvert(typeof(DateTime?));

		 //assert
		 Assert.IsTrue(canConvertDateTime);
		 Assert.IsTrue(canConvertDateTimeNullable);
	  }

	  [TestMethod]
	  public void success_WriteJson_writes_DateTime()
	  {
		 // arrange
		 var stringBuilder = new StringBuilder();
		 var textWriter = new System.IO.StringWriter(stringBuilder);
		 var writer = new JsonTextWriter(textWriter);
		 var value = DateTime.UtcNow;
		 var serializer = new JsonSerializer();
		 var acceptDateTimeFormat = AcceptDatetimeFormat.RFC3339;
		 var converter = new AcceptDateTimeConverter(acceptDateTimeFormat);
		 var valueString = Utilities.ConvertDateTimeUtcToAcceptDateFormat(value, acceptDateTimeFormat);

		 //act
		 converter.WriteJson(writer, value, serializer);

		 //assert
		 Assert.IsTrue(stringBuilder.ToString() == $"\"{valueString}\"");
	  }
   }
}
