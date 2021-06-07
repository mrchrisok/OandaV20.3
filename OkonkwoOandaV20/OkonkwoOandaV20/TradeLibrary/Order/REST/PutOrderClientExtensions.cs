using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
	  /// <summary>
	  /// Modify the Client Extensions for the order in the given account.
	  /// http://developer.oanda.com/rest-live-v20/order-ep/
	  /// </summary>
	  /// <param name="accountId">the identifier of the account to post on</param>
	  /// <param name="orderSpecifier">the orderSpecifier to update extensions for</param>
	  /// <param name="parameters">the parameters for the request</param>
	  /// <returns>an OrderClientExtensionsModifyResponse (throws an OrderClientExtensionsModifyErrorResponse if the request fails.)</returns>
	  public static async Task<OrderClientExtensionsResponse> PutOrderClientExtensionsAsync(string accountID, long orderSpecifier, OrderClientExtensionsParameters parameters)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/orders/{orderSpecifier}/clientExtensions",
			Method = "PUT",
			Parameters = parameters
		 };

		 var response = await MakeRequestAsync<OrderClientExtensionsResponse, OrderClientExtensionsErrorResponse>(request);

		 return response;
	  }

	  public class OrderClientExtensionsParameters : Parameters
	  {
		 /// <summary>
		 /// The Client Extensions to update for the Order. Do not set, modify, or
		 /// delete clientExtensions if your account is associated with MT4.
		 /// </summary>
		 [Body]
		 public ClientExtensions clientExtensions { get; set; }

		 /// <summary>
		 /// The Client Extensions to update for the Trade created when the Order is
		 /// filled. Do not set, modify, or delete clientExtensions if your account is
		 /// associated with MT4.
		 /// </summary>
		 [Body]
		 public ClientExtensions tradeClientExtensions { get; set; }
	  }
   }

   /// <summary>
   /// The PUT success response received from accounts/accountID/orders/orderSpecifier/clientExtensions
   /// </summary>
   public class OrderClientExtensionsResponse : Response
   {
	  /// <summary>
	  /// The Transaction that modified the Client Extensions for the Order
	  /// </summary>
	  public OrderClientExtensionsModifyTransaction orderClientExtensionsModifyTransaction { get; set; }
   }

   /// <summary>
   /// The PUT error response received from accounts/accountID/orders/orderSpecifier/clientExtensions
   /// </summary>
   public class OrderClientExtensionsErrorResponse : ErrorResponse
   {
	  /// <summary>
	  /// The Transaction that rejected the modification of the Client Extensions 
	  /// for the Order. Only present if the Account exists.
	  /// </summary>
	  public OrderClientExtensionsModifyRejectTransaction orderClientExtensionsModifyRejectTransaction { get; set; }
   }
}