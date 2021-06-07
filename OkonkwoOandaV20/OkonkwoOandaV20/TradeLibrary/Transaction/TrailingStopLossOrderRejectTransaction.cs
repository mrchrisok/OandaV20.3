namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// A TrailingStopLossOrderRejectTransaction represents the rejection of the 
   /// creation of a TrailingStopLoss Order.
   /// </summary>
   public class TrailingStopLossOrderRejectTransaction : ExitOrderRejectTransaction
   {
	  /// <summary>
	  /// The price distance (in price units) specified for the TrailingStopLoss 
	  /// Order.
	  /// </summary>
	  public decimal distance { get; set; }
   }
}
