using OkonkwoOandaV20.TradeLibrary.Order;

namespace OkonkwoOandaV20.TradeLibrary.Trade
{
   /// <summary>
   /// The specification of a Trade within an Account. This includes the full representation of the Trade’s dependent Orders in 
   /// addition to the IDs of those Orders.
   /// </summary>
   public class Trade : TradeBase
   {
	  /// <summary>
	  /// Full representation of the Trade’s Take Profit Order, only provided if
	  /// such an Order exists.
	  /// </summary>
	  public TakeProfitOrder takeProfitOrder { get; set; }

	  /// <summary>
	  /// Full representation of the Trade’s Stop Loss Order, only provided if such
	  /// an Order exists.
	  /// </summary>
	  public StopLossOrder stopLossOrder { get; set; }

	  /// <summary>
	  /// Full representation of the Trade’s Trailing Stop Loss Order, only
	  /// provided if such an Order exists.
	  /// </summary>
	  public TrailingStopLossOrder trailingStopLossOrder { get; set; }
   }
}
