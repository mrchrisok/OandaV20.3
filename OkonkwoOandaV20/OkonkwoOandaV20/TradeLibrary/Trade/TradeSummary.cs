namespace OkonkwoOandaV20.TradeLibrary.Trade
{
   /// <summary>
   /// The summary of a Trade within an Account. This representation does not provide the full 
   /// details of the Trade’s dependent Orders.
   /// </summary>
   public class TradeSummary : TradeBase
   {

	  /// <summary>
	  /// ID of the Trade’s Take Profit Order, only provided if such an Order
	  /// exists.
	  /// </summary>
	  public long takeProfitOrderID { get; set; }

	  /// <summary>
	  /// ID of the Trade’s Stop Loss Order, only provided if such an Order exists.
	  /// </summary>
	  public long stopLossOrderID { get; set; }

	  /// <summary>
	  /// ID of the Trade’s Trailing Stop Loss Order, only provided if such an
	  /// Order exists.
	  /// </summary>
	  public long trailingStopLossOrderID { get; set; }
   }
}
