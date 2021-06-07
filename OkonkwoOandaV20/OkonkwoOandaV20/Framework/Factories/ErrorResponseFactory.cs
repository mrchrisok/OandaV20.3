using Newtonsoft.Json;
using OkonkwoOandaV20.TradeLibrary.REST;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OkonkwoOandaV20.Framework.Factories
{
   public class ErrorResponseFactory
   {
	  private static readonly IEnumerable<Type> _errorResponseTypes =
		 typeof(ErrorResponse).Assembly.GetTypes().Where(type => type.GetInterfaces().Contains(typeof(IErrorResponse)));

	  /// <summary>
	  /// Creates an instance of an IErrorResponse object by hydrating a json message.
	  /// </summary>
	  /// <param name="json">The json message to hydrate.</param>
	  /// <returns>An IErrorResponse object or null.</returns>
	  public static IErrorResponse Create(string json)
	  {
		 // the 'type' property is injected by the Rest20.GetWebResponse catch block
		 var dynamicError = JsonConvert.DeserializeObject<dynamic>(json);
		 var errorType = dynamicError.type.ToString();

		 var errorResponseType = _errorResponseTypes.FirstOrDefault(type => type.Name == errorType);

		 var errorResponse = JsonConvert.DeserializeObject(json, errorResponseType ?? typeof(ErrorResponse));

		 return errorResponse as IErrorResponse;
	  }
   }
}
