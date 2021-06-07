namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// A MarginCallExtendTransaction is created when the margin call state for an Account has
   /// been extended
   /// </summary>
   public class MarginCallExtendTransaction : Transaction
   {
      /// <summary>
      /// The number of the extensions to the Account’s current margin call that
      /// have been applied. This value will be set to 1 for the first
      /// MarginCallExtend Transaction
      /// </summary>
      public int extensionNumber { get; set; }
   }
}
