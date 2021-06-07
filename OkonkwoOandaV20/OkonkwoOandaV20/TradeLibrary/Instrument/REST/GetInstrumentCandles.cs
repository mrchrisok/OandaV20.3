using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.TradeLibrary.Instrument;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/account-ep/
   /// </summary>
   public partial class Rest20
   {
	  /// <summary>
	  /// Fetch candlestick data for an instrument
	  /// </summary>
	  /// <param name="instrument">Name of the Instrument [required]</param>
	  /// <param name="request">The parameters for the request</param>
	  /// <returns>List of Candlestick objects (or empty list) </returns>
	  public static async Task<List<CandlestickPlus>> GetInstrumentCandlesAsync(string instrument, InstrumentCandlesParameters parameters)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}instruments/{instrument}/candles",
			Method = "GET",
			Parameters = parameters
		 };

		 var response = await MakeRequestAsync<InstrumentCandlesResponse, InstrumentCandlesErrorResponse>(request);

		 var candles = new List<CandlestickPlus>();
		 foreach (var candle in response.candles)
		 {
			candles.Add(new CandlestickPlus(candle) { instrument = instrument, granularity = response.granularity });
		 }

		 return candles;
	  }

	  public class InstrumentCandlesParameters : Parameters
	  {
		 /// <summary>
		 /// The Price component(s) to get candlestick data for. Can contain any combination 
		 /// of the characters “M” (midpoint candles) “B” (bid candles) and “A” (ask candles). 
		 /// [default=M]
		 /// Valid values are specified in the CandleStickPriceType class.
		 /// </summary>
		 [Query]
		 public string price { get; set; }

		 /// <summary>
		 /// The granularity of the candlesticks to fetch [default=S5].
		 /// Valid values are specifiec in the CandleStickGranularity class.
		 /// </summary>
		 [Query]
		 public string granularity { get; set; }

		 /// <summary>
		 /// The number of candlesticks to return in the reponse. Count should not be 
		 /// specified if both the start and end parameters are provided, as the time range 
		 /// combined with the graularity will determine the number of candlesticks to return. 
		 /// [default=500, maximum=5000]
		 /// </summary>
		 [Query]
		 public int? count { get; set; }

		 /// <summary>
		 /// The start of the time range to fetch candlesticks for.
		 /// </summary>
		 [Query]
		 public DateTime? from { get; set; }

		 /// <summary>
		 /// The end of the time range to fetch candlesticks for.
		 /// </summary>
		 [Query]
		 public DateTime? to { get; set; }

		 /// <summary>
		 /// A flag that controls whether the candlestick is “smoothed” or not. A smoothed 
		 /// candlestick uses the previous candle’s close price as its open price, while an 
		 /// unsmoothed candlestick uses the first price from its time range as its open price.
		 /// [default=False]
		 /// </summary>
		 [Query]
		 public bool? smooth { get; set; }

		 /// <summary>
		 /// A flag that controls whether the candlestick that is covered by the from time 
		 /// should be included in the results. This flag enables clients to use the timestamp of 
		 /// the last completed candlestick received to poll for future candlesticks but avoid 
		 /// receiving the previous candlestick repeatedly. [default=True]
		 /// </summary>
		 [Query]
		 public bool? includeFirst { get; set; }

		 /// <summary>
		 /// The hour of the day (in the specified timezone) to use for granularities that have 
		 /// daily alignments. [default=17, minimum=0, maximum=23]
		 /// </summary>
		 [Query]
		 public int? dailyAlignment { get; set; }

		 /// <summary>
		 /// The timezone to use for the dailyAlignment parameter. Candlesticks with daily 
		 /// alignment will be aligned to the dailyAlignment hour within the alignmentTimezone. 
		 /// Note that the returned times will still be represented in UTC. [default=America/New_York]
		 /// Review this <see cref="http://developer.oanda.com/docs/timezones.txt">Oanda Timezones List</see>
		 /// to see all supported timezone values.
		 /// </summary>
		 [Query]
		 public string alignmentTimezone { get; set; }

		 /// <summary>
		 /// The day of the week used for granularities that have weekly alignment. [default=Friday]
		 /// </summary>
		 /// Valid values are specified in the WeeklyAlignment class.
		 [Query]
		 public string weeklyAlignment { get; set; }
	  }
   }

   /// <summary>
   /// The GET success response received from instruments/instrument/candles
   /// </summary>
   public class InstrumentCandlesResponse : Response
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

   /// <summary>
   /// The GET error response received from instruments/instrument/candles
   /// </summary>
   public class InstrumentCandlesErrorResponse : ErrorResponse
   {
   }
}