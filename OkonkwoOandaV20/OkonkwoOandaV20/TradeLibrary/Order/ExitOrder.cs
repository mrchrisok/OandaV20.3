using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.Order
{
   public abstract class ExitOrder : Order
   {
	  /// <summary>
	  /// The ID of the Trade to close when the price threshold is breached.
	  /// </summary>
	  public string tradeID { get; set; }

	  /// <summary>
	  /// The client ID of the Trade to be closed when the price threshold is
	  /// breached.
	  /// </summary>
	  public string clientTradeID { get; set; }

	  /// <summary>
	  /// The time-in-force requested for the ExitOrder. Restricted to 
	  /// “GTC”, “GFD” and “GTD” for TakeProfit Orders.
	  /// </summary>
	  public string timeInForce { get; set; }

	  /// <summary>
	  /// The date/time when the Order will be cancelled if its 
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
	  /// restriction applies when creating a guaranteed Stop Loss Order. In this
	  /// case the TriggerCondition value must either be “DEFAULT”, or the
	  /// “natural” trigger side “DEFAULT” results in. So for a Stop Loss Order for
	  /// a long trade valid values are “DEFAULT” and “BID”, and for short trades
	  /// “DEFAULT” and “ASK” are valid.
	  /// </summary>
	  public string triggerCondition { get; set; }

	  /// <summary>
	  /// ID of the Transaction that filled this Order (only provided when the
	  /// Order’s state is FILLED)
	  /// </summary>
	  public long? fillingTransactionID { get; set; }

	  /// <summary>
	  /// Date/time when the Order was filled (only provided when the Order’s state
	  /// is FILLED)
	  /// </summary>
	  public DateTime? filledTime { get; set; }

	  /// <summary>
	  /// Trade ID of Trade opened when the Order was filled (only provided when
	  /// the Order’s state is FILLED and a Trade was opened as a result of the
	  /// fill)
	  /// </summary>
	  public long? tradeOpenedID { get; set; }

	  /// <summary>
	  /// Trade ID of Trade reduced when the Order was filled (only provided when
	  /// the Order’s state is FILLED and a Trade was reduced as a result of the
	  /// fill)
	  /// </summary>
	  public long? tradeReducedID { get; set; }

	  /// <summary>
	  /// Trade IDs of Trades closed when the Order was filled (only provided when
	  /// the Order’s state is FILLED and one or more Trades were closed as a
	  /// result of the fill)
	  /// </summary>
	  public List<long> tradeClosedIDs { get; set; }

	  /// <summary>
	  /// ID of the Transaction that cancelled the Order (only provided when the
	  /// Order’s state is CANCELLED)
	  /// </summary>
	  public long? cancellingTransactionID { get; set; }

	  /// <summary>
	  /// Date/time when the Order was cancelled (only provided when the state of
	  /// the Order is CANCELLED)
	  /// </summary>
	  public DateTime? cancelledTime { get; set; }

	  /// <summary>
	  /// The ID of the Order that was replaced by this Order (only provided if
	  /// this Order was created as part of a cancel/replace).
	  /// </summary>
	  public long? replacesOrderID { get; set; }

	  /// <summary>
	  /// The ID of the Order that replaced this Order (only provided if this Order
	  /// was cancelled as part of a cancel/replace).
	  /// </summary>
	  public long? replacedByOrderID { get; set; }
   }
}
