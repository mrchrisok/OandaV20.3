using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OkonkwoOandaV20.Framework.Factories;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OkonkwoOandaV20.Framework.JsonConverters
{
   public class TransactionConverter : AbstractJsonConverter
   {
	  public override bool CanConvert(Type objectType)
	  {
		 return objectType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IEnumerable<ITransaction>))
			|| objectType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(ITransaction));
	  }

	  public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	  {
		 var jsonToken = JToken.Load(reader);

		 if (jsonToken.Type == JTokenType.Null)
		 {
			return null;
		 }
		 else if (jsonToken.Type == JTokenType.Array)
		 {
			var transactions = new List<ITransaction>();

			var jsonArray = (JArray)jsonToken;

			foreach (var item in jsonArray)
			{
			   var transaction = TransactionFactory.Create(item["type"].Value<string>());
			   serializer.Populate(item.CreateReader(), transaction);
			   transactions.Add(transaction);
			}

			return transactions;
		 }
		 else if (jsonToken.Type == JTokenType.Object)
		 {
			ITransaction transaction = TransactionFactory.Create(jsonToken["type"].Value<string>());
			serializer.Populate(jsonToken.CreateReader(), transaction);
			return transaction;
		 }
		 else
			throw new ArgumentException($"Unexpected JTokenType ({jsonToken.Type}) in reader.");
	  }
   }
}
