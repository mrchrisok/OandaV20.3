using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.Order;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Collections.Generic;
using System.Linq;

namespace OkonkwoOandaV20Tests.Framework.JsonConverters
{
   [TestClass]
   public class OrderConverterTests
   {
	  [TestMethod]
	  public void success_CanConvert_type_IOrder()
	  {
		 // arrange
		 var converter = new OrderConverter();
		 var iOrderTypes = typeof(IOrder).Assembly.DefinedTypes
			.Where(type => type.ImplementedInterfaces.Contains(typeof(IOrder)));
		 var cannotConvertTypeList = new List<string>();
		 var iOrderList = new List<IOrder>() { };

		 // act
		 foreach (var type in iOrderTypes)
		 {
			if (!converter.CanConvert(type))
			{
			   cannotConvertTypeList.Add(type.FullName);
			}
		 }
		 var cannotConvertTypeNames = Utilities.ConvertListToDelimitedValues(cannotConvertTypeList);
		 var canConvertList = converter.CanConvert(typeof(List<IOrder>));

		 // assert
		 Assert.IsTrue(cannotConvertTypeList.Count == 0, $"Cannot convert types: {cannotConvertTypeNames}");
		 Assert.IsTrue(canConvertList, $"Cannot convert type '{typeof(List<IOrder>).FullName}'");
	  }

	  [TestMethod]
	  public void success_ReadJson_hydrates_IOrder_object()
	  {
		 // arrange
		 var converter = new OrderConverter();
		 var order = new LimitOrder()
		 {
			id = 1,
			//type = OrderType.Limit,
			instrument = "EUR_USD",
			price = (decimal)125.25,
			units = (decimal)25.6,
			timeInForce = "GTD",
			triggerCondition = "DEFAULT"
		 };

		 var jsonOrder = JsonConvert.SerializeObject(order);
		 var textReader = new System.IO.StringReader(jsonOrder);

		 var reader = new JsonTextReader(textReader);
		 var objectType = typeof(LimitOrderTransaction);
		 object existingValue = null;
		 var serializer = new JsonSerializer();

		 //act
		 var readJsonResult = converter.ReadJson(reader, objectType, existingValue, serializer);

		 //assert
		 if (readJsonResult is LimitOrder limitOrderResult)
		 {
			Assert.AreEqual(order.id, limitOrderResult.id);
			Assert.AreEqual(order.type, limitOrderResult.type);
			Assert.AreEqual(order.instrument, limitOrderResult.instrument);
			Assert.AreEqual(order.price, limitOrderResult.price);
			Assert.AreEqual(order.units, limitOrderResult.units);
			Assert.AreEqual(order.timeInForce, limitOrderResult.timeInForce);
			Assert.AreEqual(order.triggerCondition, limitOrderResult.triggerCondition);
		 }
		 else
		 {
			Assert.Fail($"ReadJson result type '{readJsonResult.GetType().FullName}' is incorrect.");
		 }
	  }

	  [TestMethod]
	  public void success_ReadJson_hydrates_IOrder_list()
	  {
		 // arrange
		 var converter = new OrderConverter();
		 var order = new LimitOrder()
		 {
			id = 1,
			//type = OrderType.Limit,
			instrument = "EUR_USD",
			price = (decimal)125.25,
			units = (decimal)25.6,
			timeInForce = "GTD",
			triggerCondition = "DEFAULT"
		 };
		 var orderList = new List<IOrder>() { order };

		 var jsonOrder = JsonConvert.SerializeObject(orderList);
		 var textReader = new System.IO.StringReader(jsonOrder);

		 var reader = new JsonTextReader(textReader);
		 var objectType = typeof(LimitOrderTransaction);
		 object existingValue = null;
		 var serializer = new JsonSerializer();

		 //act
		 var readJsonResult = converter.ReadJson(reader, objectType, existingValue, serializer);

		 //assert
		 if (readJsonResult is List<IOrder> limitOrderResults)
		 {
			foreach (LimitOrder limitOrderResult in limitOrderResults)
			{
			   Assert.AreEqual(order.id, limitOrderResult.id);
			   Assert.AreEqual(order.type, limitOrderResult.type);
			   Assert.AreEqual(order.instrument, limitOrderResult.instrument);
			   Assert.AreEqual(order.price, limitOrderResult.price);
			   Assert.AreEqual(order.units, limitOrderResult.units);
			   Assert.AreEqual(order.timeInForce, limitOrderResult.timeInForce);
			   Assert.AreEqual(order.triggerCondition, limitOrderResult.triggerCondition);
			}
		 }
		 else
		 {
			Assert.Fail($"ReadJson result type '{readJsonResult.GetType().FullName}' is incorrect.");
		 }
	  }
   }
}
