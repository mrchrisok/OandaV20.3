using System;

namespace OkonkwoOandaV20.TradeLibrary.Account
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/account-df/#AccountSummary
   /// </summary>
   public class AccountSummary : CalculatedAccountState
   {
	  /// <summary>
	  /// The Account’s identifier
	  /// </summary>
	  public string id { get; set; }

	  /// <summary>
	  /// Client-assigned alias for the Account. Only provided if the Account has an alias set
	  /// </summary>
	  public string alias { get; set; }

	  /// <summary>
	  /// The home currency of the Account
	  /// </summary>
	  public string currency { get; set; }

	  /// <summary>
	  /// The current balance of the account.
	  /// </summary>
	  public decimal balance { get; set; }

	  /// <summary>
	  /// ID of the user that created the Account.
	  /// </summary>
	  public int createdByUserID { get; set; }

	  /// <summary>
	  /// The date/time when the Account was created.
	  /// </summary>
	  public DateTime createdTime { get; set; }

	  /// <summary>
	  /// The current guaranteed Stop Loss Order mode of the Account.
	  /// </summary>
	  public string guaranteedStopLossOrderMode { get; set; }

	  /// <summary>
	  /// The total profit/loss realized over the lifetime of the Account.
	  /// </summary>
	  public decimal pl { get; set; }

	  /// <summary>
	  /// The total realized profit/loss for the account since it was last reset by the client.
	  /// </summary>
	  public decimal resettablePL { get; set; }

	  /// <summary>
	  /// The date/time that the Account’s resettablePL was last reset.
	  /// </summary>
	  public DateTime? resettablePLTime { get; set; }

	  /// <summary>
	  /// The total amount of financing paid/collected over the lifetime of the Account.
	  /// </summary>
	  public decimal financing { get; set; }

	  /// <summary>
	  /// The total amount of commission paid over the lifetime of the Account.
	  /// </summary>
	  public decimal commission { get; set; }

	  /// <summary>
	  /// The total amount of fees charged over the lifetime of the Account for the execution of guaranteed Stop Loss Orders.
	  /// </summary>
	  public decimal guaranteedExecutionFees { get; set; }

	  /// <summary>
	  /// Client-provided margin rate override for the Account. The effective
	  /// margin rate of the Account is the lesser of this value and the OANDA
	  /// margin rate for the Account’s division. This value is only provided if a
	  /// margin rate override exists for the Account.
	  /// </summary>
	  public decimal? marginRate { get; set; }

	  /// <summary>
	  /// The date/time when the Account entered a margin call state. Only provided
	  /// if the Account is in a margin call.
	  /// </summary>
	  public DateTime? marginCallEnterTime { get; set; }

	  /// <summary>
	  /// The number of times that the Account’s current margin call was extended.
	  /// </summary>
	  public int marginCallExtensionCount { get; set; }

	  /// <summary>
	  /// The date/time of the Account’s last margin call extension.
	  /// </summary>
	  public DateTime? lastMarginCallExtensionTime { get; set; }

	  /// <summary>
	  /// The number of Trades currently open in the Account.
	  /// </summary>
	  public int openTradeCount { get; set; }

	  /// <summary>
	  /// The number of Positions currently open in the Account.
	  /// </summary>
	  public int openPositionCount { get; set; }

	  /// <summary>
	  /// The number of Orders currently pending in the Account.
	  /// </summary>
	  public int pendingOrderCount { get; set; }

	  /// <summary>
	  /// Flag indicating that the Account has hedging enabled.
	  /// </summary>
	  public bool hedgingEnabled { get; set; }

	  /// <summary>
	  /// The ID of the last Transaction created for the Account.
	  /// </summary>
	  public long lastTransactionID { get; set; }
   }
}
