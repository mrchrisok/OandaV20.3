namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   /// <summary>
   /// A TransferFundsTransaction represents the transfer of funds in/ou of an Account
   /// </summary>
   public class TransferFundsTransaction : Transaction
   {
	  /// <summary>
	  /// The amount to deposit/withdraw from the Account in the Account’s home
	  /// currency. A positive value indicates a deposit, a negative value
	  /// indicates a withdrawal.
	  /// </summary>
	  public decimal amount { get; set; }

	  /// <summary>
	  /// The reason that an Account is being funded.
	  /// Valid values are spedcified in the FundingReason class
	  /// </summary>
	  public string fundingReason { get; set; }

	  /// <summary>
	  /// An optional comment that may be attached to a fund transfer for audit
	  /// purposes
	  /// </summary>
	  public string comment { get; set; }

	  /// <summary>
	  /// The Account’s balance after funds are transferred.
	  /// </summary>
	  public decimal accountBalance { get; set; }
   }
}