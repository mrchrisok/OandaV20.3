using OkonkwoOandaV20.TradeLibrary.Transaction;
using System;
using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.Trade
{
   public abstract class TradeBase : CalculatedTradeState
   {
	  /// <summary>
	  /// The Trade’s Instrument.
	  /// </summary>
	  public string instrument { get; set; }

	  /// <summary>
	  /// The execution price of the Trade.
	  /// </summary>
	  public decimal price { get; set; }

	  /// <summary>
	  /// The date/time when the Trade was opened.
	  /// </summary>
	  public DateTime openTime { get; set; }

	  /// <summary>
	  /// The current state of the Trade.
	  /// </summary>
	  public string state { get; set; }

	  /// <summary>
	  /// The initial size of the Trade. Negative values indicate a short Trade,
	  /// and positive values indicate a long Trade.
	  /// </summary>
	  public decimal initialUnits { get; set; }

	  /// <summary>
	  /// The margin required at the time the Trade was created. Note, this is the
	  /// ‘pure’ margin required, it is not the ‘effective’ margin used that
	  /// factors in the trade risk if a GSLO is attached to the trade.
	  /// </summary>
	  public decimal initialMarginRequired { get; set; }

	  /// <summary>
	  /// The number of units currently open for the Trade. This value is reduced
	  /// to 0.0 as the Trade is closed.
	  /// </summary>
	  public decimal currentUnits { get; set; }

	  /// <summary>
	  /// The total profit/loss realized on the closed portion of the Trade.
	  /// </summary>
	  public decimal realizedPL { get; set; }

	  /// <summary>
	  /// The average closing price of the Trade. Only present if the Trade has
	  /// been closed or reduced at least once.
	  /// </summary>
	  public decimal averageClosePrice { get; set; }

	  /// <summary>
	  /// The IDs of the Transactions that have closed portions of this Trade.
	  /// </summary>
	  public List<long> closingTransactionIDs { get; set; }

	  /// <summary>
	  /// The financing paid/collected for this Trade.
	  /// </summary>
	  public decimal financing { get; set; }

	  /// <summary>
	  /// The date/time when the Trade was fully closed. Only provided for Trade 
	  /// whose state is CLOSED.
	  /// </summary>
	  public DateTime? closeTime { get; set; }

	  /// <summary>
	  /// The client extensions of the Trade.
	  /// </summary>
	  public ClientExtensions clientExtensions { get; set; }
   }
}
