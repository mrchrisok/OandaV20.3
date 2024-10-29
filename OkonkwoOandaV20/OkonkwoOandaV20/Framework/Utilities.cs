using OkonkwoOandaV20.TradeLibrary.REST;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static OkonkwoOandaV20.TradeLibrary.REST.Rest20;

namespace OkonkwoOandaV20.Framework
{
   public class Utilities
   {
      /// <summary>
      /// Determines if trading is halted for the provided instrument.
      /// </summary>
      /// <param name="instrument">Instrument to check if halted. Default is EUR_USD.</param>
      /// <returns>True if trading is halted, false if trading is not halted.</returns>
      public static async Task<bool> IsMarketHaltedAsync(string instrument = InstrumentName.Currency.EURUSD, bool throwIfHalted = false)
      {
         var accountID = Credentials.GetDefaultCredentials().DefaultAccountId;
         var parameters = new PricingParameters() { instruments = new List<string>() { instrument } };

         var prices = await Rest20.GetPricingAsync(accountID, parameters);

         bool isTradeable = false, hasBids = false, hasAsks = false;

         if (prices[0] != null)
         {
            isTradeable = prices[0].tradeable;
            hasBids = prices[0].bids.Count > 0;
            hasAsks = prices[0].asks.Count > 0;
         }

         if (isTradeable && hasBids && hasAsks)  // not halted
            return false;

         if (throwIfHalted)
            throw new MarketHaltedException($"Market is halted for this instrument: {instrument}");

         return true;
      }

      /// <summary>
      /// 
      /// </summary>
      private const string DefaultRFC3339Format = "yyyy-MM-ddTHH:mm:ss.fffffffZ";
      private const string OandaRFC3339Format = "yyyy-MM-ddTHH:mm:ss.fffffffffZ";

      /// <summary>
      /// Convert DateTime (UTC only) object to a string of the indicated format.
      /// </summary>
      /// <param name="time">A DateTime object</param>
      /// <param name="format">Format type (RFC3339 or UNIX only)</param>
      /// <returns>A date-time string</returns>
      public static string ConvertDateTimeUtcToAcceptDateFormat(DateTime time, AcceptDatetimeFormat format = AcceptDatetimeFormat.RFC3339)
      {
         if (time.Kind != DateTimeKind.Utc)
         {
            throw new ArgumentException($"The provided time must be in UTC format.");
         }

         if (format == AcceptDatetimeFormat.RFC3339)
         {
            return XmlConvert.ToString(time, DefaultRFC3339Format);
         }
         else if (format == AcceptDatetimeFormat.Unix)
         {
            return time.Subtract(new DateTime(1970, 1, 1).ToUniversalTime()).TotalSeconds.ToString();
         }
         else
            throw new ArgumentException($"The format parameter '{format}' is not supported.");
      }

      /// <summary>
      /// Convert formatted time string to DateTime.
      /// The conversion is accurate within 1 milliSecond.
      /// Null will be returned if the time parameter is not convertible to a time.
      /// </summary>
      /// <param name="time">A formatted time string</param>
      /// <param name="format">Format type (RFC3339 or UNIX only)</param>
      /// <returns>A DateTime object (Utc only) or null if the time.</returns>
      public static DateTime? ConvertAcceptDateFormatDateToDateTimeUtc(string time, AcceptDatetimeFormat format = AcceptDatetimeFormat.RFC3339)
      {
         if (format == AcceptDatetimeFormat.RFC3339)
         {
            if (DateTime.TryParseExact(time, OandaRFC3339Format, null, DateTimeStyles.AssumeUniversal, out DateTime oandaDateTime))
            {
               return oandaDateTime.ToUniversalTime();
            }
            if (DateTime.TryParseExact(time, DefaultRFC3339Format, null, DateTimeStyles.AssumeUniversal, out DateTime defaultDateTime))
            {
               return defaultDateTime.ToUniversalTime();
            }
            if (DateTime.TryParse(time, out DateTime dateTime))
            {
               return dateTime.ToUniversalTime();
            }

            return null;
         }
         else if (format == AcceptDatetimeFormat.Unix)
         {
            try
            {
               var doubleTime = Convert.ToDouble(time);
               var dateTime = new DateTime(1970, 1, 1).ToUniversalTime().Add(TimeSpan.FromSeconds(doubleTime));
               return dateTime;
            }
            catch
            {
               return null;
            }
         }
         else
            throw new ArgumentException($"The format parameter '{format}' is not supported.");
      }

      /// <summary>
      /// Converts a list of items into a delimiter-separated values list.
      /// The default delimiter is a comma which returns string of comma-separated values (csv).
      /// </summary>
      /// <param name="items">The list of items to convert to a delimited string</param>
      /// <param name="delimiter">The delimeter to use to build the delimited string.</param>
      /// <returns>A delimiter-separated string of the list items</returns>
      public static string ConvertListToDelimitedValues(System.Collections.IList items, string delimiter = ",")
      {
         var stringBuilder = new StringBuilder();
         foreach (var item in items)
         {
            if (!stringBuilder.ToString().Contains(item.ToString()))
               stringBuilder.Append(item + delimiter);
         }
         return stringBuilder.ToString().Trim(delimiter.ToCharArray());
      }
   }
}
