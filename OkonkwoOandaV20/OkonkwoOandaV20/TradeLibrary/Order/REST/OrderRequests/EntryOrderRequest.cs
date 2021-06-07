using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.Order;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.REST.OrderRequests
{
   [JsonConverter(typeof(PriceObjectConverter))]
   public abstract class EntryOrderRequest : OrderRequest
   {
	  public EntryOrderRequest(Instrument.Instrument oandaInstrument)
	  {
		 instrument = oandaInstrument.name;
		 timeInForce = TimeInForce.FillOrKill;
		 positionFill = OrderPositionFill.Default;
		 priceInformation = new PriceInformation()
		 {
			instrument = oandaInstrument,
			priceProperties = new List<string>() { nameof(price), nameof(priceBound) },
			unitsProperties = new List<string>() { nameof(units) }
		 };
	  }

	  /// <summary> 
	  /// The Order's instrument.
	  /// </summary>
	  public string instrument { get; set; }

	  /// <summary>
	  /// The quantity requested to be filled by the Market Order. A posititive
	  /// number of units results in a long Order, and a negative number of units
	  /// results in a short Order.
	  /// </summary>
	  public decimal units { get; set; }

	  /// <summary> 
	  /// The time-in-force requested for the Market Order.
	  /// </summary>
	  public string timeInForce { get; set; }

	  /// <summary>
	  /// The price threshold specified for the Limit Order. The Limit Order will
	  /// only be filled by a market price that is equal to or better than this
	  /// price.
	  /// </summary>
	  public decimal? price { get; set; }

	  /// <summary>
	  /// The worst price that the client is willing to have the 
	  /// Order filled at.
	  /// </summary>
	  public decimal? priceBound { get; set; }

	  /// <summary>
	  /// Specification of how Positions in the Account are modified when the Order 
	  /// filled. default=DEFAULT
	  /// Valid values are specified in the OrderPositionFill class
	  /// </summary>
	  public string positionFill { get; set; }

	  /// <summary>
	  /// TakeProfitDetails specifies the details of a Take Profit Order to be
	  /// created on behalf of a client. This may happen when an Order is filled
	  /// that opens a Trade requiring a Take Profit, or when a Trade’s dependent
	  /// Take Profit Order is modified directly through the Trade.
	  /// </summary>
	  public TakeProfitDetails takeProfitOnFill { get; set; }

	  /// <summary>
	  /// StopLossDetails specifies the details of a Stop Loss Order to be created
	  /// on behalf of a client. This may happen when an Order is filled that opens
	  /// a Trade requiring a Stop Loss, or when a Trade’s dependent Stop Loss
	  /// Order is modified directly through the Trade.
	  /// </summary>
	  public StopLossDetails stopLossOnFill { get; set; }

	  /// <summary>
	  /// StopLossDetails specifies the details of a Stop Loss Order to be created
	  /// on behalf of a client. This may happen when an Order is filled that opens
	  /// a Trade requiring a Stop Loss, or when a Trade’s dependent Stop Loss
	  /// Order is modified directly through the Trade. 
	  /// </summary>
	  public TrailingStopLossDetails trailingStopLossOnFill { get; set; }

	  /// <summary>
	  /// Client Extensions to add to the Trade created when the Order is filled
	  /// (if such a Trade is created). Do not set, modify, or delete
	  /// tradeClientExtensions if your account is associated with MT4.
	  /// </summary>
	  public ClientExtensions tradeClientExtensions { get; set; }
   }
}
