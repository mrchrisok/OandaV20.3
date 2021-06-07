namespace OkonkwoOandaV20.TradeLibrary.Order
{
   public class MarketIfTouchedOrder : PriceEntryOrder
   {
      /// <summary>
      /// The worst market price that may be used to fill this MarketIfTouched
      /// Order.
      /// </summary>
      public decimal? priceBound { get; set; }

      /// <summary>
      /// The Market price at the time when the MarketIfTouched Order was created.
      /// </summary>
      public decimal initialMarketPrice { get; set; }
   }
}
