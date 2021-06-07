using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
	  /// <summary>
	  /// Gets the details for a specific Trade in an Account
	  /// </summary>
	  /// <param name="accountID">Account identifier</param>
	  /// <param name="tradeSpecifier">Specifier for the Trade</param>
	  /// <returns>TradeData object containing the details of the trade</returns>
	  public static async Task<Trade.Trade> GetTradeAsync(string accountID, long tradeSpecifier, TradeParameters parameters = null)
	  {
		 //string uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/trades/{tradeSpecifier}";

		 //var response = await MakeRequestAsync<TradeResponse, TradeErrorResponse>(uri, headers: parameters.Headers);

		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/trades/{tradeSpecifier}",
			Method = "GET",
			Parameters = parameters ?? new TradeParameters()
		 };

		 var response = await MakeRequestAsync<TradeResponse, TradeErrorResponse>(request);

		 return response.trade;
	  }

	  public class TradeParameters : Parameters
	  {
	  }
   }

   /// <summary>
   /// The GET success response received from accounts/accountID/trades/tradeSpecifier
   /// </summary>
   public class TradeResponse : Response
   {
	  /// <summary>
	  /// The details for the Trade requested
	  /// </summary>
	  public Trade.Trade trade { get; set; }
   }

   /// <summary>
   /// The GET error response received from accounts/accountID/trades/tradeSpecifier
   /// </summary>
   public class TradeErrorResponse : ErrorResponse
   {
   }
}
