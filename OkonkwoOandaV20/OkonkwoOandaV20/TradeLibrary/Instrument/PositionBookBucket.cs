namespace OkonkwoOandaV20.TradeLibrary.Instrument
{
   public class PositionBookBucket
   {
      /// <summary>
      /// The lowest price (inclusive) covered by the bucket. The bucket covers the 
      /// price range from the price to price + the position book’s bucketWidth.
      /// </summary>
      public decimal price { get; set; }

      /// <summary>
      /// The percentage of the total number of positions represented by the long 
      /// orders found in this bucket.
      /// </summary>
      public decimal longCountPercent { get; set; }

      /// <summary>
      /// The percentage of the total number of positions represented by the short 
      /// orders found in this bucket.
      /// </summary>
      public decimal shortCountPercent { get; set; }
   }
}
