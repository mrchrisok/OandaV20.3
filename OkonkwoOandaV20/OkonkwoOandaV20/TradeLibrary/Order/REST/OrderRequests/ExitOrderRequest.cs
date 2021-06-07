using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.REST.OrderRequests
{
   [JsonConverter(typeof(PriceObjectConverter))]
   public abstract class ExitOrderRequest : OrderRequest
   {
	  public ExitOrderRequest(Instrument.Instrument oandaInstrument)
	  {
		 priceInformation = new PriceInformation()
		 {
			instrument = oandaInstrument,
			priceProperties = new List<string>() { nameof(price) },
			unitsProperties = new List<string>()
		 };
	  }

	  public decimal? price { get; set; }
	  public long tradeID { get; set; }
	  public string clientTradeID { get; set; }
	  public string timeInForce { get; set; }
	  public DateTime? gtdTime { get; set; }
	  public string triggerCondition { get; set; }
   }
}
