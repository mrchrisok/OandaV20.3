using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.TypeConverters;
using OkonkwoOandaV20.TradeLibrary.Instrument;
using OkonkwoOandaV20.TradeLibrary.Pricing;
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
	  /// Get dancing bears and most recently completed candles within an Account for specified combinations of
	  /// instrument, granularity and price component.
	  /// https://developer.oanda.com/rest-live-v20/pricing-ep/#collapse_endpoint_2
	  /// </summary>
	  /// <param name="accountID">Name of the Instrument [required]</param>
	  /// <param name="parameters">The parameters for the request</param>
	  /// <returns>List of Candlestick objects (or empty list) </returns>
	  public static async Task<List<CandleStickResponse>> GetPricingCandlesLatestAsync(string accountID, PricingCandlesLatestParameters parameters)
	  {
		 if (parameters.candleSpecifications == null)
		 {
			throw new ArgumentException($"Candles specifications not set.");
		 }

		 foreach (var candleSpecification in parameters.candleSpecifications)
		 {
			var candleSpecificationString = candleSpecification.ToString();
			var candleParameters = candleSpecificationString.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

			if (candleParameters.Length != 3)
			{
			   throw new ArgumentException($"Candles specification '{candleSpecificationString}' is invalid.");
			}
		 }

		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/candles/latest",
			Method = "GET",
			Parameters = parameters
		 };

		 var response = await MakeRequestAsync<PricingCandlesResponse, PricingCandlesErrorResponse>(request);

		 return response.latestCandles;
	  }

	  public class PricingCandlesLatestParameters : Parameters
	  {
		 /// <summary>
		 /// List of candle specifications to get pricing for. [required]
		 /// </summary>
		 [Query(converter: typeof(ListToCsvConverter))]
		 public List<CandleSpecification> candleSpecifications { get; set; }

		 /// <summary>
		 /// The number of units used to calculate the volume-weighted average bid and ask prices in the 
		 /// returned candles. [default=1]
		 /// </summary>
		 [Query]
		 public decimal? units { get; set; }

		 /// <summary>
		 /// A flag that controls whether the candlestick is “smoothed” or not. A smoothed 
		 /// candlestick uses the previous candle’s close price as its open price, while an 
		 /// unsmoothed candlestick uses the first price from its time range as its open price.
		 /// [default=False]
		 /// </summary>
		 [Query]
		 public bool? smooth { get; set; }

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
   public class PricingCandlesResponse : Response
   {
	  /// <summary>
	  /// The latest candle sticks.
	  /// </summary>
	  public List<CandleStickResponse> latestCandles;
   }

   /// <summary>
   /// The GET error response received from instruments/instrument/candles
   /// </summary>
   public class PricingCandlesErrorResponse : ErrorResponse
   {
   }
}