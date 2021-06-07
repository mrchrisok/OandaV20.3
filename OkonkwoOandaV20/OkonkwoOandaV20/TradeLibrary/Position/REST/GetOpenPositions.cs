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
	  /// Retrieves the current open positions for a given account
	  /// </summary>
	  /// <param name="accountID">positions will be retrieved for this account id</param>
	  /// <returns>A list of Position objects with the details for each position (or empty list if no positions)</returns>
	  public static async Task<List<Position.Position>> GetOpenPositionsAsync(string accountID)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/openPositions",
			Method = "GET"
		 };

		 var response = await MakeRequestAsync<OpenPositionsResponse, OpenPositionsErrorResponse>(request);

		 return response.positions ?? new List<Position.Position>();
	  }
   }

   /// <summary>
   /// The GET success response received from accounts/accountID/openPositions
   /// </summary>
   public class OpenPositionsResponse : PositionsResponse
   {
   }

   /// <summary>
   /// The GET error response received from accounts/accountID/openPositions
   /// </summary>
   public class OpenPositionsErrorResponse : PositionsErrorResponse
   {
   }
}
