namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// A TrailingStopLossOrderTransaction represents the creation of a TrailingStopLoss 
   /// Order in the user's Account.
   /// </summary>
   public class TrailingStopLossOrderTransaction : ExitOrderTransaction
   {
	  /// <summary>
	  /// The price distance (in price units) specified for the TrailingStopLoss 
	  /// Order.
	  /// </summary>
	  public decimal distance { get; set; }
   }
}
