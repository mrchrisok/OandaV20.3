using Microsoft.VisualStudio.TestTools.UnitTesting;
using OkonkwoOandaV20.Framework;
using OkonkwoOandaV20.Framework.Factories;
using OkonkwoOandaV20.TradeLibrary.REST;
using System.Collections.Generic;
using System.Linq;

namespace OkonkwoOandaV20Tests.Framework.Factories
{
   [TestClass]
   public class ErrorResponseFactoryTests
   {
	  [TestMethod]
	  public void success_method_Create_returns_correct_error()
	  {
		 //arrange
		 var errorResponseTypes = typeof(ErrorResponse).Assembly.GetTypes()
			.Where(type => type.GetInterfaces().Contains(typeof(IErrorResponse)));

		 var errorResponseJson = new List<string>();
		 var failedCreates = new List<string>();

		 //act
		 foreach (var errorResponseType in errorResponseTypes)
		 {
			var jsonMessage = $"{{\"type\":\"{errorResponseType.Name}\"}}";

			var errorResponse = ErrorResponseFactory.Create(jsonMessage);

			if (errorResponse?.GetType() == errorResponseType)
			{
			   continue;
			}

			failedCreates.Add(errorResponseType.Name);
		 }
		 var failedCreatesCsv = Utilities.ConvertListToDelimitedValues(failedCreates);

		 //assert
		 Assert.IsTrue(failedCreates.Count == 0, $"Create failed for types: '{failedCreatesCsv}'");
	  }

	  [TestMethod]
	  public void success_method_Create_returns_default_error()
	  {
		 //arrange
		 var fakeErrorType = "fakeErrorType";
		 var jsonMessage = $"{{\"type\":\"{fakeErrorType}\"}}";

		 //act
		 var errorResponse = ErrorResponseFactory.Create(jsonMessage);
		 var isDefaultError = (errorResponse as ErrorResponse) != null;

		 //assert
		 Assert.IsTrue(isDefaultError, $"Default error creation failed.");
	  }
   }
}
