using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
	  /// <summary>
	  /// Close the given position
	  /// This will close all trades on the provided account/instrument
	  /// </summary>
	  /// <param name="accountID">the account to close trades on</param>
	  /// <param name="instrument">the instrument for which to close all trades</param>
	  /// <param name="parameters">the parameters for the request</param>
	  /// <returns>DeletePositionResponse object containing details about the actions taken</returns>
	  public static async Task<PositionCloseResponse> PutPositionCloseAsync(string accountID, string instrument, PositionCloseParameters parameters)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/positions/{instrument}/close",
			Method = "PUT",
			Parameters = parameters
		 };

		 var response = await MakeRequestAsync<PositionCloseResponse, PositionCloseErrorResponse>(request);

		 return response;
	  }

	  public class PositionCloseParameters : Parameters
	  {
		 /// <summary>
		 /// Indication of how much of the long Position to closeout. Either the
		 /// string “ALL”, the string “NONE”, or a DecimalNumber representing how many
		 /// units of the long position to close using a PositionCloseout MarketOrder.
		 /// The units specified must always be positive.
		 /// </summary>
		 [Body]
		 public string longUnits { get; set; }

		 /// <summary>
		 /// The client extensions to add to the MarketOrder used to close the long 
		 /// position.
		 /// </summary>
		 [Body]
		 public ClientExtensions longClientExtensions { get; set; }

		 /// <summary>
		 /// Indication of how much of the short Position to closeout. Either the
		 /// string “ALL”, the string “NONE”, or a DecimalNumber representing how many
		 /// units of the short position to close using a PositionCloseout
		 /// MarketOrder. The units specified must always be positive.
		 /// </summary>
		 [Body]
		 public string shortUnits { get; set; }

		 /// <summary>
		 /// The client extensions to add to the MarketOrder used to close the short
		 /// position.
		 /// </summary>
		 [Body]
		 public ClientExtensions shortClientExtensions { get; set; }
	  }
   }

   /// <summary>
   /// The PUT success response received from accounts/accountID/positions/instrument/close
   /// </summary>
   public class PositionCloseResponse : Response
   {
	  /// <summary>
	  /// The MarketOrderTransaction created to close the long Position.
	  /// </summary>
	  public MarketOrderTransaction longOrderCreateTransaction { get; set; }

	  /// <summary>
	  /// OrderFill Transaction that closes the long Position
	  /// </summary>
	  public OrderFillTransaction longOrderFillTransaction { get; set; }

	  /// <summary>
	  /// OrderCancel Transaction that cancels the MarketOrder created to close the
	  /// long Position
	  /// </summary>
	  public OrderCancelTransaction longOrderCancelTransaction { get; set; }

	  /// <summary>
	  /// The MarketOrderTransaction created to close the short Position.
	  /// </summary>
	  public MarketOrderTransaction shortOrderCreateTransaction { get; set; }

	  /// <summary>
	  /// OrderFill Transaction that closes the short Position
	  /// </summary>
	  public OrderFillTransaction shortOrderFillTransaction { get; set; }

	  /// <summary>
	  ///  OrderCancel Transaction that cancels the MarketOrder created to close the 
	  ///  short Position
	  /// </summary>
	  public OrderCancelTransaction shortOrderCancelTransaction { get; set; }
   }

   /// <summary>
   /// The PUT error response received from accounts/accountID/positions/instrument/close
   /// </summary>
   public class PositionCloseErrorResponse : ErrorResponse
   {
	  /// <summary>
	  /// The Transaction created that rejects the creation of a MarketOrder to
	  /// close the long Position. Only present if the Account exists and a long
	  /// Position was specified.
	  /// </summary>
	  public MarketOrderRejectTransaction longOrderRejectTransaction { get; set; }

	  /// <summary>
	  /// The Transaction created that rejects the creation of a MarketOrder to
	  /// close the short Position. Only present if the Account exists and a short
	  /// Position was specified.
	  /// </summary>
	  public MarketOrderRejectTransaction shortOrderRejectTransaction { get; set; }
   }
}