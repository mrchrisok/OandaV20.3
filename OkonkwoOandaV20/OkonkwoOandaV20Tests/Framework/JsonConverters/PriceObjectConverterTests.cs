using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.Instrument;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OkonkwoOandaV20Tests.Framework.JsonConverters
{
   [TestClass]
   public class PriceObjectConverterTests
   {
	  [TestMethod]
	  public void success_CanConvert_type_IHasPrices()
	  {
		 // arrange
		 var converter = new PriceObjectConverter();
		 var typesList = typeof(IHasPrices).Assembly.DefinedTypes
			.Where(type => type.ImplementedInterfaces.Contains(typeof(IHasPrices)));
		 var cannotConvertTypeList = new List<string>();

		 // act
		 foreach (var type in typesList)
		 {
			if (!converter.CanConvert(type))
			{
			   cannotConvertTypeList.Add(type.FullName);
			}
		 }
		 var cannotConvertTypeNames = Utilities.ConvertListToDelimitedValues(cannotConvertTypeList);

		 // assert
		 Assert.IsTrue(cannotConvertTypeList.Count == 0, $"Cannot convert types: {cannotConvertTypeNames}");
	  }

	  [TestMethod]
	  public void success_ReadJson_hydrates_IHasPrices_object()
	  {
		 // arrange
		 var converter = new PriceObjectConverter();
		 var instrument = new Instrument()
		 {
			name = "EUR_USD",
			displayPrecision = 5,
			minimumTradeSize = (decimal)0.1,
			tradeUnitsPrecision = 2
		 };
		 var takeProfit = new TakeProfitDetails(instrument)
		 {
			price = (decimal)125.25,
			timeInForce = "GTD"
		 };
		 var settings = new JsonSerializerSettings()
		 {
			NullValueHandling = NullValueHandling.Ignore
		 };
		 var jsonTakeProfit = JsonConvert.SerializeObject(takeProfit, settings);
		 var textReader = new System.IO.StringReader(jsonTakeProfit);

		 var reader = new JsonTextReader(textReader);
		 var objectType = typeof(TakeProfitDetails);
		 object existingValue = null;
		 var serializer = new JsonSerializer();

		 //act
		 var readJsonResult = converter.ReadJson(reader, objectType, existingValue, serializer);

		 //assert
		 if (readJsonResult is TakeProfitDetails takeProfitResult)
		 {
			Assert.AreEqual(takeProfit.price, takeProfitResult.price);
			Assert.AreEqual(takeProfit.timeInForce, takeProfitResult.timeInForce);
		 }
		 else
		 {
			Assert.Fail($"ReadJson result type '{readJsonResult.GetType().FullName}' is incorrect.");
		 }
	  }

	  [TestMethod]
	  public void success_WriteJson_writes_IHasPrices()
	  {
		 // arrange
		 var instrument = new Instrument()
		 {
			name = "EUR_USD",
			displayPrecision = 5,
			minimumTradeSize = (decimal)0.1,
			tradeUnitsPrecision = 2
		 };
		 var takeProfit = new TakeProfitDetails(instrument)
		 {
			price = (decimal)125.25,
			timeInForce = "GTD"
		 };

		 var stringBuilder = new StringBuilder();
		 var textWriter = new System.IO.StringWriter(stringBuilder);
		 var writer = new JsonTextWriter(textWriter);
		 var value = takeProfit;
		 var serializer = new JsonSerializer() { NullValueHandling = NullValueHandling.Ignore };
		 var converter = new PriceObjectConverter();
		 var settings = new JsonSerializerSettings()
		 {
			NullValueHandling = NullValueHandling.Ignore
		 };
		 var valueString = JsonConvert.SerializeObject(takeProfit, settings);

		 //act
		 converter.WriteJson(writer, value, serializer);
		 var stringBuilderObject = JObject.Parse(stringBuilder.ToString());
		 var valueStringObject = JObject.Parse(valueString);

		 //assert
		 Assert.IsTrue(stringBuilderObject["price"].ToString() == valueStringObject["price"].ToString());
		 Assert.IsTrue(stringBuilderObject["timeInForce"].ToString() == valueStringObject["timeInForce"].ToString());
	  }
   }
}
