using OkonkwoOandaV20.TradeLibrary.Order;

namespace OkonkwoOandaV20.TradeLibrary.REST.OrderRequests
{
   public class MarketIfTouchedOrderRequest : PriceEntryOrderRequest
   {
	  public MarketIfTouchedOrderRequest(Instrument.Instrument oandaInstrument)
		 : base(oandaInstrument)
	  {
		 type = OrderType.MarketIfTouched;
	  }
   }
}
