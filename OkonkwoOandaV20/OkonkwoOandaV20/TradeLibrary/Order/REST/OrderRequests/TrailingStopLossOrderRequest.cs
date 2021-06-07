using OkonkwoOandaV20.TradeLibrary.Order;

namespace OkonkwoOandaV20.TradeLibrary.REST.OrderRequests
{
   public class TrailingStopLossOrderRequest : ExitOrderRequest
   {
	  public TrailingStopLossOrderRequest(Instrument.Instrument oandaInstrument)
		 : base(oandaInstrument)
	  {
		 type = OrderType.TrailingStopLoss;

		 priceInformation.priceProperties.Add(nameof(distance));
	  }

	  /// <summary>
	  /// The price distance (in price units) specified for the TrailingStopLoss 
	  /// Order.
	  /// </summary>
	  public decimal distance { get; set; }
   }
}
