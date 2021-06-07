namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   public class StopLossOrderTransaction : ExitOrderTransaction
   {
	  /// <summary>
	  /// The price threshold specified for the Stop Loss Order. The associated
	  /// Trade will be closed by a market price that is equal to or worse than
	  /// this threshold.
	  /// </summary>
	  public decimal price { get; set; }

	  /// <summary>
	  /// Specifies the distance (in price units) from the Account’s current price
	  /// to use as the Stop Loss Order price. If the Trade is short the
	  /// Instrument’s bid price is used, and for long Trades the ask is used.
	  /// </summary>
	  public decimal distance { get; set; }

	  /// <summary>
	  /// Flag indicating that the Stop Loss Order is guaranteed. The default value
	  /// depends on the GuaranteedStopLossOrderMode of the account, if it is
	  /// REQUIRED, the default will be true, for DISABLED or ENABLED the default
	  /// is false.
	  /// </summary>
	  public bool guaranteed { get; set; }

	  /// <summary>
	  /// The fee that will be charged if the Stop Loss Order is guaranteed and the
	  /// Order is filled at the guaranteed price. The value is determined at Order
	  /// creation time. It is in price units and is charged for each unit of the
	  /// Trade.
	  /// </summary>
	  public decimal guaranteedExecutionPremium { get; set; }
   }
}
