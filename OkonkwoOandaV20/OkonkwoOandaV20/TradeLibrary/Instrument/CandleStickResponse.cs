using System.Collections.Generic;

namespace OkonkwoOandaV20.TradeLibrary.Instrument
{
   public class CandleStickResponse
   {
	  /// <summary>
	  /// The instrument whose Prices are represented by the candlesticks.
	  /// </summary>
	  public string instrument;

	  /// <summary>
	  /// The granularity of the candlesticks provided.
	  /// </summary>
	  public string granularity;

	  /// <summary>
	  /// The list of candlesticks that satisfy the request.
	  /// </summary>
	  public List<Candlestick> candles;
   }
}
