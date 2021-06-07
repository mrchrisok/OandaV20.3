namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// 
   /// </summary>
   public class ClientConfigureTransaction : Transaction
   {
	  /// <summary>
	  /// The client-provided alias for the Account.
	  /// </summary>
	  public string alias { get; set; }

	  /// <summary>
	  /// The margin rate override for the Account.
	  /// </summary>
	  public decimal marginRate { get; set; }
   }
}