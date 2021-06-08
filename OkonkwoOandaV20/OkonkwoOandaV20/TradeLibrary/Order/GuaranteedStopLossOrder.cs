namespace OkonkwoOandaV20.TradeLibrary.Order
{
   /// <summary>
   /// A GuaranteedStopLossOrder is an order that is linked to an open Trade and created with a 
   /// price threshold which is guaranteed against slippage that may occur as the market crosses the 
   /// price set for that order. The Order will be filled (closing the Trade) by the first price that is 
   /// equal to or worse than the threshold. The price level specified for the GuaranteedStopLossOrder 
   /// must be at least the configured minimum distance (in price units) away from the entry price for 
   /// the traded instrument. A GuaranteedStopLossOrder cannot be used to open a new Position.
   /// </summary>
   public class GuaranteedStopLossOrder : ExitOrder
   {
	  /// <summary>
	  /// The price threshold specified for the Stop Loss Order. If the guaranteed
	  /// flag is false, the associated Trade will be closed by a market price that
	  /// is equal to or worse than this threshold. If the flag is true the
	  /// associated Trade will be closed at this price.
	  /// </summary>
	  public decimal? price { get; set; }

	  /// <summary>
	  /// Specifies the distance (in price units) from the Account’s current price
	  /// to use as the Stop Loss Order price. If the Trade is short the
	  /// Instrument’s bid price is used, and for long Trades the ask is used.
	  /// </summary>
	  public decimal? distance { get; set; }

	  /// <summary>
	  /// The premium that will be charged if the Stop Loss Order is guaranteed and
	  /// the Order is filled at the guaranteed price. It is in price units and is
	  /// charged for each unit of the Trade.
	  /// </summary>
	  public decimal guaranteedExecutionPremium { get; set; }
   }
}
