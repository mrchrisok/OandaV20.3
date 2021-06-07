using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.TradeLibrary.Instrument;
using System;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/account-ep/
   /// </summary>
   public partial class Rest20
   {
	  /// <summary>
	  /// Fetch a position book for an instrument
	  /// </summary>
	  /// <param name="instrument">Name of the Instrument [required]</param>
	  /// <param name="parameters">the parameters for the request</param>
	  /// <returns>a PositionBook object</returns>
	  public static async Task<PositionBook> GetInstrumentPositionBookAsync(string instrument, InstrumentPositionBookParameters parameters)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}instruments/{instrument}/positionBook",
			Method = "GET",
			Parameters = parameters
		 };

		 InstrumentPositionBookResponse response;
		 try
		 {
			response = await MakeRequestAsync<InstrumentPositionBookResponse, InstrumentPositionBookErrorResponse>(request);
		 }
		 catch (Exception ex)
		 {
			if (parameters.getLastTimeOnFailure)
			{
			   parameters.time = null;
			   response = await MakeRequestAsync<InstrumentPositionBookResponse, InstrumentPositionBookErrorResponse>(request);
			}
			else
			   throw ex;
		 }

		 return response.positionBook;
	  }

	  public class InstrumentPositionBookParameters : Parameters
	  {
		 public InstrumentPositionBookParameters(bool getLastTimeOnFailure = true)
		 {
			this.getLastTimeOnFailure = getLastTimeOnFailure;
		 }

		 /// <summary>
		 /// The time of the snapshot to fetch. If not specified, then the most recent snapshot 
		 /// is fetched.
		 /// </summary>
		 [Query]
		 public DateTime? time { get; set; }

		 /// <summary>
		 /// Flag that indicates if the most recent PositionBook snapshot should be fetched if the
		 /// snapshot specified by the 'time' parameter cannot be retrieved.
		 /// </summary>
		 [JsonIgnore]
		 public bool getLastTimeOnFailure { get; set; }
	  }
   }

   /// <summary>
   /// The GET success response received from instruments/instrument/positionBook
   /// </summary>
   public class InstrumentPositionBookResponse : Response
   {
	  /// <summary>
	  /// The instrument’s position book
	  /// </summary>
	  public PositionBook positionBook;
   }

   /// <summary>
   /// The GET error response received from instruments/instrument/positionBook
   /// </summary>
   public class InstrumentPositionBookErrorResponse : ErrorResponse
   {
   }
}