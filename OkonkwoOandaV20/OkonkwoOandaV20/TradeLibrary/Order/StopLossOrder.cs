using System;

namespace OkonkwoOandaV20.TradeLibrary.Order
{
   public class StopLossOrder : GuaranteedStopLossOrder
   {
	  /// <summary>
	  /// Flag indicating that the Stop Loss Order is guaranteed. The default value
	  /// depends on the GuaranteedStopLossOrderMode of the account, if it is
	  /// REQUIRED, the default will be true, for DISABLED or ENABLED the default
	  /// is false.
	  /// </summary>
	  [Obsolete("Will be removed in a future API update.")]
	  public bool guaranteed { get; set; }

	  /// <summary>
	  /// The premium that will be charged if the Stop Loss Order is guaranteed and
	  /// the Order is filled at the guaranteed price. It is in price units and is
	  /// charged for each unit of the Trade.
	  /// </summary>
	  [Obsolete("Will be removed in a future API update.")]
	  public new decimal guaranteedExecutionPremium { get; set; }
   }
}
