namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// An OrderCancelRejectTransaction represents the rejection of the cancellation of an Order
   /// in the client's Account
   /// </summary>
   public class OrderCancelRejectTransaction : RejectTransaction
   {
      /// <summary>
      /// The ID of the Order intended to be cancelled
      /// </summary>
      public long orderID { get; set; }

      /// <summary>
      /// The client ID of the Order intended to be cancelled (only provided if the 
      /// Order has a client Order ID).
      /// </summary>
      public string clientOrderID { get; set; }
   }
}
