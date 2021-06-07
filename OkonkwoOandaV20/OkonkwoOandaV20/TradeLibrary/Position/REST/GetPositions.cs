using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
	  /// <summary>
	  /// Retrieves all positions for a given account
	  /// </summary>
	  /// <param name="accountID">positions will be retrieved for this account id</param>
	  /// <returns>List of Position objects with the details for each position (or empty list iff no positions)</returns>
	  public static async Task<List<Position.Position>> GetPositionsAsync(string accountID)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/positions",
			Method = "GET"
		 };

		 var response = await MakeRequestAsync<PositionsResponse, PositionsErrorResponse>(request);

		 return response.positions ?? new List<Position.Position>();
	  }
   }

   /// <summary>
   /// The GET success response received from accounts/accountID/positions/instrument/close
   /// </summary>
   public class PositionsResponse : Response
   {
	  /// <summary>
	  /// The list of Account Positions
	  /// </summary>
	  public List<Position.Position> positions { get; set; }
   }

   /// <summary>
   /// The GET error response received from accounts/accountID/positions/instrument/close
   /// </summary>
   public class PositionsErrorResponse : ErrorResponse
   {
   }
}
