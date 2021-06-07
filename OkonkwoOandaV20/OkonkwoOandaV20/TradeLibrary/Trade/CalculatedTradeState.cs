namespace OkonkwoOandaV20.TradeLibrary.Trade
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/trade-df/#CalculatedTradeState
   /// </summary>
   public class CalculatedTradeState
   {
      public long id { get; set; }
      public decimal unrealizedPL { get; set; }
      public decimal marginUsed { get; set; }
   }
}
