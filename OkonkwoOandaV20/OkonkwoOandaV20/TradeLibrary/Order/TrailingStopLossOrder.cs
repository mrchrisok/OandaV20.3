namespace OkonkwoOandaV20.TradeLibrary.Order
{
   public class TrailingStopLossOrder : ExitOrder
   {
      /// <summary>
      /// The price distance (in price units) specified for the TrailingStopLoss
      /// Order.
      /// </summary>
      public decimal distance { get; set; }

      /// <summary>
      /// The trigger price for the Trailing Stop Loss Order. The trailing stop
      /// value will trail (follow) the market price by the TSL order’s configured
      /// “distance” as the market price moves in the winning direction. If the
      /// market price moves to a level that is equal to or worse than the trailing
      /// stop value, the order will be filled and the Trade will be closed.
      /// </summary>
      public decimal trailingStopValue { get; set; }
   }
}
