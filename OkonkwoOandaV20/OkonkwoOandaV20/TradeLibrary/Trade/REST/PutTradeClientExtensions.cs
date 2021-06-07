using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {

	  /// <summary>
	  /// Update the Client Extensions for a Trade. Do not add, update, or delete the Client Extensions if your account is associated with MT4.
	  /// </summary>
	  /// <param name="accountID">Account identifier</param>
	  /// <param name="tradeSpecifier">Specifier for the Trade</param>
	  /// <param name="parameters">The parameters for the request</param>
	  /// <returns>PostOrderResponse with details of the results (throws if if fails)</returns>
	  public static async Task<TradeClientExtensionsResponse> PutTradeClientExtensionsAsync(string accountID, long tradeSpecifier, TradeClientExtensionsParameters parameters)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/trades/{tradeSpecifier}/clientExtensions",
			Method = "PUT",
			Parameters = parameters ?? new TradeClientExtensionsParameters()
		 };

		 var response = await MakeRequestAsync<TradeClientExtensionsResponse, TradeClientExtensionsErrorResponse>(request);

		 return response;
	  }

	  public class TradeClientExtensionsParameters : Parameters
	  {
		 /// <summary>
		 /// The Client Extensions to update the Trade with. Do not add, update, or
		 /// delete the Client Extensions if your account is associated with MT4.
		 /// </summary>
		 [Body]
		 public ClientExtensions clientExtensions { get; set; }
	  }
   }

   /// <summary>
   /// The PUT success response received from accounts/accountID/trades/tradeSpecifier/clientExtensions
   /// </summary>
   public class TradeClientExtensionsResponse : Response
   {
	  /// <summary>
	  /// The Transaction that updates the Trade’s Client Extensions.
	  /// </summary>
	  public TradeClientExtensionsModifyTransaction tradeClientExtensionsModifyTransaction { get; set; }
   }

   /// <summary>
   /// The PUT error response received from accounts/accountID/trades/tradeSpecifier/clientExtensions
   /// </summary>
   public class TradeClientExtensionsErrorResponse : ErrorResponse
   {
	  /// <summary>
	  /// The Transaction that rejects the modification of the Trade’s Client
	  /// Extensions.
	  /// </summary>
	  public TradeClientExtensionsModifyRejectTransaction tradeClientExtensionsModifyRejectTransaction { get; set; }
   }
}
