using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.Position
{
   /// <summary>
   /// The representation of a Position for a single direcetion (long or short).
   /// http://developer.oanda.com/rest-live-v20/position-df/#PositionSide
   /// </summary>
   public class PositionSide
   {
	  /// <summary>
	  /// Number of units in the position (negative value indicates short position,
	  /// positive indicates long position).
	  /// </summary>
	  public decimal units { get; set; }

	  /// <summary>
	  /// Volume-weighted average of the underlying Trade open prices for the
	  /// Position.
	  /// </summary>
	  public decimal averagePrice { get; set; }

	  /// <summary>
	  /// List of the open Trade IDs which contribute to the open Position.
	  /// </summary>
	  public List<long> tradeIDs { get; set; }

	  /// <summary>
	  /// Profit/loss realized by the PositionSide over the lifetime of the
	  /// Account.
	  /// </summary>
	  public decimal pl { get; set; }

	  /// <summary>
	  /// The unrealized profit/loss of all open Trades that contribute to this
	  /// PositionSide.
	  /// </summary>
	  public decimal unrealizedPL { get; set; }

	  /// <summary>
	  /// Profit/loss realized by the PositionSide since the Account’s resettablePL
	  /// was last reset by the client.
	  /// </summary>
	  public decimal resettablePL { get; set; }

	  /// <summary>
	  /// The total amount of financing paid/collected for this PositionSide over the lifetime of the Account.
	  /// </summary>
	  public decimal financing { get; set; }

	  /// <summary>
	  /// The total amount of fees charged over the lifetime of the Account for the execution of 
	  /// guaranteed Stop Loss Orders attached to Trades for this PositionSide.
	  /// </summary>
	  public decimal guaranteedExecutionFees { get; set; }
   }
}