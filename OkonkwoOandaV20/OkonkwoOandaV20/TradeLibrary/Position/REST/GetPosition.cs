using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
	  /// <summary>
	  /// Retrieves the current position for the given instrument and account
	  /// This will cause an error if there is no position for that instrument 
	  /// </summary>
	  /// <param name="accountID">the account for which to get the position</param>
	  /// <param name="instrument">the instrument for which to get the position</param>
	  /// <returns>Position object with the details of the position</returns>
	  public static async Task<Position.Position> GetPositionAsync(string accountID, string instrument)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/positions/{instrument}",
			Method = "GET"
		 };

		 var response = await MakeRequestAsync<PositionResponse, PositionErrorResponse>(request);

		 return response.position;
	  }
   }

   /// <summary>
   /// The GET success response received from accounts/accountID/positions/instrument
   /// </summary>
   public class PositionResponse : Response
   {
	  /// <summary>
	  /// The requested Position
	  /// </summary>
	  public Position.Position position { get; set; }
   }

   /// <summary>
   /// The GET error response received from accounts/accountID/positions/instrument
   /// </summary>
   public class PositionErrorResponse : ErrorResponse
   {
   }
}
