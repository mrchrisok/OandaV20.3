namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   public class OrderClientExtensionsModifyRejectTransaction : RejectTransaction
   {
	  /// <summary>
	  /// The ID of the Order who’s client extensions are to be modified.
	  /// </summary>
	  public long orderID { get; set; }

	  /// <summary>
	  /// The original Client ID of the Order who’s client extensions are to be
	  /// modified.
	  /// </summary>
	  public string clientOrderID { get; set; }

	  /// <summary>
	  /// The new Client Extensions for the Order.
	  /// </summary>
	  public ClientExtensions clientExtensionsModify { get; set; }

	  /// <summary>
	  /// The new Client Extensions for the Order’s Trade on fill.
	  /// </summary>
	  public ClientExtensions tradeClientExtensionsModify { get; set; }
   }
}
