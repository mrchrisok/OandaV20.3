namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   public class TradeClientExtensionsModifyRejectTransaction : RejectTransaction
   {
	  /// <summary>
	  /// The ID of the Trade who’s client extensions are to be modified.
	  /// </summary>
	  public long tradeID { get; set; }

	  /// <summary>
	  /// The original Client ID of the Trade who’s client extensions are to be
	  /// modified.
	  /// </summary>
	  public string clientTradeID { get; set; }

	  /// <summary>
	  /// The new Client Extensions for the Trade.
	  /// </summary>
	  public ClientExtensions tradeClientExtensionsModify { get; set; }
   }
}
