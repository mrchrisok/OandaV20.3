namespace OkonkwoOandaV20.TradeLibrary.Pricing
{
   public class CandleSpecification
   {
	  /// <summary>
	  /// The instrument of the candlesticks to fetch
	  /// </summary>
	  public string instrument { get; set; }
	  /// <summary>
	  /// The granularity of the candlesticks to fetch.
	  /// Valid values are specifiec in the CandleStickGranularity class.
	  /// </summary>
	  public string granularity { get; set; }

	  /// <summary>
	  /// The Price component(s) to get candlestick data for. Can contain any combination 
	  /// of the characters “M” (midpoint candles) “B” (bid candles) and “A” (ask candles). 
	  /// [default=M]
	  /// Valid values are specified in the CandleStickPriceType class.
	  /// </summary>
	  public string price { get; set; }

	  public override string ToString()
	  {
		 return $"{instrument}:{granularity}:{price}";
	  }
   }
}
