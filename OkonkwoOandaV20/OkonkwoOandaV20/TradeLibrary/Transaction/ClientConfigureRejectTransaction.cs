namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   public class ClientConfigureRejectTransaction : Transaction
   {
	  /// <summary>
	  /// The client-provided alias for the Account.
	  /// </summary>
	  public string alias { get; set; }

	  /// <summary>
	  /// The margin rate override for the Account.
	  /// </summary>
	  public decimal marginRate { get; set; }

	  /// <summary>
	  /// The reason that the Reject Transaction was created
	  /// </summary>
	  public string rejectReason { get; set; }
   }
}