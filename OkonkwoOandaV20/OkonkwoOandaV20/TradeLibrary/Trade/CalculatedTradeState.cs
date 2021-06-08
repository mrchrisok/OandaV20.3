namespace OkonkwoOandaV20.TradeLibrary.Trade
{
   /// <summary>
   /// The dynamic (calculated) state of an Open Trade
   /// http://developer.oanda.com/rest-live-v20/trade-df/#CalculatedTradeState
   /// </summary>
   public class CalculatedTradeState
   {
	  /// <summary>
	  /// The Trade’s ID.
	  /// </summary>
	  public long id { get; set; }

	  /// <summary>
	  /// he Trade’s unrealized profit/loss.
	  /// </summary>
	  public decimal unrealizedPL { get; set; }

	  /// <summary>
	  /// Margin currently used by the Trade.
	  /// </summary>
	  public decimal marginUsed { get; set; }
   }
}
