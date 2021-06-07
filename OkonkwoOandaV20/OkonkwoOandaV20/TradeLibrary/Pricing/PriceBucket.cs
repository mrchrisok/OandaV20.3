namespace OkonkwoOandaV20.TradeLibrary.Pricing
{
   /// <summary>
   /// A Price Bucket represents a prie available for an amount of liquidity.
   /// </summary>
   public class PriceBucket
   {
      /// <summary>
      /// The Price offered by the PriceBucket
      /// </summary>
      public decimal price { get; set; }

      /// <summary>
      /// The amount of liquidity offered by the PriceBucket
      /// </summary>
      public int liquidity { get; set; }
   }
}