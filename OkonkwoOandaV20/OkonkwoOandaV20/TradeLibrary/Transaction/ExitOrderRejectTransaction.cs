using System;

namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   public abstract class ExitOrderRejectTransaction : RejectTransaction
   {
	  /// <summary>
	  /// 
	  /// </summary>
	  public long tradeID { get; set; }

	  /// <summary>
	  /// The client ID of the Trade to be closed when the price threshold is
	  /// breached.
	  /// </summary>
	  public string clientTradeID { get; set; }

	  /// <summary>
	  /// The time-in-force requested for the Exit Order. Restricted to
	  /// “GTC”, “GFD” and “GTD” for TakeProfit Orders.
	  /// </summary>
	  public string timeInForce { get; set; }

	  /// <summary>
	  /// The date/time when the Exit Order will be cancelled if its
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

	  /// <summary>
	  /// The reason that the Exit Order was initiated
	  /// </summary>
	  public string reason { get; set; }

	  /// <summary>
	  /// Client Extensions to add to the Order (only provided if the Order is
	  /// being created with client extensions).
	  /// </summary>
	  public ClientExtensions clientExtensions { get; set; }

	  /// <summary>
	  /// The ID of the OrderFill Transaction that caused this Order to be created
	  /// (only provided if this Order was created automatically when another Order
	  /// was filled).
	  /// </summary>
	  public long? orderFillTransactionID { get; set; }

	  /// <summary>
	  /// The ID of the Order that this Order was intended to replace (only
	  /// provided if this Order was intended to replace an existing Order).
	  /// </summary>
	  public long? intendedReplacesOrderID { get; set; }
   }
}
