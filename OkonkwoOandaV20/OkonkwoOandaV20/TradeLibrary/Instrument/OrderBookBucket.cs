namespace OkonkwoOandaV20.TradeLibrary.Instrument
{
   public class OrderBookBucket
   {
      /// <summary>
      /// The lowest price (inclusive) covered by the bucket. The bucket covers the 
      /// price range from the price to price + the order book’s bucketWidth.
      /// </summary>
      public decimal price { get; set; }

      /// <summary>
      /// The percentage of the total number of orders represented by the long 
      /// orders found in this bucket.
      /// </summary>
      public decimal longCountPercent { get; set; }

      /// <summary>
      /// The percentage of the total number of orders represented by the short 
      /// orders found in this bucket.
      /// </summary>
      public decimal shortCountPercent { get; set; }
   }
}
