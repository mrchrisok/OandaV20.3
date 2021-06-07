namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   public class MarketIfTouchedOrderRejectTransaction : PriceEntryOrderRejectTransaction
   {
	  /// <summary>
	  /// The worst market price that may be used to fill this MarketIfTouched 
	  /// Order.
	  /// </summary>
	  public decimal? priceBound { get; set; }
   }
}
