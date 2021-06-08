using OkonkwoOandaV20.TradeLibrary.Order;
using System;

namespace OkonkwoOandaV20.TradeLibrary.REST.OrderRequests
{
   public class StopLossOrderRequest : GuaranteedStopLossOrderRequest
   {
	  public StopLossOrderRequest(Instrument.Instrument oandaInstrument)
		 : base(oandaInstrument)
	  {
		 type = OrderType.StopLoss;
	  }

	  /// <summary>
	  /// Flag indicating that the Stop Loss Order is guaranteed. 
	  /// The default value depends on the GuaranteedStopLossOrderMode of the account, 
	  /// if it is REQUIRED, the default will be true, for DISABLED or ENABLED the default is false.
	  /// </summary>
	  [Obsolete("Will be removed in a future API update.")]
	  public bool guaranteed { get; set; }
   }
}
