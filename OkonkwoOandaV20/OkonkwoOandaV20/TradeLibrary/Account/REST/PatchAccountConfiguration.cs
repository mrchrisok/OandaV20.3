using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.TradeLibrary.Transaction;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
	  /// <summary>
	  /// Set the client-configurable portions onf an Account
	  /// http://developer.oanda.com/rest-live-v20/account-ep/
	  /// </summary>
	  /// <param name="accountID">Account Identifier</param>
	  /// <param name="parameters">The parameters for the request</param>
	  /// <returns>an AccountConfigurationResponse object containing the updated values that were applied to the account</returns>
	  public static async Task<AccountConfigurationResponse> PatchAccountConfigurationAsync(string accountID, AccountConfigurationParameters parameters)
	  {
		 var request = new Request()
		 {
			Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/configuration",
			Method = "PATCH",
			Parameters = parameters
		 };

		 var response = await MakeRequestAsync<AccountConfigurationResponse, AccountConfigurationErrorResponse>(request);

		 return response;
	  }

	  public class AccountConfigurationParameters : Parameters
	  {
		 /// <summary>
		 /// Client-defined alias (name) for the Account
		 /// </summary>
		 [DataMember(EmitDefaultValue = false)]
		 [Body]
		 public string alias { get; set; }

		 /// <summary>
		 /// The margin rate the Account should be set to.
		 /// </summary>
		 [DataMember(EmitDefaultValue = false)]
		 [JsonConverter(typeof(StringDecimalConverter))]
		 [Body]
		 public decimal? marginRate { get; set; }
	  }
   }

   /// <summary>
   /// The PATCH success response received from the accounts/accountID/configuration
   /// </summary>
   public class AccountConfigurationResponse : Response
   {
	  /// <summary>
	  /// The transaction that configures the Account.
	  /// </summary>
	  public ClientConfigureTransaction clientConfigureTransaction;
   }

   /// <summary>
   /// The PATCH success response received from the accounts/accountID/configuration
   /// </summary>
   public class AccountConfigurationErrorResponse : ErrorResponse
   {
	  /// <summary>
	  /// The transaction that rejects the configuration of the Account.
	  /// </summary>
	  public ClientConfigureRejectTransaction clientConfigureRejectTransaction { get; set; }
   }
}