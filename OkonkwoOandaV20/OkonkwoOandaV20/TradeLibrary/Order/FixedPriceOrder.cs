namespace OkonkwoOandaV20.TradeLibrary.Order
{
   public class FixedPriceOrder : EntryOrder
   {
      /// <summary>
      /// The price specified for the Fixed Price Order. This price is the exact
      /// price that the Fixed Price Order will be filled at.
      /// </summary>
      public decimal price { get; set; }

      /// <summary>
      /// The state that the trade resulting from the Fixed Price Order should be
      /// set to.
      /// </summary>
      public string tradeState { get; set; }
   }
}
