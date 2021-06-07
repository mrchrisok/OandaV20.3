using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OkonkwoOandaV20Tests.TradeLibrary
{
   [TestClass]
   public class PositionTests : Rest20TestsBase
   {
	  #region Position
	  [TestMethod]
	  public void success_get_positions()
	  {
		 // 14.0 & 14.1 do not need to be tested.

		 var allPositionsRetrieved = Results.Items.FirstOrDefault(x => x.Key == "14.2").Value;
		 var openPositionsRetrieved = Results.Items.FirstOrDefault(x => x.Key == "14.3").Value;

		 Assert.IsTrue(allPositionsRetrieved.Success, $"{allPositionsRetrieved.Success}: {allPositionsRetrieved.Details}");
		 Assert.IsTrue(openPositionsRetrieved.Success, $"{openPositionsRetrieved.Success}: {openPositionsRetrieved.Details}");
	  }

	  [TestMethod]
	  public void success_get_open_positions()
	  {
		 var postionHasDirection = Results.Items.FirstOrDefault(x => x.Key == "14.4").Value;
		 var positionHasUnits = Results.Items.FirstOrDefault(x => x.Key == "14.5").Value;
		 var positionHasAvgPrice = Results.Items.FirstOrDefault(x => x.Key == "14.6").Value;
		 var positionHasInstrument = Results.Items.FirstOrDefault(x => x.Key == "14.7").Value;

		 Assert.IsTrue(postionHasDirection.Success, $"{postionHasDirection.Success}: {postionHasDirection.Details}");
		 Assert.IsTrue(positionHasUnits.Success, $"{positionHasUnits.Success}: {positionHasUnits.Details}");
		 Assert.IsTrue(positionHasAvgPrice.Success, $"{positionHasAvgPrice.Success}: {positionHasAvgPrice.Details}");
		 Assert.IsTrue(positionHasInstrument.Success, $"{positionHasInstrument.Success}: {positionHasInstrument.Details}");
	  }

	  [TestMethod]
	  public void success_get_position_details()
	  {
		 var postionHasDirection = Results.Items.FirstOrDefault(x => x.Key == "14.8").Value;
		 var positionHasUnits = Results.Items.FirstOrDefault(x => x.Key == "14.9").Value;
		 var positionHasAvgPrice = Results.Items.FirstOrDefault(x => x.Key == "14.10").Value;
		 var positionHasInstrument = Results.Items.FirstOrDefault(x => x.Key == "14.11").Value;

		 Assert.IsTrue(postionHasDirection.Success, $"{postionHasDirection.Success}: {postionHasDirection.Details}");
		 Assert.IsTrue(positionHasUnits.Success, $"14.9,{positionHasUnits.Success}: {positionHasUnits.Details}");
		 Assert.IsTrue(positionHasAvgPrice.Success, $"{positionHasAvgPrice.Success}: {positionHasAvgPrice.Details}");
		 Assert.IsTrue(positionHasInstrument.Success, $"{positionHasInstrument.Success}: {positionHasInstrument.Details}");
	  }

	  [TestMethod]
	  public void success_close_position()
	  {
		 var closeOrderCreated = Results.Items.FirstOrDefault(x => x.Key == "14.12").Value;
		 var closeOrderFilled = Results.Items.FirstOrDefault(x => x.Key == "14.13").Value;
		 var closeUnitsCorrect = Results.Items.FirstOrDefault(x => x.Key == "14.14").Value;

		 Assert.IsTrue(closeOrderCreated.Success, $"{closeOrderCreated.Success}: {closeOrderCreated.Details}");
		 Assert.IsTrue(closeOrderFilled.Success, $"{closeOrderFilled.Success}: {closeOrderFilled.Details}");
		 Assert.IsTrue(closeUnitsCorrect.Success, $"{closeUnitsCorrect.Success}: {closeUnitsCorrect.Details}");
	  }

	  [TestMethod]
	  public void success_error_response_has_correct_type()
	  {
		 var caughtPositionCloseError = Results.Items.FirstOrDefault(x => x.Key == "14.E0").Value;

		 Assert.IsTrue(caughtPositionCloseError.Success, $"{caughtPositionCloseError.Success}: {caughtPositionCloseError.Details}");
	  }

	  #endregion
   }
}
