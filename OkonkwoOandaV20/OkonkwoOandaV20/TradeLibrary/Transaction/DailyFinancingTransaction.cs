using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   public class DailyFinancingTransaction : Transaction
   {
	  /// <summary>
	  /// The amount of financing paid/collected for the Account.
	  /// </summary>
	  public decimal financing { get; set; }

	  /// <summary>
	  /// The Account’s balance after daily financing.
	  /// </summary>
	  public decimal accountBalance { get; set; }

	  /// <summary>
	  /// The account financing mode at the time of the daily financing. This field
	  /// is no longer in use moving forward and was replaced by
	  /// accountFinancingMode in individual positionFinancings since the financing
	  /// mode could differ between instruments.
	  /// </summary>
	  [Obsolete("Deprecated: Will be removed in a future API update.")]
	  public string accountFinancingMode { get; set; }

	  /// <summary>
	  /// The financing paid/collected for each Position in the Account.
	  /// </summary>
	  public List<PositionFinancing> positionFinancings { get; set; }
   }
}
