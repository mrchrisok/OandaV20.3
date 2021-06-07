namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// A StopLossOrderRejectTransaction represents the rejetion of the creation of a
   /// StopLoss Order
   /// </summary>
   public class StopLossOrderRejectTransaction : ExitOrderRejectTransaction
   {
      /// <summary>
      /// The price threshold specified for the Stop Loss Order. If the guaranteed
      /// flag is false, the associated Trade will be closed by a market price that
      /// is equal to or worse than this threshold. If the flag is true the
      /// associated Trade will be closed at this price.
      /// </summary>
      public decimal price { get; set; }
   }
}
