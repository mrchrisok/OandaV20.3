using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OkonkwoOandaV20.Framework.Factories;
using OkonkwoOandaV20.TradeLibrary.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OkonkwoOandaV20.Framework.JsonConverters
{
   public class OrderConverter : AbstractJsonConverter
   {
	  public override bool CanConvert(Type objectType)
	  {
		 return objectType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IEnumerable<IOrder>))
			|| objectType.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IOrder));
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
			var orders = new List<IOrder>();

			var jsonArray = (JArray)jsonToken;

			foreach (var item in jsonArray)
			{
			   var order = OrderFactory.Create(item["type"].Value<string>());
			   serializer.Populate(item.CreateReader(), order);
			   orders.Add(order);
			}

			return orders;
		 }
		 else if (jsonToken.Type == JTokenType.Object)
		 {
			IOrder order = OrderFactory.Create(jsonToken["type"].Value<string>());
			serializer.Populate(jsonToken.CreateReader(), order);
			return order;
		 }
		 else
			throw new ArgumentException($"Unexpected JTokenType ({jsonToken.Type}) in reader.");
	  }
   }
}
