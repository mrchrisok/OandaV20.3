using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OkonkwoOandaV20Tests.Framework.JsonConverters
{
   [TestClass]
   public class TransactionConverterTests
   {
	  [TestMethod]
	  public void success_CanConvert_type_ITransaction()
	  {
		 // arrange
		 var limitOrderTansaction = new LimitOrderTransaction()
		 {
			instrument = "EUR_USD",
			price = (decimal)125.25,
			timeInForce = "GTD",
			triggerCondition = "DEFAULT"
		 };
		 var converter = new TransactionConverter();

		 var iTransactionTypes = typeof(ITransaction).Assembly.DefinedTypes
			.Where(type => type.ImplementedInterfaces.Contains(typeof(ITransaction)));

		 var cannotConvertTypeList = new List<string>();

		 // act
		 foreach (var type in iTransactionTypes)
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
	  public void success_ReadJson_hydrates_ITransaction_object()
	  {
		 // arrange
		 var converter = new TransactionConverter();
		 var transaction = new LimitOrderTransaction()
		 {
			id = 1,
			//type = TransactionType.LimitOrder,
			time = DateTime.UtcNow,
			userID = 123,
			accountID = "fakeAccountID",
			batchID = 123,
			requestID = "fakeRequestID",
			//
			instrument = "EUR_USD",
			price = (decimal)125.25,
			timeInForce = "GTD",
			triggerCondition = "DEFAULT"
		 };
		 var transactionList = new List<ITransaction>() { transaction };

		 var jsonTransaction = JsonConvert.SerializeObject(transaction);
		 var textReader = new System.IO.StringReader(jsonTransaction);

		 var reader = new JsonTextReader(textReader);
		 var objectType = typeof(LimitOrderTransaction);
		 object existingValue = null;
		 var serializer = new JsonSerializer();

		 //act
		 var readJsonResult = converter.ReadJson(reader, objectType, existingValue, serializer);

		 //assert
		 if (readJsonResult is LimitOrderTransaction limitOrderResult)
		 {
			Assert.AreEqual(transaction.id, limitOrderResult.id);
			Assert.AreEqual(transaction.type, limitOrderResult.type);
			Assert.AreEqual(transaction.time, limitOrderResult.time);
			Assert.AreEqual(transaction.userID, limitOrderResult.userID);
			Assert.AreEqual(transaction.accountID, limitOrderResult.accountID);
			Assert.AreEqual(transaction.batchID, limitOrderResult.batchID);
			Assert.AreEqual(transaction.requestID, limitOrderResult.requestID);
			//
			Assert.AreEqual(transaction.instrument, limitOrderResult.instrument);
			Assert.AreEqual(transaction.price, limitOrderResult.price);
			Assert.AreEqual(transaction.timeInForce, limitOrderResult.timeInForce);
			Assert.AreEqual(transaction.triggerCondition, limitOrderResult.triggerCondition);
		 }
		 else
		 {
			Assert.Fail($"ReadJson result type '{readJsonResult.GetType().FullName}' is incorrect.");
		 }
	  }

	  [TestMethod]
	  public void success_ReadJson_hydrates_ITransaction_list()
	  {
		 // arrange
		 var converter = new TransactionConverter();
		 var transaction = new LimitOrderTransaction()
		 {
			id = 1,
			//type = TransactionType.LimitOrder,
			time = DateTime.UtcNow,
			userID = 123,
			accountID = "fakeAccountID",
			batchID = 123,
			requestID = "fakeRequestID",
			//
			instrument = "EUR_USD",
			price = (decimal)125.25,
			timeInForce = "GTD",
			triggerCondition = "DEFAULT"
		 };
		 var transactionList = new List<ITransaction>() { transaction };

		 var jsonTransaction = JsonConvert.SerializeObject(transactionList);
		 var textReader = new System.IO.StringReader(jsonTransaction);

		 var reader = new JsonTextReader(textReader);
		 var objectType = typeof(LimitOrderTransaction);
		 object existingValue = null;
		 var serializer = new JsonSerializer();

		 //act
		 var readJsonResult = converter.ReadJson(reader, objectType, existingValue, serializer);

		 //assert
		 if (readJsonResult is List<ITransaction> limitOrderResults)
		 {
			foreach (LimitOrderTransaction limitOrderResult in limitOrderResults)
			{
			   Assert.AreEqual(transaction.id, limitOrderResult.id);
			   Assert.AreEqual(transaction.type, limitOrderResult.type);
			   Assert.AreEqual(transaction.time, limitOrderResult.time);
			   Assert.AreEqual(transaction.userID, limitOrderResult.userID);
			   Assert.AreEqual(transaction.accountID, limitOrderResult.accountID);
			   Assert.AreEqual(transaction.batchID, limitOrderResult.batchID);
			   Assert.AreEqual(transaction.requestID, limitOrderResult.requestID);
			   //
			   Assert.AreEqual(transaction.instrument, limitOrderResult.instrument);
			   Assert.AreEqual(transaction.price, limitOrderResult.price);
			   Assert.AreEqual(transaction.timeInForce, limitOrderResult.timeInForce);
			   Assert.AreEqual(transaction.triggerCondition, limitOrderResult.triggerCondition);
			}
		 }
		 else
		 {
			Assert.Fail($"ReadJson result type '{readJsonResult.GetType().FullName}' is incorrect.");
		 }
	  }
   }
}
