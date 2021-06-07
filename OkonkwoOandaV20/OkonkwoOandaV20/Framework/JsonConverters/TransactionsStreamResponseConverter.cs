using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OkonkwoOandaV20.Framework.Factories;
using OkonkwoOandaV20.TradeLibrary.REST;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System;

namespace OkonkwoOandaV20.Framework.JsonConverters
{
   public class TransactionsStreamResponseConverter : AbstractJsonConverter
   {
	  public override bool CanConvert(Type objectType)
	  {
		 bool canConvert = objectType == typeof(TransactionsStreamResponse);
		 return canConvert;
	  }

	  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	  {
		 var response = new TransactionsStreamResponse();

		 var jsonToken = JToken.Load(reader);

		 if (jsonToken.Type == JTokenType.Null)
		 {
			return null;
		 }
		 else if (jsonToken.Type == JTokenType.Object)
		 {
			bool isHeartbeat = jsonToken["type"].Value<string>() == "HEARTBEAT";

			if (isHeartbeat)
			{
			   var heartbeat = new TransactionsHeartbeat();
			   serializer.Populate(jsonToken.CreateReader(), heartbeat);
			   response.heartbeat = heartbeat;
			}
			else
			{
			   ITransaction transaction = TransactionFactory.Create(jsonToken["type"].Value<string>());
			   serializer.Populate(jsonToken.CreateReader(), transaction);
			   response.transaction = transaction;
			}

			return response;
		 }
		 else
			throw new ArgumentException($"Unexpected JTokenType ({jsonToken.Type}) in reader.");
	  }
   }
}
