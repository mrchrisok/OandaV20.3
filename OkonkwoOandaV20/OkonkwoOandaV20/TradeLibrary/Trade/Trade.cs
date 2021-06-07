using OkonkwoOandaV20.TradeLibrary.Order;

namespace OkonkwoOandaV20.TradeLibrary.Trade
{
   public class Trade : TradeBase
   {
      public TakeProfitOrder takeProfitOrder { get; set; }
      public StopLossOrder stopLossOrder { get; set; }
      public TrailingStopLossOrder trailingStopLossOrder { get; set; }
   }
}
