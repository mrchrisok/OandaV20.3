using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Threading.Tasks;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   /// <summary>
   /// http://developer.oanda.com/rest-live-v20/introduction/
   /// </summary>
   public partial class Rest20
   {
	  /// <summary>
	  /// The time of the last request made to an Oanda V20 service
	  /// </summary>
	  private static DateTime m_LastRequestTime = DateTime.UtcNow;

	  /// <summary>
	  /// The V20 access token for the user's or organization's Oanda Account.
	  /// The token also authenticates operations on all sub accounts.
	  /// </summary>
	  private static string AccessToken { get { return Credentials.GetDefaultCredentials().AccessToken; } }

	  /// <summary>
	  /// Oanda recommends that requests per Account are throttled to a maximium of 100 requests/second.
	  /// http://developer.oanda.com/rest-live-v20/best-practices/
	  /// </summary>
	  public static int RequestDelayMilliSeconds = 11;

	  #region request

	  /// <summary>
	  /// Gets the base uri of the target service
	  /// </summary>
	  /// <param name="server">The enurmeration for the target service</param>
	  /// <returns>Returns the base uri of the target server</returns>
	  private static string ServerUri(EServer server) { return Credentials.GetDefaultCredentials().GetServer(server); }

	  /// <summary>
	  /// Sends a web request to a remote endpoint (uri).
	  /// </summary>
	  /// <typeparam name="T">The response type</typeparam>
	  /// <param name="request">The request specification.</param>
	  /// <returns>A success response object of type T or a failure response object of type ErrorResponse</returns>
	  private static async Task<T> MakeRequestAsync<T>(Request request)
	  {
		 return await MakeRequestAsync<T, ErrorResponse>(request);
	  }

	  /// <summary>
	  /// Sends a message request to a remote service (uri).
	  /// </summary>
	  /// <typeparam name="T">The success response type</typeparam>
	  /// <typeparam name="E">The error  response type</typeparam>
	  /// <param name="request">The request specification.</param>
	  /// <returns>A success response object of type T or a failure response object of type E</returns>
	  private static async Task<T> MakeRequestAsync<T, E>(Request request)
		 where E : IErrorResponse
	  {
		 HttpWebRequest webRequest = await CreateHttpRequestAsync(request);

		 return await GetWebResponseAsync<T, E>(webRequest, request);
	  }

	  /// <summary>
	  /// Sends a stream stream request to a remote service (uri).
	  /// </summary>
	  /// <param name="request">The Request specification for the WebResponse</param>
	  /// <returns>A WebSession that encapsulates the WebResponse containing stream handle.</returns>
	  private static async Task<WebSession> MakeSessionRequestAsync(Request request)
	  {
		 HttpWebRequest webRequest = await CreateHttpRequestAsync(request);

		 try
		 {
			WebResponse response = await webRequest.GetResponseAsync();

			var session = new WebSession(request, webRequest, response);

			return session;
		 }
		 catch (WebException ex)
		 {
			var message = GetResponseMessage<string>(ex.Response, request);
			throw new Exception(message);
		 }
	  }

	  /// <summary>
	  /// Creates the HttpWebRequest from a Request specification
	  /// </summary>
	  /// <param name="request">The Request specification</param>
	  /// <returns>An HttpWebRequest object</returns>
	  private static async Task<HttpWebRequest> CreateHttpRequestAsync(Request request)
	  {
		 HttpWebRequest webRequest = WebRequest.CreateHttp(request.Uri);
		 webRequest.Method = request.Method;
		 webRequest.ContentType = "application/json";

		 if (request.Headers != null)
		 {
			foreach (var header in request.Headers)
			{
			   webRequest.Headers[header.Key] = header.Value;
			}
		 }
		 webRequest.Headers[HttpRequestHeader.Authorization] = $"Bearer {AccessToken}";
		 webRequest.Headers[HttpRequestHeader.AcceptEncoding] = "gzip, deflate";

		 if (!string.IsNullOrWhiteSpace(request.Body))
		 {
			using (var writer = new StreamWriter(await webRequest.GetRequestStreamAsync()))
			{
			   await writer.WriteAsync(request.Body);
			}
		 }

		 return webRequest;
	  }

	  #endregion

	  #region response

	  /// <summary>
	  /// Sends an Http request to a remote service and returns the de-serialized response
	  /// </summary>
	  /// <typeparam name="T">>Type of the response returned by the remote service</typeparam>
	  /// <typeparam name="E">>Type of the error response returned by the remote service</typeparam>
	  /// <param name="webRequest">The request sent to the remote service</param>
	  /// <param name="request">The Request specification</param>
	  /// <returns>A success response object of type T or a failure response object of type E</returns>
	  private static async Task<T> GetWebResponseAsync<T, E>(HttpWebRequest webRequest, Request request)
	  {
		 while (DateTime.UtcNow < m_LastRequestTime.AddMilliseconds(RequestDelayMilliSeconds))
		 {
		 }

		 try
		 {
			using (WebResponse response = await webRequest.GetResponseAsync())
			{
			   var message = GetResponseMessage<T>(response, request);
			   return message;
			}
		 }
		 catch (WebException ex)
		 {
			// add a 'type' property for the ErrorResponseFactory
			dynamic error = GetResponseMessage<object>(ex.Response, request);
			error.type = typeof(E).Name;
			var errorResult = JsonConvert.SerializeObject(error);

			throw new Exception(errorResult);
		 }
		 finally
		 {
			m_LastRequestTime = DateTime.UtcNow;
		 }
	  }

	  /// <summary>
	  /// Deserializes and returns the message from a WebResponse
	  /// </summary>
	  /// <typeparam name="T">The response message type</typeparam>
	  /// <param name="response">A WebResponse</param>
	  /// <param name="request">The Request specification for the WebResponse</param>
	  /// <returns>The deserialized WebResponse message</returns>
	  private static T GetResponseMessage<T>(WebResponse response, Request request)
	  {
		 var stream = GetResponseStream(response);
		 var reader = new StreamReader(stream);
		 var message = reader.ReadToEnd();

		 T result = JsonConvert.DeserializeObject<T>(message, request.JsonSerializerSettings);

		 return result;
	  }

	  /// <summary>
	  /// Writes the response from the remote service to a text stream
	  /// </summary>
	  /// <param name="response">The response received from the remote service</param>
	  /// <returns>A stream object. The stream may be a subclass (GZipStream or DeflateStream) if
	  /// the response header indicates matched encoding.</returns>
	  internal static Stream GetResponseStream(WebResponse response)
	  {
		 var stream = response.GetResponseStream();

		 // handle a gzipped response
		 if (response.Headers[HttpResponseHeader.ContentEncoding] == "gzip")
			stream = new GZipStream(stream, CompressionMode.Decompress);

		 // handle a deflated response
		 else if (response.Headers[HttpResponseHeader.ContentEncoding] == "deflate")
			stream = new DeflateStream(stream, CompressionMode.Decompress);

		 return stream;
	  }

	  #endregion
   }
}
