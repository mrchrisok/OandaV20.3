using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.TypeConverters;
using OkonkwoOandaV20.TradeLibrary.Pricing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public partial class Rest20
   {
      /// <summary>
      /// Get pricing information for a specified list of instrumentes within an Account
      /// https://developer.oanda.com/rest-live-v20/pricing-ep/#collapse_endpoint_3
      /// </summary>
      /// <param name="accountID">Account Identifier</param>
      /// <param name="parameters">The parameters for the request</param>
      /// <returns></returns>
      public static async Task<List<Price>> GetPricingAsync(string accountID, PricingParameters parameters)
      {
         var request = new Request()
         {
            Uri = $"{ServerUri(EServer.Account)}accounts/{accountID}/pricing",
            Method = "GET",
            Parameters = parameters
         };

         if (!(parameters?.instruments?.Count > 0))
         {
            throw new ArgumentException("List of instruments cannot be null or empty.");
         }

         var response = await MakeRequestAsync<PricingResponse, PricingErrorResponse>(request);

         return response.prices ?? new List<Price>();
      }

      public class PricingParameters : Parameters
      {
         /// <summary>
         /// List of Instruments to get pricing for. [required]
         /// </summary>
         [Query(converter: typeof(ListToCsvConverter))]
         public List<string> instruments { get; set; }

         /// <summary>
         /// Date/Time filter to apply to the response. Only prices and home conversions (if requested) with a 
         /// time later than this filter (i.e. the price has changed after the since time) will be provided, 
         /// and are filtered independently.
         /// </summary>
         [Query]
         public DateTime? since { get; set; }

         /// <summary>
         /// Flag that enables the inclusion of the unitsAvailable field in the returned Price objects. [default=True]
         /// </summary>
         [Obsolete("Deprecated: Wil be removed in a future API update.")]
         [Query]
         public bool? includeUnitsAvailable { get; set; }

         /// <summary>
         /// Flag that enables the inclusion of the homeConversions field in the returned response. 
         /// An entry will be returned for each currency in the set of all base and quote currencies present 
         /// in the requested instruments list. [default=False]
         /// </summary>
         [Query]
         public bool? includeHomeConversions { get; set; }
      }
   }

   /// <summary>
   /// The GET success response received from accounts/accountID/pricing
   /// </summary>
   public class PricingResponse : Response
   {
      /// <summary>
      /// The list of Price objects requested.
      /// </summary>
      public List<Price> prices;

      /// <summary>
      /// The list of home currency conversion factors requested. This field will
      /// only be present if includeHomeConversions was set to true in the request.
      /// </summary>
      public List<HomeConversions> homeConversions;

      /// <summary>
      /// The DateTime value to use for the “since” parameter in the next poll request.
      /// </summary>
      public DateTime? time;
   }

   /// <summary>
   /// The GET error response received from accounts/accountID/pricing
   /// </summary>
   public class PricingErrorResponse : ErrorResponse
   {
   }
}
