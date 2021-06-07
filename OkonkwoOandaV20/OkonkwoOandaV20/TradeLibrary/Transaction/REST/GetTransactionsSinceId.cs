using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.TypeConverters;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
	  /// <summary>
	  /// Get a range of Transactions for an Account starting at (but not including) a provided Transaction ID.
	  /// http://developer.oanda.com/rest-live-v20/transaction-ep/#_collapse_endpoint_5
	  /// </summary>
	  /// <param name="accountID">the id of the account to which the transactions belongs</param>
	  /// <param name="parameters">the parameters for the request</param>
	  /// <returns>A list of transaction objects</returns>
	  public static async Task<List<ITransaction>> GetTransactionsSinceIdAsync(string accountID, TransactionsSinceIdParameters parameters)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/transactions/sinceid",
			Method = "GET",
			Parameters = parameters
		 };

		 var response = await MakeRequestAsync<TransactionsSinceIdResponse, TransactionsSinceIdErrorResponse>(request);

		 return response.transactions;
	  }

	  public class TransactionsSinceIdParameters : Parameters
	  {
		 /// <summary>
		 /// The ID of the last Transaction fetched. 
		 /// This query will return all Transactions newer than the TransactionID. [required]
		 /// </summary>
		 [Query]
		 public long id { get; set; }

		 /// <summary>
		 /// A filter for restricting the types of Transactions to retrieve.
		 /// The valid values are defined in the TransactionFilter class.
		 /// </summary>
		 [Query(converter: typeof(ListToCsvConverter))]
		 public List<string> type { get; set; }
	  }
   }

   /// <summary>
   /// The GET success response received from accounts/accountID/transactions/sinceid
   /// </summary>
   public class TransactionsSinceIdResponse : TransactionsResponse
   {
   }

   /// <summary>
   /// The GET error response received from accounts/accountID/transactions/sinceid
   /// </summary>
   public class TransactionsSinceIdErrorResponse : TransactionsErrorResponse
   {
   }
}
