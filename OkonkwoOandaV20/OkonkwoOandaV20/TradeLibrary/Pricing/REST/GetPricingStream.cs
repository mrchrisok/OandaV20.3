using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.Framework.TypeConverters;
using OkonkwoOandaV20.TradeLibrary.Pricing;
using OkonkwoOandaV20.TradeLibrary.REST.Streaming;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
	  /// <summary>
	  /// Get a stream of Account Prices starting from when the request is made. This pricing stream does not include every 
	  /// single price created for the Account, but instead will provide at most 4 prices per second(every 250 milliseconds) 
	  /// for each instrument being requested. If more than one price is created for an instrument during the 250 millisecond 
	  /// window, only the price in effect at the end of the window is sent.This means that during periods of rapid price movement, 
	  /// subscribers to this stream will not be sent every price. Pricing windows for different connections to the price stream 
	  /// are not all aligned in the same way (i.e.they are not all aligned to the top of the second). This means that during 
	  /// periods of rapid price movement, different subscribers may observe different prices depending on their alignment.
	  /// https://developer.oanda.com/rest-live-v20/pricing-ep/#collapse_endpoint_4
	  /// </summary>
	  /// <param name="accountID"></param>
	  /// <param name="parameters">The parameters for the request</param>
	  /// <returns>The WebResponse object that can be used to retrieve the prices as they stream</returns>
	  public static async Task<WebSession> GetPricingStreamAsync(string accountID, PricingStreamParameters parameters)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.PricingStream)}accounts/{accountID}/pricing/stream",
			Method = "GET",
			Parameters = parameters
		 };

		 var webSession = await MakeSessionRequestAsync(request);

		 return webSession;
	  }

	  public class PricingStreamParameters : Parameters
	  {
		 public PricingStreamParameters() { snapshot = true; }

		 /// <summary>
		 /// List of Instruments to stream Prices for. [required]
		 /// </summary>
		 [Query(converter: typeof(ListToCsvConverter))]
		 public List<string> instruments { get; set; }

		 /// <summary>
		 /// Flag that enables/disables the sending of a pricing snapshot when 
		 /// initially connecting to the stream. [default=True]
		 /// </summary>
		 [Query]
		 public bool snapshot { get; set; }

		 /// <summary>
		 /// Flag that enables the inclusion of the homeConversions field in the returned response. 
		 /// An entry will be returned for each currency in the set of all base and quote currencies 
		 /// present in the requested instruments list. [default=False]
		 /// </summary>
		 [Query]
		 public bool includeHomeConversions { get; set; }
	  }
   }

   /// <summary>
   /// Events are authorized transactions posted to the subject account.
   /// For more information, visit: http://developer.oanda.com/rest-live-v20/transaction-ep/
   /// </summary>
   [JsonConverter(typeof(PricingStreamResponseConverter))]
   public class PricingStreamResponse : StreamResponse
   {
	  public Price price { get; set; }
   }

   public class PricingHeartbeat : Heartbeat
   {
   }

   public class PricingSession : StreamSession<PricingStreamResponse>
   {
	  private readonly List<Instrument.Instrument> _instruments;
	  private bool _snapshot;
	  private bool _includeHomeConversions;

	  public PricingSession(string accountID, List<Instrument.Instrument> instruments, bool snapshot = true, bool includeHomeConversions = false)
		 : base(accountID)
	  {
		 _instruments = instruments;
		 _snapshot = snapshot;
		 _includeHomeConversions = includeHomeConversions;
	  }

	  protected override async Task<WebSession> GetSessionAsync()
	  {
		 var instruments = new List<string>();
		 _instruments.ForEach(instrument => instruments.Add(instrument.name));

		 var parameters = new Rest20.PricingStreamParameters()
		 {
			instruments = instruments,
			snapshot = _snapshot,
			includeHomeConversions = _includeHomeConversions
		 };

		 return await Rest20.GetPricingStreamAsync(_accountID, parameters);
	  }
   }
}
