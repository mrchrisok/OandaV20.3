namespace OkonkwoOandaV20.TradeLibrary.Trade
{
   /// <summary>
   /// The current state of the trade
   /// </summary>
   public class TradeState
   {
      public const string Open = "OPEN";
      public const string Closed = "CLOSED";
      public const string CloseWhenTradeable = "CLOSE_WHEN_TRADEABLE";
   }

   /// <summary>
   /// The state to filter trades by
   /// </summary>
   public class TradeStateFilter : TradeState
   {
      public const string All = "ALL";
   }

   /// <summary>
   /// The classification of TradePLs
   /// </summary>
   public class TradePL
   {
      public const string Positive = "POSITIVE";
      public const string Negative = "NEGATIVE";
      public const string Zero = "ZERO";
   }
}
