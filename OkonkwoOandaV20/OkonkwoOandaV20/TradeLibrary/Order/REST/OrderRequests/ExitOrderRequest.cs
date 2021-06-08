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

	  /// <summary>
	  /// The price threshold specified for the Stop Loss Order. The associated
	  /// Trade will be closed by a market price that is equal to or worse than
	  /// this threshold.
	  /// </summary>
	  public decimal? price { get; set; }

	  /// <summary>
	  /// The ID of the Trade to close when the price threshold is breached.
	  /// </summary>
	  public long tradeID { get; set; }

	  /// <summary>
	  /// he client ID of the Trade to be closed when the price threshold is
	  ///  breached.
	  /// </summary>
	  public string clientTradeID { get; set; }

	  /// <summary>
	  /// The time-in-force requested for the StopLoss Order. Restricted to “GTC”,
	  /// “GFD” and “GTD” for StopLoss Orders.
	  /// </summary>
	  public string timeInForce { get; set; }

	  /// <summary>
	  /// The date/time when the StopLoss Order will be cancelled if its
	  /// timeInForce is “GTD”.
	  /// </summary>
	  public DateTime? gtdTime { get; set; }

	  /// <summary>
	  /// Specification of which price component should be used when determining if
	  /// an Order should be triggered and filled. This allows Orders to be
	  /// triggered based on the bid, ask, mid, default (ask for buy, bid for sell)
	  /// or inverse (ask for sell, bid for buy) price depending on the desired
	  /// behaviour. Orders are always filled using their default price component.
	  /// This feature is only provided through the REST API. Clients who choose to
	  /// specify a non-default trigger condition will not see it reflected in any
	  /// of OANDA’s proprietary or partner trading platforms, their transaction
	  /// history or their account statements. OANDA platforms always assume that
	  /// an Order’s trigger condition is set to the default value when indicating
	  /// the distance from an Order’s trigger price, and will always provide the
	  /// default trigger condition when creating or modifying an Order. A special
	  /// restriction applies when creating a Guaranteed Stop Loss Order. In this
	  /// case the TriggerCondition value must either be “DEFAULT”, or the
	  /// “natural” trigger side “DEFAULT” results in. So for a Guaranteed Stop
	  /// Loss Order for a long trade valid values are “DEFAULT” and “BID”, and for
	  /// short trades “DEFAULT” and “ASK” are valid.
	  /// </summary>
	  public string triggerCondition { get; set; }
   }
}
