using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.Pricing;
using OkonkwoOandaV20.TradeLibrary.REST;
using System;

namespace OkonkwoOandaV20Tests.Framework.JsonConverters
{
   [TestClass]
   public class PricingStreamResponseConverterTests
   {
	  [TestMethod]
	  public void success_CanConvert_type_PricingStreamResponse()
	  {
		 // arrange
		 var converter = new PricingStreamResponseConverter();

		 // act
		 var canConvert = converter.CanConvert(typeof(PricingStreamResponse));

		 // assert
		 Assert.IsTrue(canConvert);
	  }

	  [TestMethod]
	  public void success_ReadJson_hydrates_PricingStreamResponse()
	  {
		 // arrange
		 var price = new Price()
		 {
			type = "PRICE",
			time = DateTime.UtcNow,
			instrument = "EUR_USD"
		 };

		 var jsonPrice = JsonConvert.SerializeObject(price);
		 var textReader = new System.IO.StringReader(jsonPrice);

		 var reader = new JsonTextReader(textReader);
		 var objectType = typeof(PricingStreamResponse);
		 object existingValue = null;
		 var serializer = new JsonSerializer();
		 var converter = new PricingStreamResponseConverter();

		 //act
		 var readJsonResult = converter.ReadJson(reader, objectType, existingValue, serializer);

		 //assert
		 if (readJsonResult is PricingStreamResponse pricingResponse)
		 {
			Assert.IsFalse(pricingResponse.IsHeartbeat());
			Assert.AreEqual(price.type, pricingResponse.price.type);
			Assert.AreEqual(price.time, pricingResponse.price.time);
			Assert.AreEqual(price.instrument, pricingResponse.price.instrument);
		 }
		 else
		 {
			Assert.Fail($"ReadJson result type '{readJsonResult.GetType().FullName}' is incorrect.");
		 }
	  }

	  [TestMethod]
	  public void success_ReadJson_hydrates_Hearbeat_object()
	  {
		 // arrange
		 var heartbeat = new PricingHeartbeat()
		 {
			type = "HEARTBEAT",
			time = DateTime.UtcNow
		 };

		 var jsonHeartbeat = JsonConvert.SerializeObject(heartbeat);
		 var textReader = new System.IO.StringReader(jsonHeartbeat);

		 var reader = new JsonTextReader(textReader);
		 var objectType = typeof(PricingStreamResponse);
		 object existingValue = null;
		 var serializer = new JsonSerializer();
		 var converter = new PricingStreamResponseConverter();

		 //act
		 var readJsonResult = converter.ReadJson(reader, objectType, existingValue, serializer);

		 //assert
		 if (readJsonResult is PricingStreamResponse pricingResponse)
		 {
			Assert.IsTrue(pricingResponse.IsHeartbeat());
			Assert.AreEqual(heartbeat.type, pricingResponse.heartbeat.type);
			Assert.AreEqual(heartbeat.time, pricingResponse.heartbeat.time);
		 }
		 else
		 {
			Assert.Fail($"ReadJson result type '{readJsonResult.GetType().FullName}' is incorrect.");
		 }
	  }
   }
}
