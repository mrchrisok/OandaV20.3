using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.REST;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System;

namespace OkonkwoOandaV20Tests.Framework.JsonConverters
{
   [TestClass]
   public class TransactionStreamResponseConverterTests
   {
	  [TestMethod]
	  public void success_CanConvert_type_TransactionsStreamResponse()
	  {
		 // arrange
		 var converter = new TransactionsStreamResponseConverter();

		 // act
		 var canConvert = converter.CanConvert(typeof(TransactionsStreamResponse));

		 // assert
		 Assert.IsTrue(canConvert);
	  }

	  [TestMethod]
	  public void success_ReadJson_hydrates_ITransaction_object()
	  {
		 // arrange
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
		 var heartbeat = new TransactionsHeartbeat()
		 {
			type = "HEARTBEAT",
			lastTransactionID = 123,
			time = DateTime.UtcNow
		 };

		 var jsonTransaction = JsonConvert.SerializeObject(transaction);
		 var textReader = new System.IO.StringReader(jsonTransaction);

		 var reader = new JsonTextReader(textReader);
		 var objectType = typeof(LimitOrderTransaction);
		 object existingValue = null;
		 var serializer = new JsonSerializer();
		 var converter = new TransactionsStreamResponseConverter();

		 //act
		 var readJsonResult = converter.ReadJson(reader, objectType, existingValue, serializer);

		 //assert
		 if (readJsonResult is TransactionsStreamResponse transactionResponse)
		 {
			Assert.IsFalse(transactionResponse.IsHeartbeat());
			Assert.AreEqual(transaction.id, transactionResponse.transaction.id);
			Assert.AreEqual(transaction.type, transactionResponse.transaction.type);
			Assert.AreEqual(transaction.time, transactionResponse.transaction.time);
			Assert.AreEqual(transaction.userID, transactionResponse.transaction.userID);
			Assert.AreEqual(transaction.accountID, transactionResponse.transaction.accountID);
			Assert.AreEqual(transaction.batchID, transactionResponse.transaction.batchID);
			Assert.AreEqual(transaction.requestID, transactionResponse.transaction.requestID);
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
		 var heartbeat = new TransactionsHeartbeat()
		 {
			type = "HEARTBEAT",
			lastTransactionID = 123,
			time = DateTime.UtcNow
		 };

		 var jsonHeartbeat = JsonConvert.SerializeObject(heartbeat);
		 var textReader = new System.IO.StringReader(jsonHeartbeat);

		 var reader = new JsonTextReader(textReader);
		 var objectType = typeof(LimitOrderTransaction);
		 object existingValue = null;
		 var serializer = new JsonSerializer();
		 var converter = new TransactionsStreamResponseConverter();

		 //act
		 var readJsonResult = converter.ReadJson(reader, objectType, existingValue, serializer);

		 //assert
		 if (readJsonResult is TransactionsStreamResponse transactionResponse)
		 {
			Assert.IsTrue(transactionResponse.IsHeartbeat());
			Assert.AreEqual(heartbeat.type, transactionResponse.heartbeat.type);
			Assert.AreEqual(heartbeat.time, transactionResponse.heartbeat.time);
		 }
		 else
		 {
			Assert.Fail($"ReadJson result type '{readJsonResult.GetType().FullName}' is incorrect.");
		 }
	  }
   }
}
