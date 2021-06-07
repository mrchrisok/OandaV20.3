namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// A MarketIfTouchedOrderTransaction represents the creation of a MarketIfTouched 
   /// Order in the user’s Account.
   /// </summary>
   public class MarketIfTouchedOrderTransaction : PriceEntryOrderTransaction
   {
      /// <summary>
      /// The worst market price that may be used to fill this MarketIfTouched Order. If the
      /// market gaps and crosses through both the price and the priceBound, the
      /// MarketIfTouched Order will be cancelled instead of being filled.
      /// </summary>
      public decimal? priceBound { get; set; }
   }
}
