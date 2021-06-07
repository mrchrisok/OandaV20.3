using System;

namespace OkonkwoOandaV20.TradeLibrary.Order
{
   /// <summary>
   /// Base Order definition inherited by all Entry Orders that initiate a new Trade or Position
   /// when a specified price threshold is reached.
   /// </summary>
   public abstract class PriceEntryOrder : EntryOrder
   {
	  /// <summary>
	  /// The price threshold specified for the Entry Order. The Entry Order will
	  /// only be filled by a market price that is equal to or better than this
	  /// price.
	  /// </summary>
	  public decimal price { get; set; }

	  /// <summary>
	  /// The date/time when the Order will be cancelled if its timeInForce is
	  /// “GTD”.
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
	  /// he ID of the Order that was replaced by this Order (only provided if
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
