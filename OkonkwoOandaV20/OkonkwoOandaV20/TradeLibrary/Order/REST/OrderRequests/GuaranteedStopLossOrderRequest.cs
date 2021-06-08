using OkonkwoOandaV20.TradeLibrary.Order;

namespace OkonkwoOandaV20.TradeLibrary.REST.OrderRequests
{
   /// <summary>
   /// A GuaranteedStopLossOrderRequest specifies the parameters that may be set 
   /// when creating a Guaranteed Stop Loss Order. Only one of the price and distance 
   /// fields may be specified.
   /// </summary>
   public class GuaranteedStopLossOrderRequest : ExitOrderRequest
   {
	  public GuaranteedStopLossOrderRequest(Instrument.Instrument oandaInstrument)
		 : base(oandaInstrument)
	  {
		 type = OrderType.GuaranteedStopLoss;

		 priceInformation.priceProperties.Add(nameof(distance));
	  }

	  /// <summary>
	  /// Specifies the distance (in price units) from the Account’s current price
	  /// to use as the Stop Loss Order price. If the Trade is short the
	  /// Instrument’s bid price is used, and for long Trades the ask is used.
	  /// </summary>
	  public decimal? distance { get; set; }
   }
}
