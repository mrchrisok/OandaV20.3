using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace OkonkwoOandaV20Tests.TradeLibrary
{
   [TestClass]
   public class InstrumentTests : Rest20TestsBase
   {
	  #region Instrument

	  [TestMethod]
	  public void success_retrieve_candlestick_list()
	  {
		 var candlesRetrieved = Results.Items.FirstOrDefault(x => x.Key == "12.0").Value;
		 var candlesCountCorrect = Results.Items.FirstOrDefault(x => x.Key == "12.1").Value;

		 Assert.IsTrue(candlesRetrieved.Success, $"{candlesRetrieved.Success}: {candlesRetrieved.Details}");
		 Assert.IsTrue(candlesCountCorrect.Success, $"{candlesCountCorrect.Success}: {candlesCountCorrect.Details}");
	  }

	  [TestMethod]
	  public void success_retrieve_candlestick_details()
	  {
		 var candlesInstrumentCorrect = Results.Items.FirstOrDefault(x => x.Key == "12.2").Value;
		 var candlesGranularityCorrect = Results.Items.FirstOrDefault(x => x.Key == "12.3").Value;
		 var candlesHaveMidPrice = Results.Items.FirstOrDefault(x => x.Key == "12.4").Value;
		 var candlesHaveBidPrice = Results.Items.FirstOrDefault(x => x.Key == "12.5").Value;
		 var candlesHaveAskPrice = Results.Items.FirstOrDefault(x => x.Key == "12.6").Value;

		 Assert.IsTrue(candlesInstrumentCorrect.Success, $"{candlesInstrumentCorrect.Success}: {candlesInstrumentCorrect.Details}");
		 Assert.IsTrue(candlesGranularityCorrect.Success, $"{candlesGranularityCorrect.Success}: {candlesGranularityCorrect.Details}");
		 Assert.IsTrue(candlesHaveMidPrice.Success, $"{candlesHaveMidPrice.Success}: {candlesHaveMidPrice.Details}");
		 Assert.IsTrue(candlesHaveBidPrice.Success, $"{candlesHaveBidPrice.Success}: {candlesHaveBidPrice.Details}");
		 Assert.IsTrue(candlesHaveAskPrice.Success, $"{candlesHaveAskPrice.Success}: {candlesHaveAskPrice.Details}");
	  }

	  #endregion
   }
}
