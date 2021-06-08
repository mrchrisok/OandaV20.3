using Newtonsoft.Json;
using System.Net;

namespace OkonkwoOandaV20.TradeLibrary.REST
{
   /// <summary>
   /// Encapsulates the Request specification for a request to a V20 endpoint, 
   /// the HttpWebRequest created per the specification and the WebResponse received from the V20 endpoint
   /// </summary>
   public class WebSession
   {
	  public WebSession(Request request, HttpWebRequest webRequest, WebResponse response)
	  {
		 _request = request;
		 WebRequest = webRequest;
		 WebResponse = response;
	  }

	  /// <summary>
	  /// The Request specification for the session.
	  /// </summary>
	  private readonly Request _request;

	  /// <summary>
	  /// The request sent to the remote host specified by the Request Uri.
	  /// </summary>
	  public HttpWebRequest WebRequest { get; private set; }

	  /// <summary>
	  /// The response received from the remote host specified by the Request Uri.
	  /// </summary>
	  public WebResponse WebResponse { get; private set; }

	  /// <summary>
	  /// The JsonSerializerSettings used to serialize the request (to) and response (from) the remote host.
	  /// </summary>
	  public JsonSerializerSettings JsonSerializerSettings => _request.JsonSerializerSettings;
   }
}
