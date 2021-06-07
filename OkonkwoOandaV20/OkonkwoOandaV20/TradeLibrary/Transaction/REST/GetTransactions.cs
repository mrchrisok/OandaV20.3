using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.Framework.TypeConverters;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
	  /// <summary>
	  /// Get a list of Transactions pages that satisfy a time-based Transaction query.
	  /// </summary>
	  /// <param name="accountID">Account identifier</param>
	  /// <param name="parameters">The parameters for the request</param>
	  /// <returns></returns>
	  public static async Task<List<ITransaction>> GetTransactionsAsync(string accountID, TransactionsParameters parameters)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/transactions",
			Method = "GET",
			Parameters = parameters
		 };

		 var pagesResponse = await MakeRequestAsync<TransactionPagesResponse, TransactionPagesErrorResponse>(request);

		 var transactions = new List<ITransaction>();
		 foreach (string page in pagesResponse.pages)
		 {
			var pageParams = new TransactionsByIdRangeParameters
			{
			   AcceptDatetimeFormat = request.Parameters.AcceptDatetimeFormat,
			   page = page,
			   type = parameters.type
			};

			transactions.AddRange(await GetTransactionsByIdRangeAsync(accountID, pageParams));

			await Task.Delay(parameters.pagingDelayMilliSeconds); // throttle
		 }

		 return transactions;
	  }

	  public class TransactionsParameters : Parameters
	  {
		 /// <summary>
		 /// The starting time (inclusive) of the time range for the Transactions being queried. 
		 /// [default=Account Creation Time]
		 /// </summary>
		 [Query]
		 public DateTime? from { get; set; }

		 /// <summary>
		 /// The ending time (inclusive) of the time range for the Transactions being queried. 
		 /// [default=Request Time]
		 /// </summary>
		 [Query]
		 public DateTime? to { get; set; }

		 /// <summary>
		 /// The number of Transactions to include in each page of the results. [default=100, maximum=1000]
		 /// </summary>
		 [Query]
		 public int? pageSize { get; set; }

		 /// <summary>
		 /// A filter for restricting the types of Transactions to retrieve.
		 /// The valid values are defined in the TransactionFilter class.
		 /// </summary>
		 [Query(converter: typeof(ListToCsvConverter))]
		 public List<string> type { get; set; }

		 /// <summary>
		 /// Number of milliSeconds by which to throttle the transactions page requests. Throttled retrieval
		 /// may be used optionally in test or resource-constrained production environments.
		 /// </summary>
		 [JsonIgnore]
		 public int pagingDelayMilliSeconds { get; set; }
	  }
   }

   /// <summary>
   /// The GET success response received from accounts/accountID/transactions
   /// </summary>
   public class TransactionPagesResponse : Response
   {
	  /// <summary>
	  /// The starting time provided in the request.
	  /// </summary>
	  public DateTime? from { get; set; }

	  /// <summary>
	  /// The ending time provided in the request.
	  /// </summary>
	  public DateTime? to { get; set; }

	  /// <summary>
	  /// The pageSize provided in the requests
	  /// </summary>
	  public int pageSize { get; set; }

	  /// <summary>
	  /// The Transaction-type filter provided in the request
	  /// </summary>
	  public List<string> type { get; set; }

	  /// <summary>
	  /// The number of Transactions that are contained in the pages returned
	  /// </summary>
	  public int count { get; set; }

	  /// <summary>
	  /// The list of URLs that represent idrange queries providing the data for
	  /// each page in the query results
	  /// </summary>
	  public List<string> pages { get; set; }
   }

   /// <summary>
   /// The GET error response received from accounts/accountID/transactions
   /// </summary>
   public class TransactionPagesErrorResponse : ErrorResponse
   {
   }

   #region base classes
   public class TransactionsResponse : Response
   {
	  /// <summary>
	  /// The list of Transactions that satisfy the request.
	  /// </summary>
	  [JsonConverter(typeof(TransactionConverter))]
	  public List<ITransaction> transactions;
   }

   public class TransactionsErrorResponse : ErrorResponse
   {
   }
   #endregion
}
