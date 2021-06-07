namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// A FixedPriceOrderTransaction represents the creation of a Fixed Price Order in the user’s 
   /// account. A Fixed Price Order is an Order that is filled immediately at a specified price.
   /// </summary>
   public class FixedPriceOrderTransaction : EntryOrderTransaction
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
