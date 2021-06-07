namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   public abstract class EntryOrderRejectTransaction : EntryOrderTransaction
   {
	  /// <summary>
	  /// The reason that the Reject Transaction was created
	  /// </summary>
	  public string rejectReason { get; set; }
   }
}
