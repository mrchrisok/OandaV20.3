using OkonkwoOandaV20.TradeLibrary.Order;

namespace OkonkwoOandaV20.TradeLibrary.REST.OrderRequests
{
   public class StopLossOrderRequest : ExitOrderRequest
   {
	  public StopLossOrderRequest(Instrument.Instrument oandaInstrument)
		 : base(oandaInstrument)
	  {
		 type = OrderType.StopLoss;

		 priceInformation.priceProperties.Add(nameof(distance));
	  }

	  /// <summary>
	  /// Specifies the distance (in price units) from the Account’s current price
	  /// to use as the Stop Loss Order price. If the Trade is short the
	  /// Instrument’s bid price is used, and for long Trades the ask is used.
	  /// </summary>
	  public decimal? distance { get; set; }

	  /// <summary>
	  /// Flag indicating that the Stop Loss Order is guaranteed. 
	  /// The default value depends on the GuaranteedStopLossOrderMode of the account, 
	  /// if it is REQUIRED, the default will be true, for DISABLED or ENABLED the default is false.
	  /// </summary>
	  public bool guaranteed { get; set; }
   }
}
