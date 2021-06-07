namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// A RejectTransaction represents the rejection of an transaction attempted
   /// on the user's Account
   /// </summary>
   public abstract class RejectTransaction : Transaction
   {
      /// <summary>
      /// The reason that the Reject Transaction was created
      /// </summary>
      public string rejectReason { get; set; }
   }
}
