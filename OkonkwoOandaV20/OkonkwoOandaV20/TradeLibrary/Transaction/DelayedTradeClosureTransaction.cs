using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.Transaction
{
   public class DelayedTradeClosureTransaction : Transaction
   {
	  /// <summary>
	  /// The reason for the delayed trade closure
	  /// </summary>
	  public string reason { get; set; }

	  /// <summary>
	  /// List of Trade ID’s identifying the open trades that will be closed when
	  /// their respective instruments become tradeable
	  /// </summary>
	  public List<long> tradeIDs { get; set; }
   }
}
