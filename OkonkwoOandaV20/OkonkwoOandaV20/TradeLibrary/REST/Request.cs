using Newtonsoft.Json;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.JsonConverters;
using OkonkwoOandaV20.Framework.TypeConverters;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   public class Request
   {
	  private string _uri;

	  /// <summary>
	  /// The URI of the host server.
	  /// </summary>
	  public string Uri
	  {
		 get => $"{_uri}{CreateQueryString(Query)}";
		 set => _uri = value;
	  }

	  /// <summary>
	  /// The HttpMethod that will be used to send the request message.
	  /// </summary>
	  public string Method { get; set; }

	  /// <summary>
	  /// The Parameters that will be used to construct the request message.
	  /// </summary>
	  public Parameters Parameters { get; set; }

	  /// <summary>
	  /// A collection of headers that will be sent with the request.
	  /// </summary>
	  internal IDictionary<string, string> Headers
	  {
		 get
		 {
			var headers = Parameters?.GetRequestParameters<HeaderAttribute>(TypeConverters);

			return headers?.ToDictionary(x => x.Key, x => x.Value.ToString());
		 }
	  }

	  /// <summary>
	  /// A collection of query parameters that will be appended to the request Uri.
	  /// </summary>
	  internal IDictionary<string, string> Query
	  {
		 get
		 {
			var queryProperties = Parameters?.GetRequestParameters<QueryAttribute>(TypeConverters);

			return queryProperties?.ToDictionary(x => x.Key, x => x.Value.ToString());
		 }
	  }

	  private string _cachedBody;
	  /// <summary>
	  /// A Json string that will be written into the body of the request.
	  /// </summary>
	  internal string Body
	  {
		 get
		 {
			if (_cachedBody == null)
			{
			   var bodyProperties = Parameters?.GetRequestParameters<BodyAttribute>();

			   if (bodyProperties?.Count > 0)
			   {
				  _cachedBody = JsonConvert.SerializeObject(bodyProperties, JsonSerializerSettings);
			   }
			}

			return _cachedBody;
		 }
	  }

	  /// <summary>
	  /// The serializer settings used to serialize the request body.
	  /// The same settings will be used to de-serialize the response.
	  /// </summary>
	  internal IList<ITypeConverter<string>> TypeConverters
	  {
		 get
		 {
			IList<ITypeConverter<string>> getConverters()
			{
			   if (Parameters?.AcceptDatetimeFormat.HasValue ?? false)
			   {
				  var acceptDateTimeToStringConverter = new AcceptDateTimeToStringConverter(Parameters.AcceptDatetimeFormat.Value);
				  return new List<ITypeConverter<string>>() { acceptDateTimeToStringConverter };
			   }
			   return null;
			}

			return getConverters();
		 }
	  }

	  /// <summary>
	  /// The serializer settings used to serialize the request body.
	  /// The same settings will be used to de-serialize the response.
	  /// </summary>
	  internal JsonSerializerSettings JsonSerializerSettings
	  {
		 get
		 {
			IList<JsonConverter> getConverters()
			{
			   if (Parameters?.AcceptDatetimeFormat.HasValue ?? false)
			   {
				  var acceptDateTimeConverter = new AcceptDateTimeConverter(Parameters.AcceptDatetimeFormat.Value);
				  return new List<JsonConverter>() { acceptDateTimeConverter };
			   }
			   return null;
			}

			var settings = new JsonSerializerSettings()
			{
			   TypeNameHandling = TypeNameHandling.None,
			   NullValueHandling = NullValueHandling.Ignore,
			   Converters = getConverters()
			};

			return settings;
		 }
	  }

	  #region utilities

	  /// <summary>
	  /// Creates a query string from a dictionary of parameters
	  /// </summary>
	  /// <param name="queryParams">The parameters dictionary to convert to a query string.</param>
	  /// <returns>A query string</returns>
	  private static string CreateQueryString(IDictionary<string, string> queryParams, bool includeQuestionMark = true)
	  {
		 if (queryParams == null)
		 {
			return null;
		 }

		 if (queryParams.Count == 0)
		 {
			return null;
		 }

		 string queryString = "";
		 foreach (var pair in queryParams)
		 {
			queryString += $"{WebUtility.UrlEncode(pair.Key)}={WebUtility.UrlEncode(pair.Value)}&";
		 }
		 queryString = queryString.Trim('&');

		 var questionMark = includeQuestionMark ? "?" : null;

		 return $"{questionMark}{queryString}";
	  }

	  #endregion
   }
}
