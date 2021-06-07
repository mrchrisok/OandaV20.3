using Newtonsoft.Json;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.REST.Streaming;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
	  /// <summary>
	  /// Get a stream of Transactions for an Account starting from when the request is made.
	  /// https://developer.oanda.com/rest-live-v20/transaction-ep/#collapse_endpoint_6
	  /// </summary>
	  /// <param name="accountID">the account ID you want to stream on</param>
	  /// <returns>the WebResponse object that can be used to retrieve the events as they stream</returns>
	  public static async Task<WebSession> GetTransactionsStreamAsync(string accountID)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.TransactionsStream)}accounts/{accountID}/transactions/stream",
			Method = "GET",
			Parameters = new TransactionsStreamParameters()
		 };

		 var response = await MakeSessionRequestAsync(request);

		 return response;
	  }

	  public class TransactionsStreamParameters : Parameters
	  {
		 public override AcceptDatetimeFormat? AcceptDatetimeFormat
		 {
			get => null;
			set => base.AcceptDatetimeFormat = value;
		 }
	  }
   }

   /// <summary>
   /// Events are authorized transactions posted to the subject account.
   /// http://developer.oanda.com/rest-live-v20/transaction-ep/#collapse_endpoint_6
   /// </summary>
   [JsonConverter(typeof(TransactionsStreamResponseConverter))]
   public class TransactionsStreamResponse : StreamResponse
   {
	  /// <summary>
	  /// The most recent Transaction completed for the Account
	  /// </summary>
	  public ITransaction transaction { get; set; }
   }

   public class TransactionsHeartbeat : Heartbeat
   {
	  /// <summary>
	  /// The ID of the most recent Transaction created for the Account
	  /// </summary>
	  public long? lastTransactionID { get; set; }
   }

   public class TransactionsSession : StreamSession<TransactionsStreamResponse>
   {
	  public TransactionsSession(string accountID) : base(accountID)
	  {
	  }

	  protected override async Task<WebSession> GetSessionAsync()
	  {
		 return await Rest20.GetTransactionsStreamAsync(_accountID);
	  }
   }
}
