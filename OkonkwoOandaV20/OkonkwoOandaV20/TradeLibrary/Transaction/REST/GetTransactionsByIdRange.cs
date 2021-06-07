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
	  /// Get a range of Transactions for an Account based on the Transaction IDs
	  /// http://developer.oanda.com/rest-live-v20/transaction-ep/
	  /// </summary>
	  /// <param name="accountID">Account identifier</param>
	  /// <param name="parameters">The parameters for the request</param>
	  /// <returns></returns>
	  public static async Task<List<ITransaction>> GetTransactionsByIdRangeAsync(string accountID, TransactionsByIdRangeParameters parameters)
	  {
		 TransactionsResponse response;

		 if (!string.IsNullOrEmpty(parameters.page))
		 {
			var request = new Request()
			{
			   Uri = parameters.page,
			   Method = "GET"
			};

			response = await MakeRequestAsync<TransactionsResponse>(request);
		 }
		 else
		 {
			var request = new Request()
			{
			   Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/transactions/idrange",
			   Method = "GET",
			   Parameters = parameters
			};

			response = await MakeRequestAsync<TransactionsByIdRangeResponse, TransactionsByIdRangeErrorResponse>(request);
		 }

		 return response.transactions;
	  }

	  public class TransactionsByIdRangeParameters : Parameters
	  {
		 /// <summary>
		 /// The URI that represents the idrange query providing the data for the query results
		 /// </summary>
		 public string page { get; set; }

		 /// <summary>
		 /// The starting Transacion ID (inclusive) to fetch. [required]
		 /// </summary>
		 [Query]
		 public long from { get; set; }

		 /// <summary>
		 /// The ending Transaction ID (inclusive) to fetch. [required]
		 /// </summary>
		 [Query]
		 public long to { get; set; }

		 /// <summary>
		 /// A comma separated list (csv) that restricts the types of Transactions to retreive.
		 /// The valid values are defined in the TransactionFilter class.
		 /// </summary>
		 [Query(converter: typeof(ListToCsvConverter))]
		 public List<string> type { get; set; }
	  }
   }

   /// <summary>
   /// The GET success response received from accounts/accountID/transactions/idrange
   /// </summary>
   public class TransactionsByIdRangeResponse : TransactionsResponse
   {
   }

   /// <summary>
   /// The GET error response received from accounts/accountID/transactions/idrange
   /// </summary>
   public class TransactionsByIdRangeErrorResponse : TransactionsErrorResponse
   {
   }
}
